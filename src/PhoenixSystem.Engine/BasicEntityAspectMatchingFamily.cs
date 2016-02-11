using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public class BasicAspectMatchingFamily<AspectType> : IEntityAspectMatchingFamily where AspectType : BaseAspect, new()
    {
        AspectManager<AspectType> _aspectManager = new AspectManager<AspectType>();
        IEnumerable<string> _componentTypes = null;
        Dictionary<Guid, AspectType> _entities = new Dictionary<Guid, AspectType>();

        private void add(Entity entity)
        {
            var aspect = _aspectManager.Get(entity);
            _entities.Add(entity.ID, aspect);
        }

        private void remove(Entity entity)
        {
            var aspect = _entities[entity.ID];
            aspect.Delete();
            _entities.Remove(entity.ID);
        }

        public BasicAspectMatchingFamily()
        {
            
        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }

        public void ComponentAddedToEntity(Entity e, string componentType)
        {
            throw new NotImplementedException();
        }

        public void ComponentRemovedFromEntity(Entity e, string componentType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseAspect> GetAspectList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseAspect> GetEntireAspectList()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void NewEntity(Entity e)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntity(Entity e)
        {
            throw new NotImplementedException();
        }
    }
}
