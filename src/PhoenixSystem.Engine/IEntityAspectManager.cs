using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectManager<AspectMatchingFamily> where AspectMatchingFamily : IEntityAspectMatchingFamily
    {
        IGameManager GameManager { get; set; }
        IEnumerable<AspectType> GetNodeList<AspectType>();
        IEnumerable<AspectType> GetUnfilteredNodeList<AspectType>();
        void RegisterEntity(IEntity e);
        void UnregisterEntity(IEntity e);
        void ComponentRemovedFromEntity(IEntity e, IComponent component);
        void ComponentAddedToEntity(IEntity e, IComponent component);
        void ReleaseAspectList<AspectType>();
        IEntityAspectMatchingFamily CreateAspectFamily<AspectType>();
    }
}