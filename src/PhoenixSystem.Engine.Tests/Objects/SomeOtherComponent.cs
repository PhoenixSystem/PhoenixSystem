using PhoenixSystem.Engine.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Tests.Objects
{
    public class SomeOtherComponent : BaseComponent
    {
        public override IComponent Clone()
        {
            return new SomeOtherComponent();
        }

        public override void Reset()
        {
            //does nothing
        }
    }
}
