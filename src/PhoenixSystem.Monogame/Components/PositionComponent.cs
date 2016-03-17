using Microsoft.Xna.Framework;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class PositionComponent : BaseComponent
    {
        public Vector2 CurrentPosition { get; set; }

        public override IComponent Clone()
        {
            return new PositionComponent {CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y)};
        }

        public override void Reset()
        {
            CurrentPosition = Vector2.Zero;
        }
    }
}