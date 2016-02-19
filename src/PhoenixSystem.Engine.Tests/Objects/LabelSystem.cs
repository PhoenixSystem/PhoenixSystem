using System;
using System.Collections.Generic;


namespace PhoenixSystem.Engine.Tests.Objects
{
    public class LabelSystem : BaseSystem
    {

        public LabelSystem(IChannelManager channelManager, int priority, IEnumerable<string> channels = null) : base(channelManager, priority, channels)
        {
        }

        private IEnumerable<LabelAspect> _labelAspects;
        public override void AddToGameManager(IGameManager gameManager)
        {
            _labelAspects = gameManager.GetAspectList<LabelAspect>();
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
            throw new NotImplementedException();
        }

        public override void Update(ITickEvent tickEvent)
        {
            throw new NotImplementedException();
        }


    }

}