using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine
{
    public abstract class BaseGameManager : IGameManager
    {
        private readonly IEntityAspectManager _entityAspectManager;

        private IChannelManager _channelManager;
        protected BaseGameManager(IEntityAspectManager entityAspectManager, IEntityManager entityManager, IChannelManager channelManager)
        {
            _entityAspectManager = entityAspectManager;
            _entityAspectManager.GameManager = this;
            EntityManager = entityManager;
            _channelManager = channelManager;
        }

        

        public IEntityManager EntityManager { get; private set; }

        public bool IsUpdating { get; private set; }

        public IDictionary<int, IManager> Managers { get; } = new SortedList<int, IManager>();

        public IDictionary<int, ISystem> Systems { get; } = new SortedList<int, ISystem>();
        
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
                throw new ApplicationException($"System {system.GetType().Name} already added to game manager.");

            Systems.Add(system.Priority, system);
            system.AddToGameManager(this);
            SystemAdded?.Invoke(this, new SystemChangedEventArgs(system));
        }

        public IEnumerable<IAspect> GetAspectList<TAspectType>() where TAspectType : IAspect, new()
        {
            return _entityAspectManager.GetAspectList<TAspectType>();
        }

        public IEnumerable<IAspect> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new()
        {
            return _entityAspectManager.GetUnfilteredAspectList<TAspectType>();
        }

        public void RegisterManager(IManager manager)
        {
            manager.Register(this);
            Managers.Add(manager.Priority,manager);
        }

        public void ReleaseAspectList<TAspectType>()
        {
            _entityAspectManager.ReleaseAspectList<TAspectType>();
        }

        public void RemoveAllSystems(bool shouldNotify)
        {
            while (Systems.Count > 0)
            {
                RemoveSytem(Systems[0], shouldNotify);
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
            var key = Systems.GetKeyBySystemId(system.ID);

            if (!key.HasValue) return;

            system = Systems[key.Value];
            system.RemoveFromGameManager(this);
            Systems.Remove(key.Value);

            if (!shouldNotify) return;

            SystemRemoved?.Invoke(this, new SystemRemovedEventArgs(system));
        }        

        public void StartSystem(ISystem system)
        {
            var key = Systems.GetKeyBySystemId(system.ID);

            if (!key.HasValue) return;

            system = Systems[key.Value];
            system.Start();
            SystemStarted?.Invoke(this, new SystemStartedEventArgs(system));
        }

        public void SuspendSystem(ISystem system)
        {
            var key = Systems.GetKeyBySystemId(system.ID);

            if (!key.HasValue) return;

            system = Systems[key.Value];
            system.Stop();
            SystemSuspended?.Invoke(this, new SystemSuspendedEventArgs(system));
        }

        protected virtual void OnSystemsUpdated(ITickEvent tickEvent) { }
        protected virtual void OnManagersUpdated(ITickEvent tickEvent) { }

        public virtual void Update(ITickEvent tickEvent)
        {
            IsUpdating = true;
            var curChan = _channelManager.Channel;
            foreach(var system in Systems.Values)
            {
                if(system.IsInChannel(curChan, "all"))
                    system.Update(tickEvent);
            }
            OnSystemsUpdated(tickEvent);
            foreach(var manager in Managers.Values)
            {
                if (manager.IsInChannel(curChan, "all"))
                    manager.Update();
            }
            OnManagersUpdated(tickEvent);
            IsUpdating = false;
        }

        private bool HasSystem(ISystem system)
        {
            return Systems.Any(s => s.Value.ID == system.ID);
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

    public static class SystemHelper
    {
        public static int? GetKeyBySystemId(this IDictionary<int, ISystem> systems, Guid systemId)
        {
            foreach (var system in systems.Where(system => system.Value.ID == systemId))
            {
                return system.Key;
            }

            return null;
        }
    }
}