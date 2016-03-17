using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class StringComponent : BaseComponent
    {
        public string Text { get; set; } = string.Empty;

        public override IComponent Clone()
        {
            return new StringComponent {Text = Text};
        }


        public override void Reset()
        {
            Text = string.Empty;
        }
    }
}