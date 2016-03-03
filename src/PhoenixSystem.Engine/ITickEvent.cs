using System;

namespace PhoenixSystem.Engine
{
    public interface ITickEvent
    {
        TimeSpan ElapsedGameTime { get; set; }
        bool IsRunningSlowly { get; set; }
        TimeSpan TotalGameTime { get; set; }
    }
}