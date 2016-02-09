using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public class Entity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; private set; }
        public event EventHandler Deleted;
        public virtual void OnDeleted()
        {
            if (this.Deleted != null)
                Deleted(this, null);
        }

        public void Delete()
        {
            this.IsDeleted = true;
            OnDeleted();
        }

        private IList<string> _Channels = new List<string>();
        public IList<string> Channels
        {
            get { return this._Channels; } 
        }

        private Dictionary<string, BaseComponent> _Components = new Dictionary<string, BaseComponent>();
        public Dictionary<string, BaseComponent> Components
        {
            get { return _Components; }
        }


        #region -- Add Component --
        public event EventHandler<ComponentChangedEventArgs> ComponentAdded;
        protected void OnComponentAdded(BaseComponent c)
        {
            if (this.ComponentAdded != null)
                ComponentAdded(this, new ComponentChangedEventArgs() { Component = c });
        }
        public Entity AddComponent(BaseComponent c, bool overwriteIfExists = false)
        {
            var componentType = c.GetType().Name;
            if (this.HasComponent(componentType) && !overwriteIfExists)
            {
                throw new ApplicationException("Component already exists on this entity");
            }

            this.Components[componentType] = c;
            this.OnComponentAdded(c);
            return this;
        }
        #endregion

        #region -- Remove Component -- 

        public event EventHandler<ComponentChangedEventArgs> ComponentRemoved;
        protected void OnComponentRemoved(BaseComponent c)
        {
            if (this.ComponentRemoved != null)
                ComponentRemoved(this, new ComponentChangedEventArgs() { Component = c });
        }

        public bool RemoveComponent(Type componentType)
        {
            var componentTypeName = componentType.Name;
            return this.RemoveComponent(componentTypeName);
        }

        public bool RemoveComponent(string componentType)
        {
            if(this.HasComponent(componentType))
            {
                var component = _Components[componentType];
                _Components.Remove(componentType);
                OnComponentRemoved(component);
                component = null;
                return true;
            }
            return false;
        }

        #endregion

        #region -- Has Component --

        public bool HasComponent(string componentTypeName)
        {
            return _Components.ContainsKey(componentTypeName);
        }

        public bool HasComponent(Type componentType)
        {
            return HasComponent(componentType.Name);
        }

        public bool HasComponents(IEnumerable<string> types)
        {
            return types.All(t => HasComponent(t));
        }

        public bool HasComponents(IEnumerable<Type> types)
        {
            return types.All(t => HasComponent(t));
        }

        #endregion -- Has Component --

        public Entity Clone()
        {
            Entity e = new Entity();
            //TODO: implement this
            return e;
        }

        public bool IsInChannel(string channel)
        {
            return Channels.Contains(channel);
        }



    }

    public class ComponentChangedEventArgs : EventArgs
    {
        public BaseComponent Component { get; set; }
    }
}
