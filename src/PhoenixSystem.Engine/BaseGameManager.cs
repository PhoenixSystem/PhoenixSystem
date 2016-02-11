using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseGameManager : IGameManager
    {
        public IEnumerable<IEntity> Entities { get; }
        public IEnumerable<ISystem> Systems { get; }
        public IEnumerable<IManager> Managers { get; }
        public bool IsUpdating { get; }
        public string CurrentChannel { get; }
        public event EventHandler ChannelChanged;

        public void SetChannel(string newChannel)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Updating;
        public event EventHandler UpdateComplete;

        public void Update(ITickEvent tickEvent)
        {
            throw new NotImplementedException();
        }

        public event EventHandler EntityAdded;

        public void AddEntity(IEntity e)
        {
            throw new NotImplementedException();
        }

        public void AddEntities(IEnumerable<IEntity> entities)
        {
            throw new NotImplementedException();
        }

        public event EventHandler EntityRemoved;

        public void RemoveEntity(IEntity e)
        {
            throw new NotImplementedException();
        }

        public event EventHandler SystemAdded;

        public void AddSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public event EventHandler SystemRemoved;

        public void RemoveSytem(ISystem system, bool shouldNotify)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllSystems(bool shouldNotify)
        {
            throw new NotImplementedException();
        }

        public event EventHandler SystemSuspended;

        public void SuspendSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public event EventHandler SystemStarted;

        public void StartSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspectType> GetNodeList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspectType> GetUnfilteredNodeList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspectType> ReleaseNodeList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public ISystem GetSystemByType<SystemType>()
        {
            throw new NotImplementedException();
        }

        public void RegisterManager(IManager manager)
        {
            throw new NotImplementedException();
        }
    }
}