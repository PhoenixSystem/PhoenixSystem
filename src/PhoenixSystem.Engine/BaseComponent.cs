using System;

namespace PhoenixSystem.Engine
{
    public abstract class BaseComponent
    {
        public Guid ID { get; } = Guid.NewGuid();
    }
}