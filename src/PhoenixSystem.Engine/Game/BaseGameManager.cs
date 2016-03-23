using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.System;

// ReSharper disable CollectionNeverQueried.Local

namespace PhoenixSystem.Engine.Game
{
    public abstract class BaseGameManager : IGameManager
    {
        private readonly IEntityAspectManager _entityAspectManager;
        private readonly List<IManager> _managers = new List<IManager>();

        protected BaseGameManager(
            IEntityAspectManager entityAspectManager,
            IEntityManager entityManager,
            ISystemManager systemManager)
        {
            _entityAspectManager = entityAspectManager;
            EntityManager = entityManager;
            Systems = systemManager;

            RegisterEvents();
        }

        public IEntityManager EntityManager { get; }
        public IEnumerable<IManager> Managers => _managers;
        public ISystemManager Systems { get; }
        public bool IsUpdating { get; private set; }
        public bool IsDrawing { get; private set; }

        public event EventHandler EntityAdded;
        public event EventHandler EntityRemoved;
        public event EventHandler SystemAdded;
        public event EventHandler SystemRemoved;
        public event EventHandler SystemStarted;
        public event EventHandler SystemSuspended;

        public void AddEntities(IEnumerable<IEntity> entities)
        {
            foreach (var e in entities)
            {
                AddEntity(e);
            }
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
            return _entityAspectManager.GetAspectList<TAspectType>();
        }

        public IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new()
        {
            return _entityAspectManager.GetUnfilteredAspectList<TAspectType>();
        }

        public void RegisterManager(IManager manager)
        {
            manager.Register(this);

            _managers.Add(manager);
            _managers.Sort();
        }

        public void ReleaseAspectList<TAspectType>()
        {
            _entityAspectManager.ReleaseAspectList<TAspectType>();
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

            foreach (var manager in _managers)
            {
                manager.Update();
            }

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

        private void RegisterEvents()
        {
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
            Systems.SystemStarted += (sender, args) => SystemStarted?.Invoke(sender, args);
            Systems.SystemStopped += (sender, args) => SystemSuspended?.Invoke(sender, args);
        }

        private void EntityManagerOnComponentRemoved(object sender, ComponentRemovedEventArgs args)
        {
            _entityAspectManager.ComponentRemovedFromEntity(sender as IEntity, args.Component);
        }

        private void EntityManagerOnComponentAdded(object sender, ComponentAddedEventArgs args)
        {
            _entityAspectManager.ComponentAddedToEntity(sender as IEntity, args.Component);
        }

        private void EntityManagerOnEntityRemoved(object sender, EntityRemovedEventArgs args)
        {
            _entityAspectManager.UnregisterEntity(args.Entity);

            EntityRemoved?.Invoke(sender, args);
        }

        private void EntityManagerOnEntityAdded(object sender, EntityChangedEventArgs args)
        {
            _entityAspectManager.RegisterEntity(args.Entity);

            EntityAdded?.Invoke(sender, args);
        }
    }
}