using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Game
{
    public interface IGameManager
    {
        IEntityManager EntityManager { get; }
        IEnumerable<IManager> Managers { get; }
        IEnumerable<ISystem> Systems { get; }
        bool IsUpdating { get; }
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
        void RemoveSystem<TSystemType>(bool shouldNotify) where TSystemType : ISystem;
        void RemoveAllSystems(bool shouldNotify);
        event EventHandler SystemSuspended;
        void SuspendSystem<TSystemType>() where TSystemType : ISystem;
        event EventHandler SystemStarted;
        void StartSystem<TSystemType>() where TSystemType : ISystem;
        IEnumerable<TAspectType> GetAspectList<TAspectType>() where TAspectType : IAspect, new();
        IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new();
        void ReleaseAspectList<TAspectType>();
        void RegisterManager(IManager manager);
    }
}