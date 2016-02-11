using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public class BasicEntityAspectManager : BaseEntityAspectManager
    {
        Dictionary<string, IEntityAspectMatchingFamily> _aspectFamilies = new Dictionary<string, IEntityAspectMatchingFamily>();
        public override void ComponentAddedToEntity(IEntity e, IComponent component)
        {
            throw new NotImplementedException();
        }

        public override void ComponentRemovedFromEntity(IEntity e, IComponent component)
        {
            foreach(var kvp in _aspectFamilies)
            {
                kvp.Value.ComponentRemovedFromEntity(e, component.GetType().Name);
            }
        }

        public override IEntityAspectMatchingFamily CreateAspectFamily<AspectType>()
        {
            throw new NotImplementedException();
        }

        public override IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IAspect> GetAspectList<AspectType>() 
        {
            var aspectType = typeof(AspectType).Name;
            IEnumerable<IAspect> nodeList;
            if (_aspectFamilies.ContainsKey(aspectType))
            {
                nodeList = _aspectFamilies[aspectType].ActiveAspectList;
            }
            else {
                var aspectFamily = CreateAspectFamily<AspectType>();
                aspectFamily.Init();
                _aspectFamilies[aspectType] = aspectFamily;
                foreach (var e in GameManager.Entities)
                {
                    aspectFamily.NewEntity(e);
                }
                nodeList = aspectFamily.ActiveAspectList;
            }
            return nodeList;

        }
        

        public override IEnumerable<IAspect> GetUnfilteredAspectList<AspectType>()
        {
            var type = typeof(AspectType).Name;
            if (!_aspectFamilies.ContainsKey(type))
                throw new ApplicationException("Unable to retrieve unfiltered aspect list until AspectType is registered using GetNodeList");
            return _aspectFamilies[type].EntireAspectList;
        }

        public override void RegisterEntity(IEntity e)
        {
            foreach(var kvp in _aspectFamilies)
            {
                kvp.Value.NewEntity(e);
            }
        }

        public override void ReleaseAspectList<AspectType>()
        {
            var type = typeof(AspectType).Name;
            if (_aspectFamilies.ContainsKey(type))
            {
                var aspectFamily = _aspectFamilies[typeof(AspectType).Name];
                aspectFamily.CleanUp();
                _aspectFamilies.Remove(type);
            }
            else
                throw new ApplicationException("Aspect Family does not exist for type: " + type);
        }

        public override void UnregisterEntity(IEntity e)
        {
            foreach(var kvp in _aspectFamilies)
            {
                kvp.Value.RemoveEntity(e);
            }
        }
       
    }
}