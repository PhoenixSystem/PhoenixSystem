using System.Collections.Generic;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Aspect
{
    public interface IAspectManager
    {
        IEnumerable<IAspect> Aspects { get; }
        IEnumerable<IAspect> ChannelAspects { get; }
        IAspect Get(IEntity e);
    }
}