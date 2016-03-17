using System.Collections.Generic;

namespace PhoenixSystem.Monogame.Render.Sprite
{
    public class SpriteSheet
    {
        private readonly IDictionary<string, SpriteFrame> _spriteList;

        public SpriteSheet()
        {
            _spriteList = new Dictionary<string, SpriteFrame>();
        }

        public void Add(string name, SpriteFrame sprite)
        {
            _spriteList.Add(name, sprite);
        }

        public void Add(SpriteSheet otherSheet)
        {
            foreach (var sprite in otherSheet._spriteList)
            {
                _spriteList.Add(sprite);
            }
        }

        public SpriteFrame Sprite(string sprite)
        {
            return _spriteList[sprite];
        }
    }
}