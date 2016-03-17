using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class SizeComponent : BaseComponent
    {
        public float Height { get; set; } = 1.0f;
        public float Width { get; set; } = 1.0f;

        public override IComponent Clone()
        {
            return new SizeComponent() { Height = this.Height, Width = this.Width };
        }

        public override void Reset()
        {
            Height = 1.0f;
            Width = 1.0f;
        }
    }
}
