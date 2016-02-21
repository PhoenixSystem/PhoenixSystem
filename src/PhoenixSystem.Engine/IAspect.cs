using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspect :IChannelFilterable
    {
        Guid ID { get; }
        Dictionary<string, IComponent> Components { get; }
        void Delete();
        void Reset();
        void Init(IEntity e, IEnumerable<string> channels = null);
        event EventHandler Deleted;
    }
}