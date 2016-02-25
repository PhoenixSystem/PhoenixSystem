using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Tests.Objects
{
    public class XYComponent : BaseComponent
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override IComponent Clone()
        {
            return new XYComponent
            {
                X = X,
                Y = Y
            };
        }

        public override void Reset()
        {
            X = 0;
            Y = 0;
        }
    }
}