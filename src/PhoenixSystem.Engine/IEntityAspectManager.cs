using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectManager
    {
        IEnumerable<IAspect> GetNodeList<AspectType>() where AspectType: BaseAspect, new();
        IEnumerable<IAspect> GetUnfilteredNodeList<AspectType>() where AspectType : BaseAspect, new();
        void RegisterEntity(Entity e);
        void UnregisterEntity(Entity e);
        void ComponentRemovedFromEntity(Entity e, BaseComponent component);
        void ComponentAddedToEntity(Entity e, BaseComponent component);
        void ReleaseAspectList<AspectType>();      
        
        BaseGameManager GameManager { get; set; }
        IEntityAspectMatchingFamily CreateAspectFamily<AspectType>() where AspectType : BaseAspect, new();

    }
}
