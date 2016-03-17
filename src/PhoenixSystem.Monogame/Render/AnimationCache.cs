using PhoenixSample.PCL.TexturePacker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Animation
{
    public class AnimationCache
    {
        private SpriteSheet _spriteSheet;

        public AnimationCache(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }
        public IDictionary<string, string[]> Animations { get; private set; } = new Dictionary<string, string[]>();

        public SpriteFrame GetSpriteFrame(string animation, int frameIndex)
        {
            return _spriteSheet.Sprite(Animations[animation][frameIndex]);
        }
    }
}
