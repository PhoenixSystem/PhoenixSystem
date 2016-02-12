using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager : IEntityAspectManager
    {
        public IGameManager GameManager { get; set; }
        public abstract void ComponentAddedToEntity(IEntity e, IComponent component);
        public abstract void ComponentRemovedFromEntity(IEntity e, IComponent component);
        public abstract IEnumerable<IAspect> GetAspectList<AspectType>() where AspectType : IAspect, new();
        public abstract IEnumerable<IAspect> GetUnfilteredAspectList<AspectType>() where AspectType : IAspect, new();
        public abstract void RegisterEntity(IEntity e);
        public abstract void ReleaseAspectList<AspectType>();
        public abstract IEntityAspectMatchingFamily CreateAspectMatchingFamily<AspectType>() where AspectType : IAspect, new();
        public abstract void UnregisterEntity(IEntity e);
    }
}