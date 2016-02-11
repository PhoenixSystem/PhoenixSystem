using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager : IEntityAspectManager
    {
        public IGameManager GameManager { get; set; }

        public abstract void ComponentAddedToEntity(IEntity e, IComponent component);

        public abstract void ComponentRemovedFromEntity(IEntity e, IComponent component);

        public abstract IEntityAspectMatchingFamily CreateAspectFamily<AspectType>() where AspectType : BaseAspect, new();

        

        public abstract IEnumerable<IAspect> GetNodeList<AspectType>() where AspectType : BaseAspect, new();

        public abstract IEnumerable<IAspect> GetUnfilteredNodeList<AspectType>() where AspectType : BaseAspect, new();

        public abstract void RegisterEntity(IEntity e);

        public abstract void ReleaseAspectList<AspectType>();

        public abstract void UnregisterEntity(IEntity e);

        public abstract IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>();
    }
}
