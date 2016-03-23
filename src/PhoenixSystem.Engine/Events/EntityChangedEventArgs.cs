using System;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Events
{
    public class EntityChangedEventArgs : EventArgs
    {
        public EntityChangedEventArgs(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }
    }
}