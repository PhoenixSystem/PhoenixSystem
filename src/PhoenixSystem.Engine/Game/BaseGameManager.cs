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
            EntityManager = entityManager;
            Systems = systemManager;
            _entityAspectManager = entityAspectManager;
            
            Systems.SetGameManager(this);
            Systems.SystemAdded += (sender, args) => SystemAdded?.Invoke(sender, args);
            Systems.SystemStarted += (sender, args) => SystemStarted?.Invoke(sender, args);
            Systems.SystemRemoved += (sender, args) => SystemRemoved?.Invoke(sender, args);
            Systems.SystemStarted += (sender, args) => SystemStarted?.Invoke(sender, args);
            Systems.SystemStopped += (sender, args) => SystemSuspended?.Invoke(sender, args);
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
            EntityManager.Entities[entity.ID] = entity;

            entity.ComponentAdded += EntityOnComponentAdded;
            entity.ComponentRemoved += EntityOnComponentRemoved;

            _entityAspectManager.RegisterEntity(entity);

            EntityAdded?.Invoke(this, new EntityChangedEventArgs(entity));
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
            foreach (var entity in EntityManager.Entities)
            {
                entity.Value.Delete();
            }

            EntityManager.Entities.Clear();
        }

        public void RemoveEntity(IEntity entity)
        {
            if (!EntityManager.Entities.ContainsKey(entity.ID)) return;

            entity = EntityManager.Entities[entity.ID];
            entity.ComponentAdded -= EntityOnComponentAdded;
            entity.ComponentRemoved -= EntityOnComponentRemoved;

            _entityAspectManager.UnregisterEntity(entity);

            EntityManager.Entities.Remove(entity.ID);

            EntityRemoved?.Invoke(this, new EntityRemovedEventArgs(entity));
        }

        public void RemoveSystem<TSystemType>(bool shouldNotify) where TSystemType : ISystem
        {
            RemoveSystem(typeof (TSystemType), shouldNotify);
        }

        public void StartSystem<TSystemType>() where TSystemType : ISystem
        {
            Systems.Start(typeof (TSystemType));
        }

        public void SuspendSystem<TSystemType>() where TSystemType : ISystem
        {
            Systems.Stop(typeof (TSystemType));
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

        private void EntityOnComponentRemoved(object sender, ComponentChangedEventArgs e)
        {
            _entityAspectManager.ComponentRemovedFromEntity(sender as IEntity, e.Component);
        }

        private void EntityOnComponentAdded(object sender, ComponentChangedEventArgs e)
        {
            _entityAspectManager.ComponentAddedToEntity(sender as IEntity, e.Component);
        }
    }
}