using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectManager<AspectMatchingFamily> where AspectMatchingFamily : IEntityAspectMatchingFamily
    {
        IEnumerable<AspectType> GetNodeList<AspectType>();
        IEnumerable<AspectType> GetUnfilteredNodeList<AspectType>();
        void RegisterEntity(Entity e);
        void UnregisterEntity(Entity e);
        void ComponentRemovedFromEntity(Entity e, BaseComponent component);
        void ComponentAddedToEntity(Entity e, BaseComponent component);
        void ReleaseAspectList<AspectType>();      
        
        BaseGameManager GameManager { get; set; }
        IEntityAspectMatchingFamily CreateAspectFamily<AspectType>();

    }
}
