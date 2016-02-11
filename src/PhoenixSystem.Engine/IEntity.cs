using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntity
    {
        Guid ID { get; }
        Dictionary<string, IComponent> Components { get; }
        bool HasComponent(Type componentType);
        bool HasComponent(string componentTypeName);
        bool HasComponents(IEnumerable<Type> types);
        bool HasComponents(IEnumerable<string> types);
    }
}