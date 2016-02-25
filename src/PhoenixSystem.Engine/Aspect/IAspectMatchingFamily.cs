using System.Collections.Generic;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Aspect
{
    public interface IAspectMatchingFamily
    {
        IEnumerable<IAspect> ActiveAspectList { get; }
        IEnumerable<IAspect> EntireAspectList { get; }
        void Init();
        void NewEntity(IEntity entity);
        void RemoveEntity(IEntity entity);
        void ComponentAddedToEntity(IEntity entity, string componentType);
        void ComponentRemovedFromEntity(IEntity entity, string componentType);
        void CleanUp();
    }
}