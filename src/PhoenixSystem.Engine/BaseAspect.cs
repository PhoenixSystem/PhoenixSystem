using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixSystem.Engine
{
    public abstract class BaseAspect : EqualityComparer<BaseAspect>, ICloneable, IAspect
    {
        private readonly List<string> _channels = new List<string>();

        protected BaseAspect()
        {
            Components = new Dictionary<string, IComponent>();
        }

        public Dictionary<string, IComponent> Components { get; }

        public bool IsDeleted { get; private set; }

        public Guid ID { get; } = Guid.NewGuid();

        public abstract object Clone();

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
            foreach (var componentType in Components.Keys)
            {
                Components[componentType] = e.Components[componentType];
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

        public override bool Equals(BaseAspect x, BaseAspect y)
        {
            return x.ID == y.ID;
        }

        public bool IsInChannel(string channelName)
        {
            return _channels.Contains(channelName);
        }
    }
}