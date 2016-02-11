using System;

namespace PhoenixSystem.Engine.Events
{
    public class ComponentChangedEventArgs : EventArgs
    {
        public IComponent Component { get; set; }
    }
}