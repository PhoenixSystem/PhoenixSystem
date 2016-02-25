using System;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Events
{
    public class SystemSuspendedEventArgs : EventArgs
    {
        public SystemSuspendedEventArgs(ISystem system)
        {
            System = system;
        }

        public ISystem System { get; set; }
    }
}