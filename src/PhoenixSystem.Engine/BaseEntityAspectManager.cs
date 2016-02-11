using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public abstract class BaseEntityAspectManager<AspectFamily> where AspectFamily : IEntityAspectMatchingFamily
    {
        public BaseGameManager GameManager
        {
            get;
            set;
        }

        public IEntityAspectMatchingFamily CreateAspectMatchingFamily<Aspect>()
        {
            return null;
        }

    }
}
