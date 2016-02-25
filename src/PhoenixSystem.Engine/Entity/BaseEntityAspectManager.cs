using System.Collections.Generic;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Entity
{
    public abstract class BaseEntityAspectManager : IEntityAspectManager
    {    
        public abstract void ComponentAddedToEntity(IEntity entity, IComponent component);
        public abstract void ComponentRemovedFromEntity(IEntity entity, IComponent component);
        public abstract IEnumerable<TAspectType> GetAspectList<TAspectType>() where TAspectType : IAspect, new();
        public abstract IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>() where TAspectType : IAspect, new();
        public abstract void RegisterEntity(IEntity entity);
        public abstract void ReleaseAspectList<TAspectType>();        
        public abstract void UnregisterEntity(IEntity entity);
    }
}