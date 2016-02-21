using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectManager
    {
        
        IEnumerable<AspectType> GetAspectList<AspectType>() where AspectType : IAspect, new();
        IEnumerable<AspectType> GetUnfilteredAspectList<AspectType>() where AspectType : IAspect, new();
        void RegisterEntity(IEntity e);
        void UnregisterEntity(IEntity e);
        void ComponentRemovedFromEntity(IEntity e, IComponent component);
        void ComponentAddedToEntity(IEntity e, IComponent component);
        void ReleaseAspectList<AspectType>();              
    }
}
