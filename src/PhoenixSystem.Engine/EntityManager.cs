using PhoenixSystem.Engine.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public class EntityManager : IEntityManager
    {
        public EntityManager(IChannelManager channelManager)
        {
            _channelManager = channelManager;
            _entityPool = new ObjectPool<IEntity>(() => new Entity(), resetEntity);
        }

        IObjectPool<IEntity> _entityPool; 
        IDictionary<Guid, IEntity> _activeEntities = new Dictionary<Guid, IEntity>();
        IGameManager _gameManager;
        private readonly IChannelManager _channelManager;

        public IDictionary<Guid, IEntity> Entities { get { return _activeEntities; } }

        private void resetEntity(IEntity e)
        {
            e.Channels.Clear();
            e.IsDeleted = false;
        }
        public IEntity Get(string name = "", string[] channels = null)
        {
            IEntity e = _entityPool.Get();
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
            e.Deleted += cleanupDeleted;
            _activeEntities[e.ID] = e;
           
            return e;

        }

        private void cleanupDeleted(object sender, EventArgs eargs)
        {
            var e = sender as IEntity;
            e.Deleted -= cleanupDeleted;
            _entityPool.Put(e);
            _activeEntities.Remove(e.ID);
            _gameManager.RemoveEntity(e);

        }

        public void Register(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }


    }
}
