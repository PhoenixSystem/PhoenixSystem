using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhoenixSystem.Engine.Attributes;
namespace PhoenixSystem.Engine.Tests.Objects
{
    [AssociatedComponents(new Type[] { typeof(StringComponent), typeof(XYComponent) })]
    public class LabelAspect : BaseAspect
    {
        public LabelAspect() : base()
        {

        }

        
        
    }

    public static class LabelAspectExtensions
    {
        public static IEntity CreateLabelAspect(this IEntity entity, string label, int x, int y)
        {
            var stringComponent = new StringComponent() { Value = label };
            var xyComponent = new XYComponent() { X = x, Y = y };
            entity.AddComponent(stringComponent).AddComponent(xyComponent);
            return entity;
        }
    }
}
