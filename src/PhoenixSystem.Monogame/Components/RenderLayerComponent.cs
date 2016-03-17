using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class RenderLayerComponent : BaseComponent
    {
        public float Depth { get; set; } = 1.0f;
        public override IComponent Clone()
        {
            return new RenderLayerComponent() { Depth = this.Depth };
        }

        public override void Reset()
        {
            Depth = 1.0f;
        }
    }
}
