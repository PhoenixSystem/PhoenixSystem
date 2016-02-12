using System;

namespace PhoenixSystem.Engine.Events
{
    internal class EntityChangedEventArgs : EventArgs
    {
        public IEntity Entity { get; set; }
    }
}