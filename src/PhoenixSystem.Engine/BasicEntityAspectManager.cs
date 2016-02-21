using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public sealed class BasicEntityAspectManager : BaseEntityAspectManager
    {
        private readonly Dictionary<string, IAspectMatchingFamily> _aspectFamilies = new Dictionary<string, IAspectMatchingFamily>();
        private readonly IChannelManager _channelManager;
        private readonly IEntityManager _entityManager;
        public BasicEntityAspectManager(IChannelManager channelManager, IEntityManager entityManager)
        {
            _channelManager = channelManager;
            _entityManager = entityManager;
        }

        public override void ComponentAddedToEntity(IEntity e, IComponent component)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentAddedToEntity(e, component.GetType().Name);
            }
        }

        public override void ComponentRemovedFromEntity(IEntity e, IComponent component)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentRemovedFromEntity(e, component.GetType().Name);
            }
        }
        
        public override IEnumerable<TAspectType> GetAspectList<TAspectType>()
        {
            var aspectType = typeof (TAspectType).Name;

            if (_aspectFamilies.ContainsKey(aspectType))
            {
                return (IEnumerable<TAspectType>)_aspectFamilies[aspectType].ActiveAspectList;
            }

            var aspectFamily = new BasicAspectMatchingFamily<TAspectType>(_channelManager);

            aspectFamily.Init();

            _aspectFamilies[aspectType] = aspectFamily;

            foreach (var kvp in _entityManager.Entities)
            {
                aspectFamily.NewEntity(kvp.Value);
            }

            return (IEnumerable<TAspectType>)aspectFamily.ActiveAspectList;
        }

        public override IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>()
        {
            var type = typeof (TAspectType).Name;

            if (!_aspectFamilies.ContainsKey(type))
            {
                throw new ApplicationException("Unable to retrieve unfiltered aspect list until AspectType is registered using GetNodeList");
            }

            return (IEnumerable<TAspectType>)_aspectFamilies[type].EntireAspectList;
        }

        public override void RegisterEntity(IEntity e)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.NewEntity(e);
            }
        }

        public override void ReleaseAspectList<TAspectType>()
        {
            var type = typeof (TAspectType).Name;

            if (!_aspectFamilies.ContainsKey(type))
            {
                throw new ApplicationException("Aspect Family does not exist for type: " + type);
            }

            var aspectFamily = _aspectFamilies[type];

            aspectFamily.CleanUp();

            _aspectFamilies.Remove(type);
        }

        public override void UnregisterEntity(IEntity e)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.RemoveEntity(e);
            }
        }
    }
}