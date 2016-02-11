using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspectManager<AspectType> where AspectType : BaseAspect, new()
    {
        IEnumerable<AspectType> ActiveAspects { get; }
        IEnumerable<AspectType> ChannelAspects { get; }
        int AvailableAspectCount { get; }
        void ClearCache();
        AspectType Get(Entity e);
    }
}