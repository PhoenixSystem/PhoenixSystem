using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class RenderLayerComponent : BaseComponent
    {
        public float Depth { get; set; } = 1.0f;

        public override IComponent Clone()
        {
            return new RenderLayerComponent {Depth = Depth};
        }

        public override void Reset()
        {
            Depth = 1.0f;
        }
    }
}