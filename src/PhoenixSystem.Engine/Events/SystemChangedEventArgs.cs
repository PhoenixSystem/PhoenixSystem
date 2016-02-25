using System;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Events
{
    public class SystemChangedEventArgs : EventArgs
    {
        public SystemChangedEventArgs(ISystem system)
        {
            System = system;
        }

        public ISystem System { get; set; }
    }
}