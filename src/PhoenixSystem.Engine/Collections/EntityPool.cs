using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Collections
{
    public class EntityPool : ObjectPool<IEntity>
    {
        public EntityPool() : base(() => new DefaultEntity(), entity => entity.Reset())
        {
        }
    }
}