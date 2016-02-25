using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Extensions;

namespace PhoenixSystem.Engine
{
    public class AspectManager<TAspectType> : IAspectManager where TAspectType : IAspect, new()
    {
        private readonly LinkedList<IAspect> _aspects = new LinkedList<IAspect>();
        private readonly ObjectPool<IAspect> _aspectPool;
        private readonly LinkedList<IAspect> _channelAspects = new LinkedList<IAspect>();
        private readonly IChannelManager _channelManager;

        public AspectManager(IChannelManager channelManager)
        {
            _aspectPool = new ObjectPool<IAspect>(() => new TAspectType(), a => a.Reset());
            _channelManager = channelManager;
        }

        public IEnumerable<IAspect> Aspects => _aspects;

        public IEnumerable<IAspect> ChannelAspects => _channelAspects;

        public IAspect Get(IEntity e)
        {
            var aspect = _aspectPool.Get();

            aspect.Init(e);
            aspect.Deleted += Aspect_Deleted;

            if (aspect.IsInChannel(_channelManager.Channel, "all"))
            {
                _channelAspects.AddLast(aspect);
            }

            _aspects.AddLast(aspect);

            return aspect;
        }

        protected virtual void Aspect_Deleted(object sender, EventArgs e)
        {
            var aspect = (IAspect) sender;

            aspect.Deleted -= Aspect_Deleted;

            if (aspect.IsInChannel(_channelManager.Channel, "all"))
            {
                _channelAspects.Remove(aspect);
            }

            _aspects.Remove(aspect);
            _aspectPool.Put(aspect);
            
            
        }
    }
}