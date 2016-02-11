using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public interface IEntityAspectMatchingFamily
    {
        void Init();
        IEnumerable<BaseAspect> GetAspectList();
        IEnumerable<BaseAspect> GetEntireAspectList();
        void NewEntity(Entity e);
        void RemoveEntity(Entity e);
        void ComponentAddedToEntity(Entity e, string componentType);
        void ComponentRemovedFromEntity(Entity e, string componentType);
        void CleanUp();

    }
}
