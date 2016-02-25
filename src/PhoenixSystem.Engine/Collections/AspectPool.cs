using PhoenixSystem.Engine.Aspect;

namespace PhoenixSystem.Engine.Collections
{
    public class AspectPool<TAspectType> : ObjectPool<IAspect> where TAspectType : IAspect, new()
    {
        public AspectPool() : base(() => new TAspectType(), aspect => aspect.Reset())
        {
        }
    }
}