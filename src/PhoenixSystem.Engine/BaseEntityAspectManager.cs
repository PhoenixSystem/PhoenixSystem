using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager<AspectFamily> : IEntityAspectManager<AspectFamily> where AspectFamily : IEntityAspectMatchingFamily
    {
        public BaseGameManager GameManager
        {
            get;
            set;
        }

        public abstract void ComponentAddedToEntity(Entity e, BaseComponent component);

        public abstract void ComponentRemovedFromEntity(Entity e, BaseComponent component);

        public abstract IEntityAspectMatchingFamily CreateAspectFamily<AspectType>();

        public abstract IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>();

        public abstract IEnumerable<AspectType> GetNodeList<AspectType>();

        public abstract IEnumerable<AspectType> GetUnfilteredNodeList<AspectType>();

        public abstract void RegisterEntity(Entity e);

        public abstract void ReleaseAspectList<AspectType>();

        public abstract void UnregisterEntity(Entity e);
    }
}
