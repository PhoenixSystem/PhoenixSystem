using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public class BasicEntityAspectManager : BaseEntityAspectManager
    {
        Dictionary<string, IEntityAspectMatchingFamily> _aspectFamilies = new Dictionary<string, IEntityAspectMatchingFamily>();
        public override void ComponentAddedToEntity(Entity e, BaseComponent component)
        {
            throw new NotImplementedException();
        }

        public override void ComponentRemovedFromEntity(Entity e, BaseComponent component)
        {
            throw new NotImplementedException();
        }

        public override IEntityAspectMatchingFamily CreateAspectFamily<AspectType>()
        {
            throw new NotImplementedException();
        }

        public override IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IAspect> GetNodeList<AspectType>() 
        {
            var aspectType = typeof(AspectType).Name;
            IEnumerable<IAspect> nodeList;
            if (containsAspectFamily(aspectType))
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

        private bool containsAspectFamily(string aspectType)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IAspect> GetUnfilteredNodeList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public override void RegisterEntity(Entity e)
        {
            throw new NotImplementedException();
        }

        public override void ReleaseAspectList<AspectType>()
        {
            throw new NotImplementedException();
        }

        public override void UnregisterEntity(Entity e)
        {
            throw new NotImplementedException();
        }
    }
}