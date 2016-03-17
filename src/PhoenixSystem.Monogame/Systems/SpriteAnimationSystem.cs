using System.Collections.Generic;
using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Extensions;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;
using PhoenixSystem.Monogame.Aspects;
using PhoenixSystem.Monogame.Components;
using PhoenixSystem.Monogame.Render;

namespace PhoenixSystem.Monogame.Systems
{
    public class SpriteAnimationSystem : BaseSystem
    {
        private readonly AnimationCache _animationCache;
        private IEnumerable<SpriteAnimationAspect> _aspects;

        public SpriteAnimationSystem(AnimationCache animationCache, IChannelManager channelManager, int priority,
            params string[] channels) : base(channelManager, priority, channels)
        {
            _animationCache = animationCache;
        }

        public override void AddToGameManager(IGameManager gameManager)
        {
            _aspects = gameManager.GetAspectList<SpriteAnimationAspect>();
        }

        public override void RemoveFromGameManager(IGameManager gameManager)
        {
        }

        public override void Update(ITickEvent tickEvent)
        {
            foreach (var aspect in _aspects)
            {
                var textureComponent = aspect.GetComponent<TextureRenderComponent>();
                var spriteAnimationComponent = aspect.GetComponent<AnimationComponent>();

                if (!spriteAnimationComponent.Active) continue;

                var frameCount = _animationCache.Animations[spriteAnimationComponent.CurrentAnimation].Length;
                spriteAnimationComponent.TimeInCurrentFrame += (float) tickEvent.ElapsedGameTime.TotalSeconds;

                if (spriteAnimationComponent.TimeInCurrentFrame < spriteAnimationComponent.TimePerFrame) continue;

                spriteAnimationComponent.CurrentFrameIndex++;
                spriteAnimationComponent.TimeInCurrentFrame -= spriteAnimationComponent.TimePerFrame;

                if (spriteAnimationComponent.CurrentFrameIndex == frameCount && spriteAnimationComponent.ShouldLoop)
                    spriteAnimationComponent.CurrentFrameIndex = 0;

                var sf = _animationCache.GetSpriteFrame(spriteAnimationComponent.CurrentAnimation,
                    spriteAnimationComponent.CurrentFrameIndex);

                textureComponent.IsRotated = sf.IsRotated;
                textureComponent.Origin = sf.Origin;
                textureComponent.SourceRect = sf.SourceRectangle;
                textureComponent.Texture = sf.Texture;
            }
        }
    }
}