using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Aspect
{
    public class DefaultAspectMatchingFamily<TAspectType> : IAspectMatchingFamily where TAspectType : IAspect, new()
    {
        private readonly IAspectManager _aspectManager;
        private readonly IList<string> _componentTypes = new List<string>();
        private readonly IDictionary<Guid, IAspect> _entities = new Dictionary<Guid, IAspect>();

        public DefaultAspectMatchingFamily(IChannelManager channelManager)
        {
            _aspectManager = new AspectManager(channelManager, new ObjectPool(() => new TAspectType(), a => ((TAspectType) a).Reset()));
        }

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
            if (!_componentTypes.Contains(componentType) || !IsMatch(e)) return;

            Add(e);
        }

        public void ComponentRemovedFromEntity(IEntity e, string componentType)
        {
            if (!ContainsEntity(e) || !_componentTypes.Contains(componentType)) return;

            Remove(e);
        }

        public IEnumerable<IAspect> ActiveAspectList => _aspectManager.ChannelAspects;

        public IEnumerable<IAspect> EntireAspectList => _aspectManager.Aspects;

        public void Init()
        {
            var n = new TAspectType();

            foreach (var c in n.Components)
            {
                _componentTypes.Add(c.Key);
            }
        }

        public void NewEntity(IEntity e)
        {
            if (ContainsEntity(e) || !IsMatch(e)) return;

            Add(e);
        }

        public void RemoveEntity(IEntity e)
        {
            if (!ContainsEntity(e)) return;

            Remove(e);
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