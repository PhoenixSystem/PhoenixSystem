using Microsoft.Xna.Framework;
using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class ColorComponent : BaseComponent
    {
        public Color? Color { get; set; } = null;
        public override IComponent Clone()
        {
            return new ColorComponent() { Color = this.Color };
        }

        public override void Reset()
        {
            Color = null;
        }
    }
}
