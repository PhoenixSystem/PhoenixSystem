using System;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Events
{
    public class ComponentChangedEventArgs : EventArgs
    {
        public ComponentChangedEventArgs(IComponent component)
        {
            Component = component;
        }

        public IComponent Component { get; set; }
    }
}