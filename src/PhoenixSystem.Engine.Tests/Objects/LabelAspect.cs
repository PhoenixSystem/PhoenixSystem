using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Attributes;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Tests.Objects
{
    [AssociatedComponents(new[] {typeof (StringComponent), typeof (XYComponent)})]
    public class LabelAspect : BaseAspect
    {
    }

    public static class LabelAspectExtensions
    {
        public static IEntity CreateLabelAspect(this IEntity entity, string label, int x, int y)
        {
            var stringComponent = new StringComponent {Value = label};
            var xyComponent = new XYComponent {X = x, Y = y};
            entity.AddComponent(stringComponent).AddComponent(xyComponent);
            return entity;
        }
    }
}