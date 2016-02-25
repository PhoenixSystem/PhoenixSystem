using System;

namespace PhoenixSystem.Engine.Component
{
    public interface IComponent
    {
        Guid ID { get; }
        IComponent Clone();
        void Reset();
    }
}