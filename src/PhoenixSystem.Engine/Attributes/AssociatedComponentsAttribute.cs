using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AssociatedComponentsAttribute : Attribute, IAssociatedComponentsAttribute
    {
        private readonly Type[] _AssociatedComponents;
        // This is a positional argument
        public AssociatedComponentsAttribute(Type[] AssociatedComponents)
        {
            this._AssociatedComponents = AssociatedComponents;
        }

        public Type[] ComponentTypes
        {
            get
            {
                return _AssociatedComponents;
            }
        }
    }

    public static class AssociatedComponentsAttributeHelper
    {
        public static Type[] GetAssociatedComponentTypes(this IAspect aspect)
        {
            return GetAssociatedComponentTypes(aspect.GetType());
        }

        public static Type[] GetAssociatedComponentTypes(Type aspect)
        {
            var types = aspect.GetType().GetCustomAttributes(typeof(AssociatedComponentsAttribute), false);
            if (types.Length == 0)
                throw new ApplicationException("Associated Components missing from Aspect");

            var attribute = types[0] as IAssociatedComponentsAttribute;

            if (attribute == null || attribute.ComponentTypes.Length == 0)
                throw new ApplicationException("Associated Components missing from Aspect");
            return attribute.ComponentTypes;
        }
    }
}
