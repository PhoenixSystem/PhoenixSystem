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
            AddChannels(channels);
        }

        public event EventHandler Deleted;
        public event EventHandler<ComponentAddedEventArgs> ComponentAdded;
        public event EventHandler<ComponentRemovedEventArgs> ComponentRemoved;

        public Guid ID { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IList<string> Channels { get; } = new List<string>();
        public IDictionary<Type, IComponent> Components { get; } = new Dictionary<Type, IComponent>();

        public void Reset()
        {
            Channels.Clear();
            IsDeleted = false;
        }        

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
                e.Components.Add(c.GetType(), c.Clone());
            }
            return e;
        }

        public IEntity AddComponent(IComponent c, bool overwriteIfExists = false)
        {
            var componentType = c.GetType();

            if (HasComponent(componentType) && !overwriteIfExists)
            {
                throw new InvalidOperationException("Component already exists on this entity");
            }

            Components[componentType] = c;
            OnComponentAdded(c);
            return this;
        }

        public bool RemoveComponent(Type componentType)
        {
            if (!HasComponent(componentType)) return false;
            var component = Components[componentType];
            Components.Remove(componentType);
            OnComponentRemoved(component);
            component = null;
            return true;
        }

        public bool HasComponent(Type componentType)
        {
            return Components.ContainsKey(componentType);
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
            ComponentAdded?.Invoke(this, new ComponentAddedEventArgs(c));
        }

        protected void OnComponentRemoved(IComponent c)
        {
            ComponentRemoved?.Invoke(this, new ComponentRemovedEventArgs(c));
        }

        private void AddChannels(string[] channels)
        {
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
    }
}