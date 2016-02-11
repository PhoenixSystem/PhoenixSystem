using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectManager
    {
        IGameManager GameManager { get; set; }
        IEnumerable<IAspect> GetNodeList<AspectType>() where AspectType : BaseAspect, new();;
        IEnumerable<IAspect> GetUnfilteredNodeList<AspectType>() where AspectType : BaseAspect, new();;
        void RegisterEntity(IEntity e);
        void UnregisterEntity(IEntity e);
        void ComponentRemovedFromEntity(IEntity e, IComponent component);
        void ComponentAddedToEntity(IEntity e, IComponent component);
        void ReleaseAspectList<AspectType>();      
        IEntityAspectMatchingFamily CreateAspectFamily<AspectType>() where AspectType : BaseAspect, new();;
    }
}
