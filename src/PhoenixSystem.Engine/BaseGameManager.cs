using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.Extensions;

namespace PhoenixSystem.Engine
{
    public abstract class BaseGameManager : IGameManager
    {
        private readonly IEntityAspectManager _entityAspectManager;

        private readonly IChannelManager _channelManager;

        protected BaseGameManager(
            IEntityAspectManager entityAspectManager, 
            IEntityManager entityManager,
            IChannelManager channelManager)
        {
            EntityManager = entityManager;
            _channelManager = channelManager;
            _entityAspectManager = entityAspectManager;
            _entityAspectManager.GameManager = this;            
        }


        public IEntityManager EntityManager { get; }

        public bool IsUpdating { get; private set; }

        private SortedList<int, IManager> _managers = new SortedList<int, IManager>();
        public IEnumerable<IManager> Managers
        {
            get
            {
                return _managers.Values;
            }
        }

        private SortedList<int, ISystem> _systems = new SortedList<int, ISystem>();
        public IEnumerable<ISystem> Systems
        {
            get
            {
                return _systems.Values;
            }
        }

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
            if (HasSystem(system))
            {
                throw new ApplicationException($"System {system.GetType().Name} already added to game manager.");
            }

            _systems.Add(system.Priority, system);
            system.AddToGameManager(this);

            SystemAdded?.Invoke(this, new SystemChangedEventArgs(system));
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
            _managers.Add(manager.Priority, manager);
        }

        public void ReleaseAspectList<TAspectType>()
        {
            _entityAspectManager.ReleaseAspectList<TAspectType>();
        }

        public void RemoveAllSystems(bool shouldNotify)
        {
            while (_systems.Count > 0)
            {
                RemoveSytem(_systems.First().Value, shouldNotify);
            }
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

        public void RemoveSytem(ISystem system, bool shouldNotify)
        {
            var key = _systems.GetKeyBySystemId(system.ID);

            if (!key.HasValue) return;

            system = _systems[key.Value];
            system.RemoveFromGameManager(this);
            _systems.Remove(key.Value);

            if (!shouldNotify) return;

            SystemRemoved?.Invoke(this, new SystemRemovedEventArgs(system));
        }

        public void StartSystem(ISystem system)
        {
            var key = _systems.GetKeyBySystemId(system.ID);

            if (!key.HasValue) return;

            system = _systems[key.Value];
            system.Start();
            SystemStarted?.Invoke(this, new SystemStartedEventArgs(system));
        }

        public void SuspendSystem(ISystem system)
        {
            var key = _systems.GetKeyBySystemId(system.ID);

            if (!key.HasValue) return;

            system = _systems[key.Value];
            system.Stop();

            SystemSuspended?.Invoke(this, new SystemSuspendedEventArgs(system));
        }

        public virtual void Update(ITickEvent tickEvent)
        {
            IsUpdating = true;

            var curChan = _channelManager.Channel;
            foreach (var system in _systems.Values)
            {
                if (system.IsInChannel(curChan, "all"))
                    system.Update(tickEvent);
            }

            OnSystemsUpdated(tickEvent);
            foreach (var manager in _managers.Values)
            {
                    manager.Update();
            }

            OnManagersUpdated(tickEvent);

            IsUpdating = false;
        }

        protected virtual void OnSystemsUpdated(ITickEvent tickEvent)
        {
        }

        protected virtual void OnManagersUpdated(ITickEvent tickEvent)
        {
        }

        private bool HasSystem(ISystem system)
        {
            return _systems.Any(s => s.Value.ID == system.ID);
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