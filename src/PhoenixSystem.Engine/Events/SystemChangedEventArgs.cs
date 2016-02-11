using System;

namespace PhoenixSystem.Engine.Events
{
    public class SystemChangedEventArgs : EventArgs
    {
        public ISystem System { get; set; }
    }
}