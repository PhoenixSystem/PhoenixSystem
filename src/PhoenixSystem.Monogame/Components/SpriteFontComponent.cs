using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class SpriteFontComponent : BaseComponent
    {
        public SpriteFont Font { get; set; }

        public override IComponent Clone()
        {
            var tc = new SpriteFontComponent {Font = Font};
            return tc;
        }

        public override void Reset()
        {
            Font = null;
        }
    }
}