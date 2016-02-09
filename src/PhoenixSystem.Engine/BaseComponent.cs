using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public abstract class BaseComponent
    {
        private Guid _ID = Guid.NewGuid();
        public Guid ID
        {
            get { return _ID; }
        }


    }
}
