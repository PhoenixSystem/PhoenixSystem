using System;

namespace PhoenixSystem.Engine
{
    public abstract class BaseComponent : IComponent
    {
        public Guid ID { get; } = Guid.NewGuid();
    }
}