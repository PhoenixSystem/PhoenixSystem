using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseGameManager :IGameManager
    {
        public BaseGameManager()
        {

        }

        public string CurrentChannel
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IEntity> Entities
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsUpdating
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IManager> Managers
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<ISystem> Systems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler ChannelChanged;
        public event EventHandler EntityAdded;
        public event EventHandler EntityRemoved;
        public event EventHandler SystemAdded;
        public event EventHandler SystemRemoved;
        public event EventHandler SystemStarted;
        public event EventHandler SystemSuspended;
        public event EventHandler UpdateComplete;
        public event EventHandler Updating;

        public void AddEntities(IEnumerable<Entity> entities)
        {
            throw new NotImplementedException();
        }

        public void AddEntity(Entity e)
        {
            throw new NotImplementedException();
        }

        public void AddSystem(ISystem system)
        {
            throw new NotImplementedException();
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

        public void RemoveEntity(Entity e)
        {
            throw new NotImplementedException();
        }

        public void RemoveSytem(ISystem system, bool shouldNotify)
        {
            throw new NotImplementedException();
        }

        public void SetChannel(string newChannel)
        {
            throw new NotImplementedException();
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