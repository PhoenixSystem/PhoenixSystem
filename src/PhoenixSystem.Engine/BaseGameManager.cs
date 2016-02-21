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

        private List<ISystem> _systems = new List<ISystem>();
        public IEnumerable<ISystem> Systems
        {
            get
            {
                return _systems;
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

            _systems.Add(system);
            _systems.Sort();
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
                RemoveSystem(_systems.First().GetType(), shouldNotify);
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

        public void RemoveSystem(Type systemType, bool shouldNotify)
        {
            var system = _systems.FirstOrDefault(s => s.GetType() == systemType);
            if (system == null) return;

            system.RemoveFromGameManager(this);
            _systems.Remove(system);
            _systems.Sort();
            if (!shouldNotify) return;

            SystemRemoved?.Invoke(this, new SystemRemovedEventArgs(system));
        }

        public void RemoveSystem<SystemType>(bool shouldNotify) where SystemType : ISystem
        {
            RemoveSystem(typeof(SystemType), shouldNotify);
        }

        public void StartSystem<SystemType>() where SystemType: ISystem
        {
            var system = _systems.SingleOrDefault(s => s.GetType() == typeof(SystemType));
            if (system == null) return;
            system.Start();
            SystemStarted?.Invoke(this, new SystemStartedEventArgs(system));
        }

        public void SuspendSystem<SystemType>() where SystemType : ISystem
        {
            var system = _systems.SingleOrDefault(s => s.GetType() == typeof(SystemType));
            if (system == null) return;
            system.Stop();

            SystemSuspended?.Invoke(this, new SystemSuspendedEventArgs(system));
        }

        public virtual void Update(ITickEvent tickEvent)
        {
            IsUpdating = true;

            var curChan = _channelManager.Channel;
            foreach (var system in _systems)
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
            return _systems.Any(s => s.ID == system.ID);
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