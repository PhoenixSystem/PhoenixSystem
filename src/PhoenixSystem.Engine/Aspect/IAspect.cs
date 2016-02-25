using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Component;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Aspect
{
    public interface IAspect : IChannelFilterable
    {
        Guid ID { get; }
        IDictionary<string, IComponent> Components { get; }
        event EventHandler Deleted;
        void Delete();
        void Reset();
        void Init(IEntity e);        
    }
}