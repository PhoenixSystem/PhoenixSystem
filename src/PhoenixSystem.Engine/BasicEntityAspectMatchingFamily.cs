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
        IList<string> _componentTypes = new List<string>();
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
            foreach(var kvp in _entities)
            {
                kvp.Value.Delete();
            }
            _entities.Clear();
        }

        public void ComponentAddedToEntity(Entity e, string componentType)
        {
            if(_componentTypes.Contains(componentType) && isMatch(e))
            {
                add(e);
            }
        }

        private bool isMatch(Entity e)
        {
            return e.HasComponents(_componentTypes);
        }

        public void ComponentRemovedFromEntity(Entity e, string componentType)
        {
            if(containsEntity(e) && _componentTypes.Contains(componentType))
            {
                remove(e);
            }
        }

        private bool containsEntity(Entity e)
        {
            return _entities.ContainsKey(e.ID);
        }

        public IEnumerable<BaseAspect> ActiveAspectList => _aspectManager.ChannelAspects;
        
        public IEnumerable<BaseAspect> EntireAspectList => _aspectManager.ActiveAspects;
        
        public void Init()
        {
            var n = new AspectType();
            foreach(var c in n.Components)
            {
                _componentTypes.Add(c.Key);
            }
        }

        public void NewEntity(Entity e)
        {
            if(!containsEntity(e) && isMatch(e))
            {
                add(e);
            }
        }

        public void RemoveEntity(Entity e)
        {
            if(containsEntity(e))
            {
                remove(e);
            }
        }
    }
}
