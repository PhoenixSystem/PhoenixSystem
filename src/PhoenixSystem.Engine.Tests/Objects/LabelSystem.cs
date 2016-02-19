using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine.Tests.Objects
{
    public class LabelSystem : BaseSystem
    {
        private IEnumerable<LabelAspect> _labelAspects;

        public LabelSystem(IChannelManager channelManager, int priority, IEnumerable<string> channels = null)
            : base(channelManager, priority, channels)
        {
        }

        public override void AddToGameManager(IGameManager gameManager)
        {
            _labelAspects = gameManager.GetAspectList<LabelAspect>();
            OnAddedToGameManager(gameManager);
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
            gameManager.ReleaseAspectList<LabelAspect>();
            OnRemovedFromGameManager();
        }

        public override void Update(ITickEvent tickEvent)
        {
            Console.WriteLine("LabelSystem Update Executed");
        }
    }
}