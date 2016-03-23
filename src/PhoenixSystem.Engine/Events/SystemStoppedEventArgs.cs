using System;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Events
{
    public class SystemStoppedEventArgs : EventArgs
    {
        public SystemStoppedEventArgs(ISystem system)
        {
            System = system;
        }

        public ISystem System { get; set; }
    }
}