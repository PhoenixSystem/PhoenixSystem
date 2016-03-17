using System;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Monogame.Components
{
    public class ScaleComponent : BaseComponent
    {
        public float Factor { get; set; } = 1.0f;

        public override IComponent Clone()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}