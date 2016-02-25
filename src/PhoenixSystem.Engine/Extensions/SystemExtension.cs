using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Extensions
{
    public static class SystemExtension
    {
        public static int? GetKeyBySystemId(this IDictionary<int, ISystem> systems, Guid systemId)
        {
            foreach (var system in systems.Where(system => system.Value.ID == systemId))
            {
                return system.Key;
            }

            return null;
        }
    }
}