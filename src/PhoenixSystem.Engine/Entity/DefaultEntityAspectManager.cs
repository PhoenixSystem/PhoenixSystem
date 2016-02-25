using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Entity
{
    public sealed class DefaultEntityAspectManager : BaseEntityAspectManager
    {
        private readonly IDictionary<string, IAspectMatchingFamily> _aspectFamilies = new Dictionary<string, IAspectMatchingFamily>();
        private readonly IChannelManager _channelManager;
        private readonly IEntityManager _entityManager;

        public DefaultEntityAspectManager(IChannelManager channelManager, IEntityManager entityManager)
        {
            _channelManager = channelManager;
            _entityManager = entityManager;
        }

        public override void ComponentAddedToEntity(IEntity entity, IComponent component)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentAddedToEntity(entity, component.GetType().Name);
            }
        }

        public override void ComponentRemovedFromEntity(IEntity entity, IComponent component)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentRemovedFromEntity(entity, component.GetType().Name);
            }
        }
        
        public override IEnumerable<TAspectType> GetAspectList<TAspectType>()
        {
            var aspectType = typeof (TAspectType).Name;

            if (_aspectFamilies.ContainsKey(aspectType))
            {
                return _aspectFamilies[aspectType].ActiveAspectList.Cast<TAspectType>();
            }

            var aspectFamily = new DefaultAspectMatchingFamily<TAspectType>(_channelManager);

            aspectFamily.Init();

            _aspectFamilies[aspectType] = aspectFamily;

            foreach (var kvp in _entityManager.Entities)
            {
                aspectFamily.NewEntity(kvp.Value);
            }

            return aspectFamily.ActiveAspectList.Cast<TAspectType>();
        }

        public override IEnumerable<TAspectType> GetUnfilteredAspectList<TAspectType>()
        {
            var type = typeof (TAspectType).Name;

            if (!_aspectFamilies.ContainsKey(type))
            {
                throw new ApplicationException("Unable to retrieve unfiltered aspect list until AspectType is registered using GetNodeList");
            }

            return _aspectFamilies[type].EntireAspectList.Cast<TAspectType>();
        }

        public override void RegisterEntity(IEntity entity)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.NewEntity(entity);
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

        public override void UnregisterEntity(IEntity entity)
        {
            foreach (var kvp in _aspectFamilies)
            {
                kvp.Value.RemoveEntity(entity);
            }
        }
    }
}