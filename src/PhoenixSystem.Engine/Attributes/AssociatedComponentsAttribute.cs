using System;
using System.Collections.Generic;
using System.Reflection;
using PhoenixSystem.Engine.Aspect;
using System.Linq;

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

    public static class AssociatedComponentsAttributeHelper
    {
        public static Type[] GetAssociatedComponentTypes(this IAspect aspect)
        {
            return GetAssociatedComponentTypes(aspect.GetType());
        }

        public static Type[] GetAssociatedComponentTypes(Type aspect)
        {
            //TODO: optimize this so that we don't have to construct a new attribute everytime referenced.
            //Should probably reference some sort of in-memory manager that caches the AssociatedComponents for each 
            //Aspect
            
            IEnumerable<Attribute> types = aspect.GetTypeInfo().GetCustomAttributes(typeof (AssociatedComponentsAttribute), true);

            if (types.Count() == 0)
            {
                throw new InvalidOperationException("Associated Components missing from Aspect");
            }

            var attribute = types.FirstOrDefault() as IAssociatedComponentsAttribute;

            if (attribute == null || attribute.ComponentTypes.Length == 0)
            {
                throw new InvalidOperationException("Associated Components missing from Aspect");
            }

            return attribute.ComponentTypes;
        }
    }
}