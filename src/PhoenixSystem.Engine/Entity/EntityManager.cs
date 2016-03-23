using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.Entity
{
    public class EntityManager : IEntityManager
    {
        private readonly IChannelManager _channelManager;
        private readonly IObjectPool<IEntity> _entityPool;
        private IGameManager _gameManager;

        public EntityManager(
            IChannelManager channelManager,
            IObjectPool<IEntity> objectPool)
        {
            _channelManager = channelManager;
            _entityPool = objectPool;
        }

        public event EventHandler<EntityChangedEventArgs> EntityAdded;
        public event EventHandler<EntityRemovedEventArgs> EntityRemoved;
        public event EventHandler<ComponentChangedEventArgs> ComponentAdded;
        public event EventHandler<ComponentRemovedEventArgs> ComponentRemoved;

        public void Register(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public IDictionary<Guid, IEntity> Entities { get; } = new Dictionary<Guid, IEntity>();

        public IEntity Get(string name = "", string[] channels = null)
        {
            var entity = _entityPool.Get();

            entity.Name = name;

            if (channels == null || channels.Length == 0)
            {
                entity.Channels.Add(_channelManager.Channel);
            }
            else
            {
                foreach (var c in channels)
                {
                    entity.Channels.Add(c);
                }
            }

            entity.Deleted += CleanupDeleted;

            Entities[entity.ID] = entity;

            return entity;
        }

        public void Clear()
        {
            foreach (var entity in Entities)
            {
                entity.Value.Delete();
            }

            Entities.Clear();
        }

        public void Add(IEntity entity)
        {
            Entities[entity.ID] = entity;

            entity.ComponentAdded += ComponentAdded;
            entity.ComponentRemoved += ComponentRemoved;

            EntityAdded?.Invoke(this, new EntityChangedEventArgs(entity));
        }

        public void Add(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Remove(IEntity entity)
        {
            if (!Entities.ContainsKey(entity.ID)) return;

            entity = Entities[entity.ID];
            entity.ComponentAdded -= ComponentAdded;
            entity.ComponentRemoved -= ComponentRemoved;

            Entities.Remove(entity.ID);

            EntityRemoved?.Invoke(this, new EntityRemovedEventArgs(entity));
        }

        private void CleanupDeleted(object sender, EventArgs eargs)
        {
            var entity = sender as IEntity;

            if (entity == null) return;

            entity.Deleted -= CleanupDeleted;

            _entityPool.Put(entity);

            Entities.Remove(entity.ID);

            _gameManager.RemoveEntity(entity);
        }
    }
}