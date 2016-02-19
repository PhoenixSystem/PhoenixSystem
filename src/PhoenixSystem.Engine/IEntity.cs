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
        IEntity Clone();
        bool HasComponent(Type componentType);
        bool HasComponent(string componentTypeName);
        bool HasComponents(IEnumerable<Type> types);
        bool HasComponents(IEnumerable<string> types);
        string Name { get; set; }
        IList<string> Channels { get; }
        bool IsInChannel(string channelname);

        IEntity AddComponent(IComponent component, bool shouldOverwrite = false);
        event EventHandler<ComponentChangedEventArgs> ComponentAdded;
        event EventHandler<ComponentChangedEventArgs> ComponentRemoved;
        bool RemoveComponent(Type componentType);

        bool RemoveComponent(string componentType);
        
    }

    public static class IEntityHelpers
    {
        public static IComponent GetComponent<ComponentType>(this IEntity entity)
        {
            var componentTypeName = typeof(ComponentType).Name;
            if (!entity.HasComponent(componentTypeName))
                throw new ArgumentException("entity does not have component of type " + componentTypeName);
            return entity.Components[componentTypeName];
        }

        public static bool HasComponent<ComponentType>(this IEntity entity)
        {
            return entity.HasComponent(typeof(ComponentType));
        }

        public static bool RemoveComponent<ComponentType>(this IEntity entity)
        {
            return entity.RemoveComponent(typeof(ComponentType));
        }
    }
}