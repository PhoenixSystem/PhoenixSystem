using Microsoft.Xna.Framework;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class ColorComponent : BaseComponent
    {
        public Color? Color { get; set; }

        public override IComponent Clone()
        {
            return new ColorComponent {Color = Color};
        }

        public override void Reset()
        {
            Color = null;
        }
    }
}