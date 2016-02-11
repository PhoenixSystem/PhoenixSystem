using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectMatchingFamily
    {
        void Init();
        IEnumerable<BaseAspect> GetAspectList();
        IEnumerable<BaseAspect> GetEntireAspectList();
        void NewEntity(Entity e);
        void RemoveEntity(Entity e);
        //        CleanUp: cleanUp
        //        ComponentRemovedFromEntity: componentRemovedFromEntity,

        //ComponentAddedToEntity: componentAddedToEntity,
        //void ComponentAddedToEntity<ComponentType>(Entity e) : 
    }
}