using System;
using PhoenixSystem.Engine.Component;

namespace PhoenixSystem.Engine.Events
{
    public class ComponentRemovedEventArgs : EventArgs
    {
        public ComponentRemovedEventArgs(IComponent component)
        {
            Component = component;
        }

        public IComponent Component { get; set; }
    }
}