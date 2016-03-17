using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class StringComponent : BaseComponent
    {
        public StringComponent()
        {

        }

        public string Text { get; set; } = String.Empty;

        public override IComponent Clone()
        {
            return new StringComponent() { Text = this.Text };
        }


        public override void Reset()
        {
            Text = string.Empty;
        }
    }
}
