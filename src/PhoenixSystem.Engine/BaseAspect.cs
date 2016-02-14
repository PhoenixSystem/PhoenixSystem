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

        public Dictionary<string, IComponent> Components { get; }

        public bool IsDeleted { get; private set; }

        public Guid ID { get; } = Guid.NewGuid();

        public event EventHandler Deleted;

        protected virtual void OnDeleted()
        {
            Deleted?.Invoke(this, null);
        }

        public void Delete()
        {
            IsDeleted = true;
            OnDeleted();
        }

        public void InitComponents(IEntity e)
        {
            foreach (var componentType in this.GetAssociatedComponentTypes())
            {
                var componentTypeName = componentType.Name;
                if (Components.ContainsKey(componentTypeName))
                    Components[componentTypeName] = e.Components[componentTypeName];
                else
                    Components.Add(componentTypeName, e.Components[componentTypeName]);
            }
        }

        public void Init(IEntity e, IEnumerable<string> channels = null)
        {
            InitComponents(e);
            if (channels != null)
                _channels.AddRange(channels);
        }

        public bool EntityIsMatch(IEntity e)
        {
            var componentTypes = e.Components.Select(kvp => kvp.Key);
            return e.HasComponents(componentTypes);
        }

        public abstract void Reset();

        public abstract IAspect Clone();

        public bool IsInChannel(string channelName)
        {
            return _channels.Contains(channelName);
        }
    }
}