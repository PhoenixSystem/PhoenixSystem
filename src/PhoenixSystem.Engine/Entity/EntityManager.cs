using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.Entity
{
    public class EntityManager : IEntityManager
    {
        private readonly IChannelManager _channelManager;
        private readonly IObjectPool<IEntity> _entityPool;
        private IGameManager _gameManager;

        public EntityManager(IChannelManager channelManager, IObjectPool<IEntity> objectPool)
        {
            _channelManager = channelManager;
            _entityPool = objectPool;
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

        public void Register(IGameManager gameManager)
        {
            _gameManager = gameManager;
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