using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Tests.Objects
{
    public class StringComponent : BaseComponent
    {
        public string Value { get; set; } = string.Empty;

        public override IComponent Clone()
        {
            return new StringComponent {Value = Value};
        }

        public override void Reset()
        {
            Value = string.Empty;
        }
    }
}