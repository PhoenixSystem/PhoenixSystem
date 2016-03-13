using System;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Extensions
{
    public static class AspectExtension
    {
        public static T GetComponent<T>(this IAspect aspect) where T : class, IComponent
        {
            if (!aspect.Components.ContainsKey(typeof (T)))
            {
                throw new InvalidOperationException("Component type " + typeof (T).Name + "not found.");
            }

            return aspect.Components[typeof(T)] as T;
        }
    }
}