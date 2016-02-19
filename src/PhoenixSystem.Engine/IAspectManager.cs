using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspectManager<TAspectType> where TAspectType : IAspect, new()
    {
        IEnumerable<IAspect> ActiveAspects { get; }
        IEnumerable<IAspect> ChannelAspects { get; }
        int AvailableAspectCount { get; }
        void ClearCache();
        IAspect Get(IEntity e);
    }
}