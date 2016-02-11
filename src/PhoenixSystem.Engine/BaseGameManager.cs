using PhoenixSystem.Engine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseGameManager : IGameManager
    {

        private IEntityAspectManager _entityAspectManager;
        public BaseGameManager(IEntityAspectManager  entityAspectManager)
        {
            _entityAspectManager = entityAspectManager;
            _entityAspectManager.GameManager = this;
        }
        
        public string CurrentChannel
        {
            get;
            private set;
        }

        public IDictionary<Guid, IEntity> Entities { get; } = new Dictionary<Guid, IEntity>();

        public bool IsUpdating { get; private set; } = false;            

        public IEnumerable<IManager> Managers { get; } = new List<IManager>();

        public IDictionary<int, ISystem> Systems { get; } = new SortedList<int,ISystem>();

        public event EventHandler<ChannelChangedEventArgs> ChannelChanged;
        public event EventHandler EntityAdded;
        public event EventHandler EntityRemoved;
        public event EventHandler SystemAdded;
        public event EventHandler SystemRemoved;
        public event EventHandler SystemStarted;
        public event EventHandler SystemSuspended;
        public event EventHandler UpdateComplete;
        public event EventHandler Updating;

        public void AddEntities(IEnumerable<IEntity> entities)
        {
            foreach(var e in entities)
            {
                AddEntity(e);
            }
        }

        public void AddEntity(IEntity e)
        {
            Entities[e.ID] = e;
            e.ComponentAdded += entityOnComponentAdded;
            e.ComponentRemoved += entityOnComponentRemoved;
            _entityAspectManager.RegisterEntity(e);
            EntityAdded(this, new EntityChangedEventArgs() { Entity = e });
        }

        private void entityOnComponentRemoved(object sender, ComponentChangedEventArgs e)
        {
            IEntity entity = sender as IEntity;
            _entityAspectManager.ComponentRemovedFromEntity(entity, e.Component);
        }

        private void entityOnComponentAdded(object sender, ComponentChangedEventArgs e)
        {
            IEntity entity = sender as IEntity; 
            _entityAspectManager.ComponentAddedToEntity(entity, e.Component);
        }

        public void AddSystem(ISystem system)
        {
            if (hasSystem(system))
                throw new ApplicationException("System " + system.GetType().Name + " already added to game manager");
            Systems.Add(system.Priority,system);
            system.AddToGameManager(this);
            SystemAdded(this, new SystemChangedEventArgs() { System = system });
        }

        private bool hasSystem(ISystem system)
        {
            return Systems.Any(s => s.Value.ID == system.ID);
        }

        public IEnumerable<AspectType> GetAspectList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public ISystem GetSystemByType<SystemType>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspectType> GetUnfilteredAspectList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public void RegisterManager(IManager manager)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspectType> ReleaseAspectList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public void RemoveAllSystems(bool shouldNotify)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntity(IEntity e)
        {
            throw new NotImplementedException();
        }

        public void RemoveSytem(ISystem system, bool shouldNotify)
        {
            throw new NotImplementedException();
        }

        public void SetChannel(string newChannel)
        {
            CurrentChannel = newChannel;
            ChannelChanged(this, new ChannelChangedEventArgs() { Channel = CurrentChannel });
        }

        public void StartSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public void SuspendSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public void Update(ITickEvent tickEvent)
        {
            throw new NotImplementedException();
        }
    }
}