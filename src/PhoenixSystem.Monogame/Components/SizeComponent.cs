using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class SizeComponent : BaseComponent
    {
        public float Height { get; set; } = 1.0f;
        public float Width { get; set; } = 1.0f;

        public override IComponent Clone()
        {
            return new SizeComponent {Height = Height, Width = Width};
        }

        public override void Reset()
        {
            Height = 1.0f;
            Width = 1.0f;
        }
    }
}