using Microsoft.Xna.Framework;
using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class PositionComponent : BaseComponent
    {
        

        public Vector2 CurrentPosition { get; set; }

        public override IComponent Clone()
        {
            return new PositionComponent() { CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y) };
        }

        public override void Reset()
        {
            CurrentPosition = Vector2.Zero;
        }
    }
}
