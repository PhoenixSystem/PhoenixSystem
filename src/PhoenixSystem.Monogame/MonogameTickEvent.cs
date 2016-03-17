using Microsoft.Xna.Framework;
using PhoenixSystem.Engine;

namespace PhoenixSystem.Monogame
{
    public class MonogameTickEvent : GameTime, ITickEvent
    {
        public MonogameTickEvent(GameTime gameTime)
        {
            TotalGameTime = gameTime.TotalGameTime;
            ElapsedGameTime = gameTime.ElapsedGameTime;
            IsRunningSlowly = gameTime.IsRunningSlowly;
        }
    }
}