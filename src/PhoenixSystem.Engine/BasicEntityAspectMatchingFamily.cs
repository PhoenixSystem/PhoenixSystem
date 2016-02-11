using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public class BasicAspectMatchingFamily<AspectType> : IEntityAspectMatchingFamily
        where AspectType : BaseAspect, new()
    {
        private readonly AspectManager<AspectType> _aspectManager = new AspectManager<AspectType>();
        private readonly IList<string> _componentTypes = new List<string>();
        private readonly Dictionary<Guid, AspectType> _entities = new Dictionary<Guid, AspectType>();

        public void CleanUp()
        {
            foreach (var kvp in _entities)
            {
                kvp.Value.Delete();
            }

            _entities.Clear();
        }

        public void ComponentAddedToEntity(IEntity e, string componentType)
        {
            if (_componentTypes.Contains(componentType) && IsMatch(e))
            {
                Add(e);
            }
        }

        public void ComponentRemovedFromEntity(IEntity e, string componentType)
        {
            if (ContainsEntity(e) && _componentTypes.Contains(componentType))
            {
                Remove(e);
            }
        }

        public IEnumerable<IAspect> ActiveAspectList => _aspectManager.ChannelAspects;

        public IEnumerable<IAspect> EntireAspectList => _aspectManager.ActiveAspects;

        public void Init()
        {
            var n = new AspectType();

            foreach (var c in n.Components)
            {
                _componentTypes.Add(c.Key);
            }
        }

        public void NewEntity(IEntity e)
        {
            if (!ContainsEntity(e) && IsMatch(e))
            {
                Add(e);
            }
        }

        public void RemoveEntity(IEntity e)
        {
            if (ContainsEntity(e))
            {
                Remove(e);
            }
        }

        private bool IsMatch(IEntity e)
        {
            return e.HasComponents(_componentTypes);
        }

        private bool ContainsEntity(IEntity e)
        {
            return _entities.ContainsKey(e.ID);
        }

        private void Add(IEntity entity)
        {
            var aspect = _aspectManager.Get(entity);
            _entities.Add(entity.ID, aspect);
        }

        private void Remove(IEntity entity)
        {
            var aspect = _entities[entity.ID];
            aspect.Delete();
            _entities.Remove(entity.ID);
        }
    }
}