using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine
{
    public interface IEntity
    {
        Guid ID { get; }
        bool IsDeleted { get; set; }
        Dictionary<string, IComponent> Components { get; }
        string Name { get; set; }
        IList<string> Channels { get; }
        void Delete();
        event EventHandler Deleted;
        IEntity Clone();
        bool HasComponent(Type componentType);
        bool HasComponent(string componentTypeName);
        bool HasComponents(IEnumerable<Type> types);
        bool HasComponents(IEnumerable<string> types);
        bool IsInChannel(string channelname);
        IEntity AddComponent(IComponent component, bool shouldOverwrite = false);
        event EventHandler<ComponentChangedEventArgs> ComponentAdded;
        event EventHandler<ComponentChangedEventArgs> ComponentRemoved;
        bool RemoveComponent(Type componentType);
        bool RemoveComponent(string componentType);
    }
}