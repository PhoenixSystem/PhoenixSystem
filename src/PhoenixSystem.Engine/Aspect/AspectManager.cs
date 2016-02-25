using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Extensions;

namespace PhoenixSystem.Engine.Aspect
{
    public class AspectManager : IAspectManager
    {
        private readonly LinkedList<IAspect> _aspects = new LinkedList<IAspect>();
        private readonly LinkedList<IAspect> _channelAspects = new LinkedList<IAspect>();
        private readonly IObjectPool<IAspect> _aspectPool;
        private readonly IChannelManager _channelManager;

        public AspectManager(IChannelManager channelManager, IObjectPool<IAspect> aspectPool)
        {
            _aspectPool = aspectPool;
            _channelManager = channelManager;
        }

        public IEnumerable<IAspect> Aspects => _aspects;

        public IEnumerable<IAspect> ChannelAspects => _channelAspects;

        public IAspect Get(IEntity entity)
        {
            var aspect = _aspectPool.Get();

            aspect.Init(entity);
            aspect.Deleted += AspectDeleted;

            if (aspect.IsInChannel(_channelManager.Channel, "all"))
            {
                _channelAspects.AddLast(aspect);
            }

            _aspects.AddLast(aspect);

            return aspect;
        }

        protected virtual void AspectDeleted(object sender, EventArgs e)
        {
            var aspect = sender as IAspect;

            if (aspect == null) return;

            aspect.Deleted -= AspectDeleted;

            if (aspect.IsInChannel(_channelManager.Channel, "all"))
            {
                _channelAspects.Remove(aspect);
            }

            _aspects.Remove(aspect);
            _aspectPool.Put(aspect);
        }
    }
}