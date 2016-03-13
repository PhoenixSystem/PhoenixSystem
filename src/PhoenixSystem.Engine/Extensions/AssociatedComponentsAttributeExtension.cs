using System;
using System.Linq;
using System.Reflection;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Attributes;

namespace PhoenixSystem.Engine.Extensions
{
    public static class AssociatedComponentsAttributeExtension
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

            var types = aspect.GetTypeInfo().GetCustomAttributes(typeof (AssociatedComponentsAttribute), true);

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