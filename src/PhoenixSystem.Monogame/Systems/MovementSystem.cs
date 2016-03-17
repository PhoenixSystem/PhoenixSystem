using System.Collections.Generic;
using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Extensions;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;
using PhoenixSystem.Monogame.Aspects;
using PhoenixSystem.Monogame.Components;

namespace PhoenixSystem.Monogame.Systems
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
            foreach (var aspect in _aspects)
            {
                var pos = aspect.GetComponent<PositionComponent>();
                var velo = aspect.GetComponent<VelocityComponent>();

                pos.CurrentPosition += velo.Direction*velo.Speed*(float) tickEvent.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}