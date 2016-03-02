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
        IDictionary<Type, IComponent> Components { get; }
        event EventHandler Deleted;
        void Delete();
        void Reset();
        void Init(IEntity e);
    }

    public static class IAspectHelpers
    {
        public static T GetComponent<T>(this IAspect aspect) where T : class, IAspect
        {
            if (!aspect.Components.ContainsKey(typeof(T)))
                throw new InvalidOperationException("Component type " + typeof(T).Name + "not found.");
            return aspect.Components[typeof(T)] as T;
        }
    }
}