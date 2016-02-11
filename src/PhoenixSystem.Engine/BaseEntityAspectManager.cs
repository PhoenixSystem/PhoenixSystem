using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager<AspectFamily> : IEntityAspectManager<AspectFamily>
        where AspectFamily : IEntityAspectMatchingFamily
    {
        public IGameManager GameManager { get; set; }

        public abstract void ComponentAddedToEntity(IEntity e, IComponent component);

        public abstract void ComponentRemovedFromEntity(IEntity e, IComponent component);

        public abstract IEntityAspectMatchingFamily CreateAspectFamily<AspectType>();

        public abstract IEnumerable<AspectType> GetNodeList<AspectType>();

        public abstract IEnumerable<AspectType> GetUnfilteredNodeList<AspectType>();

        public abstract void RegisterEntity(IEntity e);

        public abstract void ReleaseAspectList<AspectType>();

        public abstract void UnregisterEntity(IEntity e);

        public abstract IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>();
    }
}