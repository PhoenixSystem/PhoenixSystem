using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IAspect
    {
        Guid ID { get; }
        Dictionary<string, IComponent> Components { get; }
        void Delete();
        void Reset();
        IAspect Clone();
        void Init(IEntity e, IEnumerable<string> channels = null);
        event EventHandler Deleted;
        bool IsInChannel(string channelName);
    }
}