using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Attributes;
using PhoenixSystem.Monogame.Components;

namespace PhoenixSystem.Monogame.Aspects
{
    [AssociatedComponents(typeof(PositionComponent), typeof(VelocityComponent))]
    public class MovementAspect :BaseAspect
    {
    }
}
