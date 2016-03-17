using Microsoft.Xna.Framework;
using PhoenixSystem.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL
{
    public class MonogameTickEvent : GameTime, ITickEvent
    {
        public MonogameTickEvent(GameTime gameTime) : base()
        {
            TotalGameTime = gameTime.TotalGameTime;
            ElapsedGameTime = gameTime.ElapsedGameTime;
            IsRunningSlowly = gameTime.IsRunningSlowly;
            
        }
    }
}
