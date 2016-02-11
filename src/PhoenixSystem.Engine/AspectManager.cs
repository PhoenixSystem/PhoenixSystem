using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    //TODO: Channel mechanics
    public class AspectManager<AspectType> : IAspectManager<AspectType> where AspectType : BaseAspect, new()
    {
        private readonly LinkedList<AspectType> _activeAspects = new LinkedList<AspectType>();
        private readonly LinkedList<AspectType> _availableAspects = new LinkedList<AspectType>();
        private readonly LinkedList<AspectType> _channelAspects = new LinkedList<AspectType>();

        public int AvailableAspectCount => _availableAspects.Count;

        public IEnumerable<AspectType> ActiveAspects => _activeAspects;

        public IEnumerable<AspectType> ChannelAspects => _channelAspects;
               

        public AspectType Get(Entity e)
        {
            AspectType aspect;

            if (_availableAspects.Count > 0)
            {
                aspect = _availableAspects.First.Value;
                aspect.Reset();
                _availableAspects.RemoveFirst();
            }
            else
            {
                aspect = new AspectType();
            }

            aspect.Init(e);
            aspect.Deleted += Aspect_Deleted;

            _activeAspects.AddLast(aspect);

            return aspect;
        }

        protected virtual void Aspect_Deleted(object sender, EventArgs e)
        {
            var aspect = (AspectType) sender;

            aspect.Deleted -= Aspect_Deleted;
            _availableAspects.AddLast(aspect);

            _activeAspects.Remove(aspect);
        }

        private void applyChannelFilter(string channel)
        {
            _channelAspects.Clear();
            foreach(var aspect in _activeAspects)
            {
                if (aspect.IsInChannel(channel) || aspect.IsInChannel("all"))
                    _channelAspects.AddLast(aspect);
            }
        }

        public void ClearCache()
        {
            _availableAspects.Clear();
        }       
    }
}