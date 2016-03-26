using System;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Events
{
    public class EntityRemovedEventArgs : EventArgs
    {
        public EntityRemovedEventArgs(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }
    }
}