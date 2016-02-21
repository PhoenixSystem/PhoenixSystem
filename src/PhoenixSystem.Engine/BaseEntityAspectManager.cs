using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager : IEntityAspectManager
    {
    
        public abstract void ComponentAddedToEntity(IEntity e, IComponent component);
        public abstract void ComponentRemovedFromEntity(IEntity e, IComponent component);
        public abstract IEnumerable<AspectType> GetAspectList<AspectType>() where AspectType : IAspect, new();
        public abstract IEnumerable<AspectType> GetUnfilteredAspectList<AspectType>() where AspectType : IAspect, new();
        public abstract void RegisterEntity(IEntity e);
        public abstract void ReleaseAspectList<AspectType>();        
        public abstract void UnregisterEntity(IEntity e);
    }
}