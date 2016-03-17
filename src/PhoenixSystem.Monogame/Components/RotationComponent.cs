using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL
{
    public class RotationComponent : BaseComponent
    {
        public float Factor { get; set; }
        public override IComponent Clone()
        {
            return new RotationComponent() { Factor = this.Factor };
        }

        public override void Reset()
        {
            this.Factor = 0.0f;
        }
    }
}
