using Microsoft.Xna.Framework;
using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL.Monogame.Components
{
    public class VelocityComponent : BaseComponent
    {
        public Vector2 Speed { get; set; }
        public Vector2 Direction { get; set; }

        public override IComponent Clone()
        {
            return new VelocityComponent()
            {
                Direction = new Vector2(Direction.X, Direction.Y),
                Speed = new Vector2(Direction.X, Direction.Y)
            };
        }

        public override void Reset()
        {
            Speed = Direction = Vector2.Zero;
        }
    }
}
