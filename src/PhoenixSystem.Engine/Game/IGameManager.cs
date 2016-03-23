using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Game
{
    public interface IGameManager
    {
        event EventHandler SystemAdded;
        event EventHandler SystemRemoved;
        event EventHandler SystemSuspended;
        event EventHandler SystemStarted;
        event EventHandler EntityAdded;
        event EventHandler EntityRemoved;

        IEntityAspectManager EntityAspectManager { get; }
        IEntityManager EntityManager { get; }
        IManagers Managers { get; }
        ISystemManager Systems { get; }
        bool IsUpdating { get; }
        bool IsDrawing { get; }
        void Update(ITickEvent tickEvent);
        void Draw(ITickEvent tickEvent);        
        void AddEntity(IEntity e);
        void AddEntities(IEnumerable<IEntity> entities);
        void RemoveAllEntities();        
        void RemoveEntity(IEntity e);        
        void AddSystem(ISystem system);        
        void RemoveSystem<TSystemType>(bool shouldNotify) where TSystemType : ISystem;
        void RemoveAllSystems(bool shouldNotify);        
        void SuspendSystem<TSystemType>() where TSystemType : ISystem;        
        void StartSystem<TSystemType>() where TSystemType : ISystem;        
        void ReleaseAspectList<TAspectType>();
        void RegisterManager(IManager manager);
        IEnumerable<TAspectType> GetAspectList<TAspectType>() where TAspectType : IAspect, new();
        IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new();
    }
}