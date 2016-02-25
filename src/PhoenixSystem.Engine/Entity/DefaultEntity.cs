using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Component;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine.Entity
{
    public class DefaultEntity : IEntity
    {
        public DefaultEntity(string name = "", params string[] channels)
        {
            Name = name;

            if (channels == null) return;

            foreach (var s in channels)
            {
                if (string.IsNullOrEmpty(s))
                {
                    throw new ArgumentException("channel cannot be empty string or null");
                }

                Channels.Add(s);
            }
        }

        public Guid ID { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IList<string> Channels { get; } = new List<string>();
        public IDictionary<string, IComponent> Components { get; } = new Dictionary<string, IComponent>();

        public void Reset()
        {
            Channels.Clear();
            IsDeleted = false;
        }

        public event EventHandler Deleted;

        public void Delete()
        {
            IsDeleted = true;
            OnDeleted();
        }

        public IEntity Clone()
        {
            var e = new DefaultEntity(Name, Channels.ToArray());
            foreach (var c in Components.Values)
            {
                e.Components.Add(c.GetType().Name, c.Clone());
            }
            return e;
        }

        public event EventHandler<ComponentChangedEventArgs> ComponentAdded;

        public IEntity AddComponent(IComponent c, bool overwriteIfExists = false)
        {
            var componentType = c.GetType().Name;

            if (HasComponent(componentType) && !overwriteIfExists)
            {
                throw new ApplicationException("Component already exists on this entity");
            }

            Components[componentType] = c;
            OnComponentAdded(c);
            return this;
        }

        public event EventHandler<ComponentChangedEventArgs> ComponentRemoved;

        public bool RemoveComponent(Type componentType)
        {
            var componentTypeName = componentType.Name;
            return RemoveComponent(componentTypeName);
        }

        public bool RemoveComponent(string componentType)
        {
            if (!HasComponent(componentType)) return false;
            var component = Components[componentType];
            Components.Remove(componentType);
            OnComponentRemoved(component);
            component = null;
            return true;
        }

        public bool HasComponent(string componentTypeName)
        {
            return Components.ContainsKey(componentTypeName);
        }

        public bool HasComponent(Type componentType)
        {
            return HasComponent(componentType.Name);
        }

        public bool HasComponents(IEnumerable<string> types)
        {
            return types.All(HasComponent);
        }

        public bool HasComponents(IEnumerable<Type> types)
        {
            return types.All(HasComponent);
        }

        public virtual void OnDeleted()
        {
            Deleted?.Invoke(this, null);
        }

        protected void OnComponentAdded(IComponent c)
        {
            ComponentAdded?.Invoke(this, new ComponentChangedEventArgs(c));
        }

        protected void OnComponentRemoved(IComponent c)
        {
            ComponentRemoved?.Invoke(this, new ComponentChangedEventArgs(c));
        }
    }
}