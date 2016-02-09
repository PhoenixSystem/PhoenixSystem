using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public class BaseNode
    {
        private Guid _ID = Guid.NewGuid();
        public Guid ID
        {
            get
            {
                return _ID;
            }
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
    }
}
