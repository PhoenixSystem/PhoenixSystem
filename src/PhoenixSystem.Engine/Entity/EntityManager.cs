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
        private readonly IObjectPool _entityPool;
        private IGameManager _gameManager;

        public EntityManager(IChannelManager channelManager)
        {
            _channelManager = channelManager;
            _entityPool = new ObjectPool(() => new DefaultEntity(), entity => ResetEntity((IEntity)entity));
        }

        public IDictionary<Guid, IEntity> Entities { get; } = new Dictionary<Guid, IEntity>();

        public IEntity Get(string name = "", string[] channels = null)
        {
            var entity = _entityPool.Get<IEntity>();

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

        private static void ResetEntity(IEntity e)
        {
            e.Channels.Clear();
            e.IsDeleted = false;
        }

        private void CleanupDeleted(object sender, EventArgs eargs)
        {
            var e = sender as IEntity;

            if (e == null) return;

            e.Deleted -= CleanupDeleted;

            _entityPool.Put(e);

            Entities.Remove(e.ID);

            _gameManager.RemoveEntity(e);
        }
    }
}