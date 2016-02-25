using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Attributes;
using PhoenixSystem.Engine.Component;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Aspect
{
    public abstract class BaseAspect : IAspect
    {
        protected BaseAspect()
        {
            Components = new Dictionary<Type, IComponent>();
        }

        public bool IsDeleted { get; private set; }

        public IDictionary<Type, IComponent> Components { get; }

        public IList<string> Channels { get; } = new List<string>();

        public Guid ID { get; } = Guid.NewGuid();

        public event EventHandler Deleted;

        public void Delete()
        {
            IsDeleted = true;
            OnDeleted();
        }

        public void Init(IEntity entity)
        {
            if (!EntityIsMatch(entity)) return;

            InitComponents(entity);

            foreach (var s in entity.Channels)
            {
                Channels.Add(s);
            }
        }


        public virtual void Reset()
        {
            Components.Clear();
            Channels.Clear();
        }

        protected virtual void OnDeleted()
        {
            Deleted?.Invoke(this, null);
        }

        private bool EntityIsMatch(IEntity entity)
        {
            var componentTypes = this.GetAssociatedComponentTypes();
            return entity.HasComponents(componentTypes);
        }

        private void InitComponents(IEntity entity)
        {
            foreach (var type in this.GetAssociatedComponentTypes())
            {
                if (Components.ContainsKey(type))
                {
                    Components[type] = entity.Components[type];
                }
                else
                {
                    Components.Add(type, entity.Components[type]);
                }
            }
        }
    }
}