using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class RotationComponent : BaseComponent
    {
        public float Factor { get; set; }

        public override IComponent Clone()
        {
            return new RotationComponent {Factor = Factor};
        }

        public override void Reset()
        {
            Factor = 0.0f;
        }
    }
}