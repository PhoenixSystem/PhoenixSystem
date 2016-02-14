using System;

namespace PhoenixSystem.Engine
{
    public interface IComponent
    {
        Guid ID { get; }
        IComponent Clone();
    }
}