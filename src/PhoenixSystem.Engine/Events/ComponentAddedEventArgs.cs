using System;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Events
{
    public class ComponentAddedEventArgs : EventArgs
    {
        public ComponentAddedEventArgs(IComponent component)
        {
            Component = component;
        }

        public IComponent Component { get; set; }
    }
}