using Microsoft.Xna.Framework.Graphics;
using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class SpriteFontComponent : BaseComponent
    {
        public SpriteFont Font { get; set; }

        public override IComponent Clone()
        {
            var tc = new SpriteFontComponent() { Font = this.Font };
            return tc;
        }

        public override void Reset()
        {
            Font = null;
        }
    }
}
