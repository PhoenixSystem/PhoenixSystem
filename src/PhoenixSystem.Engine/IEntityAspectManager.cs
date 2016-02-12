using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectManager
    {
        IGameManager GameManager { get; set; }
        IEnumerable<IAspect> GetAspectList<AspectType>() where AspectType : IAspect, new();
        IEnumerable<IAspect> GetUnfilteredAspectList<AspectType>() where AspectType : IAspect, new();
        void RegisterEntity(IEntity e);
        void UnregisterEntity(IEntity e);
        void ComponentRemovedFromEntity(IEntity e, IComponent component);
        void ComponentAddedToEntity(IEntity e, IComponent component);
        void ReleaseAspectList<AspectType>();      
        IEntityAspectMatchingFamily CreateAspectMatchingFamily<AspectType>() where AspectType : IAspect, new();
    }
}
