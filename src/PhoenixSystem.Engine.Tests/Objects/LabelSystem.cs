using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;

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
			foreach(var labelAspect in _labelAspects)
				Console.WriteLine("Label Aspect: " + labelAspect.ID.ToString() + " exists");
        }
    }
}