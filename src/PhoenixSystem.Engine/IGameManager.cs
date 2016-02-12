using PhoenixSystem.Engine.Events;
using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IGameManager
    {
        IDictionary<Guid, IEntity> Entities { get; }
        IDictionary<int, ISystem> Systems { get; }
        IList<IManager> Managers { get; }
        bool IsUpdating { get; }
        string CurrentChannel { get; }
        event EventHandler<ChannelChangedEventArgs> ChannelChanged;
        void SetChannel(string newChannel);
        event EventHandler Updating;
        event EventHandler UpdateComplete;
        void Update(ITickEvent tickEvent);
        event EventHandler EntityAdded;        
        void AddEntity(IEntity e);
        void AddEntities(IEnumerable<IEntity> entities);
        void RemoveAllEntities();
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
        IEnumerable<IAspect> GetAspectList<AspectType>() where AspectType : IAspect, new();
        IEnumerable<IAspect> GetUnfilteredAspectList<AspectType>() where AspectType : IAspect, new();
        void ReleaseAspectList<AspectType>();
        void RegisterManager(IManager manager);
    }
}