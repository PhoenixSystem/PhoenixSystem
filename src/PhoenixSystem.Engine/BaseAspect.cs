using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public abstract class BaseAspect : EqualityComparer<BaseAspect>, ICloneable
    {
        public Dictionary<string, BaseComponent> Components { get; private set; }
        private Guid _ID = Guid.NewGuid();
        public Guid ID
        {
            get
            {
                return _ID;
            }
        } 

        public BaseAspect()
        {
            Components = new Dictionary<string, BaseComponent>();
        }

        public bool IsDeleted { get; private set; }
        public event EventHandler Deleted;
        protected virtual void OnDeleted()
        {
            if (this.Deleted != null)
                Deleted(this, null);
        }

        public void Delete()
        {
            IsDeleted = true;
            OnDeleted();
        }

        public void InitComponents(Entity e)
        {
            foreach(string componentType in Components.Keys)
            {
                Components[componentType] = e.Components[componentType];
            }
        }

        public void Init(Entity e)
        {
            this.InitComponents(e);
        }

        public bool EntityIsMatch(Entity e)
        {
            var componentTypes = e.Components.Select(kvp => kvp.Key);
            return e.HasComponents(componentTypes);
        }

        public abstract void Reset();        
        public abstract object Clone();

        public override bool Equals(BaseAspect x, BaseAspect y)
        {
            return x.ID == y.ID;
        }
    }
}
