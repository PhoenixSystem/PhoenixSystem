using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Tests.Classes
{
    class XYComponent : BaseComponent
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public override IComponent Clone()
        {
            return new XYComponent()
            {
                X = this.X,
                Y = this.Y
            };
        }

        public override void Reset()
        {
            X = 0;
            Y = 0;
        }
    }
}
