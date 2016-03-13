using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.Extensions;
using PhoenixSystem.Engine.System;

// ReSharper disable CollectionNeverQueried.Local

namespace PhoenixSystem.Engine.Game
{
    public abstract class BaseGameManager : IGameManager
    {
        private readonly IChannelManager _channelManager;
        private readonly List<IDrawableSystem> _drawableSystems = new List<IDrawableSystem>();
        private readonly IEntityAspectManager _entityAspectManager;
        private readonly List<IManager> _managers = new List<IManager>();
        private readonly List<ISystem> _systems = new List<ISystem>();

        protected BaseGameManager(
            IEntityAspectManager entityAspectManager,
            IEntityManager entityManager,
            IChannelManager channelManager)
        {
            EntityManager = entityManager;
            _channelManager = channelManager;
            _entityAspectManager = entityAspectManager;
        }

        public IEntityManager EntityManager { get; }
        public bool IsUpdating { get; private set; }
        public IEnumerable<IManager> Managers => _managers;
        public IEnumerable<ISystem> Systems => _systems;
        public IEnumerable<IDrawableSystem> DrawableSystems => _drawableSystems;

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
            if (HasSystem(system))
            {
                throw new InvalidOperationException($"System {system.GetType().Name} already added to game manager.");
            }

            _systems.Add(system);
            _systems.Sort();

            if (system is IDrawableSystem)
            {
                _drawableSystems.Add(system as IDrawableSystem);
                _drawableSystems.Sort();
            }

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

            _managers.Add(manager);
            _managers.Sort();
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

        public void RemoveSystem<TSystemType>(bool shouldNotify) where TSystemType : ISystem
        {
            RemoveSystem(typeof (TSystemType), shouldNotify);
        }

        public void StartSystem<TSystemType>() where TSystemType : ISystem
        {
            var system = _systems.SingleOrDefault(s => s.GetType() == typeof (TSystemType));

            if (system == null) return;

            system.Start();

            SystemStarted?.Invoke(this, new SystemStartedEventArgs(system));
        }

        public void SuspendSystem<TSystemType>() where TSystemType : ISystem
        {
            var system = _systems.SingleOrDefault(s => s.GetType() == typeof (TSystemType));

            if (system == null) return;

            system.Stop();

            SystemSuspended?.Invoke(this, new SystemSuspendedEventArgs(system));
        }

        public virtual void Update(ITickEvent tickEvent)
        {
            IsUpdating = true;

            var curChan = _channelManager.Channel;

            foreach (var system in _systems.Where(system => system.IsInChannel(curChan, "all")))
            {
                system.Update(tickEvent);
            }

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

            foreach (var drawableSystem in DrawableSystems.Where(sys => sys.IsInChannel(_channelManager.Channel)))
            {
                drawableSystem.Draw(tickEvent);
            }

            IsDrawing = false;
        }

        public void RemoveSystem(Type systemType, bool shouldNotify)
        {
            var system = _systems.FirstOrDefault(s => s.GetType() == systemType);

            if (system == null) return;

            system.RemoveFromGameManager(this);

            _systems.Remove(system);
            _systems.Sort();

            if (system is IDrawableSystem)
            {
                _drawableSystems.Remove(system as IDrawableSystem);
                _drawableSystems.Sort();
            }

            if (!shouldNotify) return;

            SystemRemoved?.Invoke(this, new SystemRemovedEventArgs(system));
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