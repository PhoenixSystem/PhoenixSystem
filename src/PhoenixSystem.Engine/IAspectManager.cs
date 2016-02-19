using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspectManager<TAspectType> where TAspectType : IAspect, new()
    {
        IEnumerable<IAspect> Aspects { get; }
        IEnumerable<IAspect> ChannelAspects { get; }
        IAspect Get(IEntity e);
    }
}