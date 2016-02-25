using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Collections;

namespace PhoenixSystem.Engine
{
    public class EntityManager : IEntityManager
    {
        private readonly IChannelManager _channelManager;
        private readonly IObjectPool<IEntity> _entityPool;
        private IGameManager _gameManager;

        public EntityManager(IChannelManager channelManager)
        {
            _channelManager = channelManager;
            _entityPool = new ObjectPool<IEntity>(() => new Entity(), ResetEntity);
        }

        public IDictionary<Guid, IEntity> Entities { get; } = new Dictionary<Guid, IEntity>();

        public IEntity Get(string name = "", string[] channels = null)
        {
            var e = _entityPool.Get();
            e.Name = name;
            if (channels == null || channels.Length == 0)
                e.Channels.Add(_channelManager.Channel);
            else
            {
                foreach (var c in channels)
                {
                    e.Channels.Add(c);
                }
            }
            e.Deleted += CleanupDeleted;
            Entities[e.ID] = e;

            return e;
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