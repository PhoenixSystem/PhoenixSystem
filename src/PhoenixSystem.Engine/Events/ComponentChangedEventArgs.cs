using System;

namespace PhoenixSystem.Engine.Events
{
    public class ComponentChangedEventArgs : EventArgs
    {
        public BaseComponent Component { get; set; }
    }
}