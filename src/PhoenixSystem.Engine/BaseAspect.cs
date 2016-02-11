using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixSystem.Engine
{
    public abstract class BaseAspect : EqualityComparer<BaseAspect>, ICloneable
    {
        List<string> _Channels = new List<string>();
        protected BaseAspect()
        {
            Components = new Dictionary<string, BaseComponent>();
        }

        public Dictionary<string, BaseComponent> Components { get; }

        public Guid ID { get; } = Guid.NewGuid();

        public bool IsDeleted { get; private set; }
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

        public void InitComponents(Entity e)
        {
            foreach (var componentType in Components.Keys)
            {
                Components[componentType] = e.Components[componentType];
            }
        }

        public void Init(Entity e, IEnumerable<string> channels = null)
        {
            InitComponents(e);
            if (channels != null)
                _Channels.AddRange(channels);
        }

        public bool EntityIsMatch(Entity e)
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
            return _Channels.Contains(channelName);
        }
    }
}