using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixSystem.Engine
{
    public class Entity : IEntity
    {
        public Guid ID { get; private set; }

        public string Name { get; set; }

        public Entity()
        {
            this.ID = Guid.NewGuid();
        }
        public bool IsDeleted { get; private set; }

        public IList<string> Channels { get; } = new List<string>();

        public Dictionary<string, BaseComponent> Components { get; } = new Dictionary<string, BaseComponent>();

        public event EventHandler Deleted;

        public virtual void OnDeleted()
        {
            Deleted?.Invoke(this, null);
        }

        public void Delete()
        {
            IsDeleted = true;
            OnDeleted();
        }

        public Entity Clone()
        {
            var e = new Entity();
            //TODO: implement this
            return e;
        }

        public bool IsInChannel(string channel)
        {
            return Channels.Contains(channel);
        }

        #region -- Add Component --

        public event EventHandler<ComponentChangedEventArgs> ComponentAdded;

        protected void OnComponentAdded(BaseComponent c)
        {
            ComponentAdded?.Invoke(this, new ComponentChangedEventArgs {Component = c});
        }

        public Entity AddComponent(BaseComponent c, bool overwriteIfExists = false)
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

        protected void OnComponentRemoved(BaseComponent c)
        {
            ComponentRemoved?.Invoke(this, new ComponentChangedEventArgs {Component = c});
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

    public class ComponentChangedEventArgs : EventArgs
    {
        public BaseComponent Component { get; set; }
    }
}