using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Attributes;

namespace PhoenixSystem.Engine
{
    public abstract class BaseAspect : IAspect
    {
        private readonly List<string> _channels = new List<string>();

        protected BaseAspect()
        {
            Components = new Dictionary<string, IComponent>();
        }

        public bool IsDeleted { get; private set; }

        public Dictionary<string, IComponent> Components { get; }

        public Guid ID { get; } = Guid.NewGuid();

        public event EventHandler Deleted;

        public void Delete()
        {
            IsDeleted = true;
            OnDeleted();
        }

        public void Init(IEntity e, IEnumerable<string> channels = null)
        {
            InitComponents(e);
            if (channels != null)
                _channels.AddRange(channels);
        }

        public virtual void Reset()
        {
            Components.Clear();
            _channels.Clear();
        }

        public bool IsInChannel(string channelName)
        {
            return _channels.Contains(channelName);
        }

        protected virtual void OnDeleted()
        {
            Deleted?.Invoke(this, null);
        }

        public void InitComponents(IEntity e)
        {
            foreach (var componentTypeName in this.GetAssociatedComponentTypes().Select(componentType => componentType.Name))
            {
                if (Components.ContainsKey(componentTypeName))
                {
                    Components[componentTypeName] = e.Components[componentTypeName];
                    return;
                }

                Components.Add(componentTypeName, e.Components[componentTypeName]);
            }
        }

        public bool EntityIsMatch(IEntity e)
        {
            var componentTypes = this.GetAssociatedComponentTypes();
            return e.HasComponents(componentTypes);
        }
    }
}