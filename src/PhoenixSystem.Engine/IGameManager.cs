using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IGameManager
    {
        IEnumerable<IEntity> Entities { get; }
        IEnumerable<ISystem> Systems { get; }
        IEnumerable<IManager> Managers { get; }
        bool IsUpdating { get; }
        string CurrentChannel { get; }
        event EventHandler ChannelChanged;
        void SetChannel(string newChannel);
        event EventHandler Updating;
        event EventHandler UpdateComplete;
        void Update(ITickEvent tickEvent);
        event EventHandler EntityAdded;
        void AddEntity(IEntity e);
        void AddEntities(IEnumerable<IEntity> entities);
        event EventHandler EntityRemoved;
        void RemoveEntity(IEntity e);
        event EventHandler SystemAdded;
        void AddSystem(ISystem system);
        event EventHandler SystemRemoved;
        void RemoveSytem(ISystem system, bool shouldNotify);
        void RemoveAllSystems(bool shouldNotify);
        event EventHandler SystemSuspended;
        void SuspendSystem(ISystem system);
        event EventHandler SystemStarted;
        void StartSystem(ISystem system);
        IEnumerable<AspectType> GetNodeList<AspectType>();
        IEnumerable<AspectType> GetUnfilteredNodeList<AspectType>();
        IEnumerable<AspectType> ReleaseNodeList<AspectType>();
        ISystem GetSystemByType<SystemType>();
        void RegisterManager(IManager manager);
    }
}