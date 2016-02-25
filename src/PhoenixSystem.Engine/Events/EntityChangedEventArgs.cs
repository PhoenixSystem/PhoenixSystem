using System;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Events
{
    internal class EntityChangedEventArgs : EventArgs
    {
        public EntityChangedEventArgs(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }
    }
}