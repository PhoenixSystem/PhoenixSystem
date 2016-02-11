using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectMatchingFamily
    {
        void Init();
        IEnumerable<BaseAspect> ActiveAspectList { get; }
        IEnumerable<BaseAspect> EntireAspectList { get; }
        void NewEntity(Entity e);
        void RemoveEntity(Entity e);
        void ComponentAddedToEntity(Entity e, string componentType);
        void ComponentRemovedFromEntity(Entity e, string componentType);
        void CleanUp();

    }
}
