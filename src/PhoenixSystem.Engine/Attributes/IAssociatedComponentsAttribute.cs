using System;

namespace PhoenixSystem.Engine.Attributes
{
    public interface IAssociatedComponentsAttribute
    {
        Type[] ComponentTypes { get; }
    }
}