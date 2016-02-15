using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoenixSystem.Engine.Attributes;
namespace PhoenixSystem.Engine.Tests.Classes
{
    [AssociatedComponents(new Type[] { typeof(StringComponent), typeof(XYComponent) })]
    class LabelAspect : BaseAspect
    {
        public LabelAspect() : base()
        {

        }
        
    }
}
