using System;

namespace PhoenixSystem.Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AssociatedComponentsAttribute : Attribute, IAssociatedComponentsAttribute
    {
        // This is a positional argument
        public AssociatedComponentsAttribute(params Type[] associatedComponents)
        {
            ComponentTypes = associatedComponents;
        }

        public Type[] ComponentTypes { get; }
    }
}