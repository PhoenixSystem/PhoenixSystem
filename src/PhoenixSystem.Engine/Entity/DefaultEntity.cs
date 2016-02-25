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
        public Dictionary<string, IComponent> Components { get; } = new Dictionary<string, IComponent>();

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

        public virtual void OnDeleted()
        {
            Deleted?.Invoke(this, null);
        }

        #region -- Add Component --

        public event EventHandler<ComponentChangedEventArgs> ComponentAdded;

        protected void OnComponentAdded(IComponent c)
        {
            ComponentAdded?.Invoke(this, new ComponentChangedEventArgs(c));
        }

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

        #endregion

        #region -- Remove Component -- 

        public event EventHandler<ComponentChangedEventArgs> ComponentRemoved;

        protected void OnComponentRemoved(IComponent c)
        {
            ComponentRemoved?.Invoke(this, new ComponentChangedEventArgs(c));
        }

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

        #endregion

        #region -- Has Component --

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

        #endregion -- Has Component --
    }
}