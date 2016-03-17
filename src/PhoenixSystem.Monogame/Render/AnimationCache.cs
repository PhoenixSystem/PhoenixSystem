using System.Collections.Generic;
using PhoenixSystem.Monogame.Render.Sprite;

namespace PhoenixSystem.Monogame.Render
{
    public class AnimationCache
    {
        private readonly SpriteSheet _spriteSheet;

        public AnimationCache(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public IDictionary<string, string[]> Animations { get; } = new Dictionary<string, string[]>();

        public SpriteFrame GetSpriteFrame(string animation, int frameIndex)
        {
            return _spriteSheet.Sprite(Animations[animation][frameIndex]);
        }
    }
}