using System;
using PhoenixSystem.Engine.Entity;

namespace PhoenixSystem.Engine.Events
{
    internal class EntityRemovedEventArgs : EventArgs
    {
        public EntityRemovedEventArgs(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }
    }
}