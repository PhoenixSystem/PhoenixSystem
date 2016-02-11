using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager : IEntityAspectManager
    {
        public BaseGameManager GameManager
        {
            get;
            set;
        }

        public abstract void ComponentAddedToEntity(Entity e, BaseComponent component);

        public abstract void ComponentRemovedFromEntity(Entity e, BaseComponent component);

        public abstract IEntityAspectMatchingFamily CreateAspectFamily<AspectType>() where AspectType : BaseAspect, new();

        public abstract IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>();

        public abstract IEnumerable<IAspect> GetNodeList<AspectType>() where AspectType : BaseAspect, new();

        public abstract IEnumerable<IAspect> GetUnfilteredNodeList<AspectType>() where AspectType : BaseAspect, new();

        public abstract void RegisterEntity(Entity e);

        public abstract void ReleaseAspectList<AspectType>();

        public abstract void UnregisterEntity(Entity e);
    }
}
