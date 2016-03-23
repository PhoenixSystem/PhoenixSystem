using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Game
{
    public abstract class BaseGameManager : IGameManager
    {        
        protected BaseGameManager(
            IEntityAspectManager entityAspectManager,
            IEntityManager entityManager,
            ISystemManager systemManager, 
            IManagers managers)
        {
            EntityAspectManager = entityAspectManager;
            EntityManager = entityManager;
            Systems = systemManager;
            Managers = managers;

            RegisterManagers();
        }

        public IEntityAspectManager EntityAspectManager { get; }
        public IEntityManager EntityManager { get; }
        public IManagers Managers { get; }
        public ISystemManager Systems { get; }

        public bool IsUpdating { get; private set; }
        public bool IsDrawing { get; private set; }

        public event EventHandler<SystemChangedEventArgs> SystemAdded;
        public event EventHandler<SystemRemovedEventArgs> SystemRemoved;
        public event EventHandler<SystemStoppedEventArgs> SystemStopped;
        public event EventHandler<SystemStartedEventArgs> SystemStarted;
        public event EventHandler<EntityChangedEventArgs> EntityAdded;
        public event EventHandler<EntityRemovedEventArgs> EntityRemoved;

        public void AddEntities(IEnumerable<IEntity> entities)
        {
            EntityManager.Add(entities);
        }

        public void AddEntity(IEntity entity)
        {
            EntityManager.Add(entity);
        }

        public void AddSystem(ISystem system)
        {
            Systems.Add(system);
        }

        public IEnumerable<TAspectType> GetAspectList<TAspectType>() where TAspectType : IAspect, new()
        {
            return EntityAspectManager.GetAspectList<TAspectType>();
        }

        public IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new()
        {
            return EntityAspectManager.GetUnfilteredAspectList<TAspectType>();
        }

        public void RegisterManager(IManager manager)
        {
            Managers.Add(manager);
        }

        public void ReleaseAspectList<TAspectType>()
        {
            EntityAspectManager.ReleaseAspectList<TAspectType>();
        }

        public void RemoveAllSystems(bool shouldNotify)
        {
            Systems.Clear(shouldNotify);
        }

        public void RemoveAllEntities()
        {
            EntityManager.Clear();
        }

        public void RemoveEntity(IEntity entity)
        {
            EntityManager.Remove(entity);
        }

        public void RemoveSystem<TSystemType>(bool shouldNotify) where TSystemType : ISystem
        {
            RemoveSystem(typeof(TSystemType), shouldNotify);
        }

        public void StartSystem<TSystemType>() where TSystemType : ISystem
        {
            Systems.Start(typeof(TSystemType));
        }

        public void SuspendSystem<TSystemType>() where TSystemType : ISystem
        {
            Systems.Stop(typeof(TSystemType));
        }

        public virtual void Update(ITickEvent tickEvent)
        {
            IsUpdating = true;

            Systems.Update(tickEvent);

            OnSystemsUpdated(tickEvent);

            Managers.Update(tickEvent);
            
            OnManagersUpdated(tickEvent);

            IsUpdating = false;
        }

        public void Draw(ITickEvent tickEvent)
        {
            IsDrawing = true;

            Systems.Draw(tickEvent);

            IsDrawing = false;
        }

        public void RemoveSystem(Type systemType, bool shouldNotify)
        {
            Systems.Remove(systemType, shouldNotify);
        }

        protected virtual void OnSystemsUpdated(ITickEvent tickEvent)
        {
        }

        protected virtual void OnManagersUpdated(ITickEvent tickEvent)
        {
        }

        private void RegisterManagers()
        {
            Managers.Register(this);

            // Entity events
            EntityManager.Register(this);
            EntityManager.EntityAdded += EntityManagerOnEntityAdded;
            EntityManager.EntityRemoved += EntityManagerOnEntityRemoved;
            EntityManager.ComponentAdded += EntityManagerOnComponentAdded;
            EntityManager.ComponentRemoved += EntityManagerOnComponentRemoved;

            // Systems events
            Systems.Register(this);
            Systems.SystemAdded += (sender, args) => SystemAdded?.Invoke(sender, args);
            Systems.SystemStarted += (sender, args) => SystemStarted?.Invoke(sender, args);
            Systems.SystemRemoved += (sender, args) => SystemRemoved?.Invoke(sender, args);
            Systems.SystemStopped += (sender, args) => SystemStopped?.Invoke(sender, args);
        }

        private void EntityManagerOnComponentRemoved(object sender, ComponentRemovedEventArgs args)
        {
            EntityAspectManager.ComponentRemovedFromEntity(sender as IEntity, args.Component);
        }

        private void EntityManagerOnComponentAdded(object sender, ComponentAddedEventArgs args)
        {
            EntityAspectManager.ComponentAddedToEntity(sender as IEntity, args.Component);
        }

        private void EntityManagerOnEntityRemoved(object sender, EntityRemovedEventArgs args)
        {
            EntityAspectManager.UnregisterEntity(args.Entity);
            EntityRemoved?.Invoke(sender, args);
        }

        private void EntityManagerOnEntityAdded(object sender, EntityChangedEventArgs args)
        {
            EntityAspectManager.RegisterEntity(args.Entity);
            EntityAdded?.Invoke(sender, args);
        }
    }
}