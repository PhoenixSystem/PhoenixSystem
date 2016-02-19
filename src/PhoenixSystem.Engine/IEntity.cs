using PhoenixSystem.Engine.Events;
using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntity
    {
        Guid ID { get; }
        bool IsDeleted { get; set; }
        void Delete();
        Dictionary<string, IComponent> Components { get; }
        event EventHandler Deleted;

        bool HasComponent(Type componentType);
        bool HasComponent(string componentTypeName);
        bool HasComponents(IEnumerable<Type> types);
        bool HasComponents(IEnumerable<string> types);
        string Name { get; set; }
        IList<string> Channels { get; }

        IEntity AddComponent(IComponent component, bool shouldOverwrite = false);
        event EventHandler<ComponentChangedEventArgs> ComponentAdded;
        event EventHandler<ComponentChangedEventArgs> ComponentRemoved;
    }
}