using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixSystem.Engine
{
    //TODO: Channel mechanics
    public class AspectManager : IAspectManager
    {
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
            _availableAspects.AddLast(aspect);

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

        public IAspect Get<TAspectType>(IEntity e) where TAspectType : IAspect, new()
        {
            IAspect aspect;

            if (_availableAspects.Count <= 0)
            {
                aspect = new TAspectType();
            }
            else
            {
                aspect = _availableAspects.First.Value;
                aspect.Reset();
                _availableAspects.RemoveFirst();
            }

            aspect.Init(e);
            aspect.Deleted += Aspect_Deleted;

            _activeAspects.AddLast(aspect);

            return aspect;
        }
    }
}