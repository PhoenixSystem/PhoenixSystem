using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspectManager<AspectType> where AspectType : IAspect, new()
    {
        IEnumerable<IAspect> ActiveAspects { get; }
        IEnumerable<IAspect> ChannelAspects { get; }
        int AvailableAspectCount { get; }
        void ClearCache();
        IAspect Get(IEntity e);
    }
}