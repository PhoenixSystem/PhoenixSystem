using PhoenixSystem.Engine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.Channel;
using PhoenixSample.PCL.Monogame.Aspects;
using PhoenixSystem.Engine.Aspect;
using PhoenixSample.PCL.Monogame.Components;
using PhoenixSystem.Engine.Extensions;

namespace PhoenixSample.PCL.Systems
{
    public class MovementSystem : BaseSystem
    {
        private IEnumerable<MovementAspect> _aspects;
        public MovementSystem(IChannelManager channelManager, int priority, string[] channels) : 
            base(channelManager, priority, channels)
        {

        }
        public override void AddToGameManager(IGameManager gameManager)
        {
            _aspects = gameManager.GetAspectList<MovementAspect>();
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
            
        }

        public override void Update(ITickEvent tickEvent)
        {
            foreach(var aspect in _aspects)
            {
                var pos = aspect.GetComponent<PositionComponent>();
                var velo = aspect.GetComponent<VelocityComponent>();

                pos.CurrentPosition += velo.Direction * velo.Speed * (float)tickEvent.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
