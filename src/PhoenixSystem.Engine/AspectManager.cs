using System;
using System.Collections.Generic;
using System.Linq;

using PhoenixSystem.Engine.Collections;

namespace PhoenixSystem.Engine
{
    public class AspectManager<AspectType> : IAspectManager<AspectType> where AspectType: IAspect, new()
    {
        private ObjectPool<IAspect> _aspectPool;
        private IChannelManager _channelManager;

        public AspectManager(IChannelManager channelManager)
        {
            _aspectPool = new ObjectPool<IAspect>(() => new AspectType(), (a) => a.Reset());
            _channelManager = channelManager;
        }

        private readonly LinkedList<IAspect> _activeAspects = new LinkedList<IAspect>();
        private readonly LinkedList<IAspect> _availableAspects = new LinkedList<IAspect>();
        private readonly LinkedList<IAspect> _channelAspects = new LinkedList<IAspect>();

        public int AvailableAspectCount => _availableAspects.Count;

        public IEnumerable<IAspect> ActiveAspects => _activeAspects;

        public IEnumerable<IAspect> ChannelAspects => _channelAspects;              

        protected virtual void Aspect_Deleted(object sender, EventArgs e)
        {
            var aspect = (IAspect) sender;

            aspect.Deleted -= Aspect_Deleted;
            
            _aspectPool.Put(aspect);
            _activeAspects.Remove(aspect);
        }

        private void ApplyChannelFilter(string channel)
        {
            _channelAspects.Clear();
            foreach (var aspect in _activeAspects.Where(aspect => aspect.IsInChannel(channel) || aspect.IsInChannel("all")))
            {
                _channelAspects.AddLast(aspect);
            }
        }

        public void ClearCache()
        {
            _availableAspects.Clear();
        }

        public IAspect Get(IEntity e)
        {
            IAspect aspect = _aspectPool.Get();
            
            aspect.Init(e);
            aspect.Deleted += Aspect_Deleted;

            _activeAspects.AddLast(aspect);

            return aspect;
        }
    }
}