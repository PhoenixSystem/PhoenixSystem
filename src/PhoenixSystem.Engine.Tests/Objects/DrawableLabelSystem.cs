using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Tests.Objects
{
    public class DrawableLabelSystem : BaseSystem, IDrawableSystem
    {
        private IEnumerable<LabelAspect> _labelAspects;

        public DrawableLabelSystem(IChannelManager channelManager, int priority, IEnumerable<string> channels = null)
            : base(channelManager, priority, channels)
        {
        }

        public bool IsDrawing { get; private set; } = false;


        public override void AddToGameManager(IGameManager gameManager)
        {
            _labelAspects = gameManager.GetAspectList<LabelAspect>();
            OnAddedToGameManager(gameManager);
        }

        public void Draw(ITickEvent tickEvent)
        {
            Console.WriteLine("Drew Label Things");
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
            gameManager.ReleaseAspectList<LabelAspect>();
            OnRemovedFromGameManager();
        }

        public override void Update(ITickEvent tickEvent)
        {
            foreach (var labelAspect in _labelAspects)
                Console.WriteLine("Label Aspect: " + labelAspect.ID.ToString() + " exists");
        }
    }
}
