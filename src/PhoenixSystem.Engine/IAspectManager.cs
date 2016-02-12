using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspectManager
    {
        IEnumerable<IAspect> ActiveAspects { get; }
        IEnumerable<IAspect> ChannelAspects { get; }
        int AvailableAspectCount { get; }
        void ClearCache();
        IAspect Get<TAspectType>(IEntity e) where TAspectType : IAspect, new();
    }
}