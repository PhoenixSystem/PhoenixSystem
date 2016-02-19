using System;
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
        }
        Stack<IEntity> _inactiveEntities = new Stack<IEntity>();
        IDictionary<Guid, IEntity> _activeEntities = new Dictionary<Guid, IEntity>();
        IGameManager _gameManager;
        private readonly IChannelManager _channelManager;

        public IDictionary<Guid, IEntity> Entities { get { return _activeEntities; } }
        public IEntity Get(string name = "", string[] channels = null)
        {

            if (_inactiveEntities.Count > 0)
            {
                var e = _inactiveEntities.Pop();
                e.Channels.Clear();
                e.IsDeleted = false;
                if (channels != null && channels.Length > 0)
                {
                    foreach (var c in channels)
                    {
                        e.Channels.Add(c);
                    }
                }
                else
                {
                    e.Channels.Add(_channelManager.Channel);
                }
                return e;
            }
            var newEnt = newEntity(name, channels);
            _activeEntities[newEnt.ID] = newEnt;
            return newEnt;
        }

        private IEntity newEntity(string name, string[] channels)
        {
            var e = new Entity(name,channels);
            e.Deleted += cleanupDeleted;
            return e;
        }

        private void cleanupDeleted(object sender, EventArgs eargs)
        {
            var e = sender as IEntity;
            _inactiveEntities.Push(e);
            _activeEntities.Remove(e.ID);
            _gameManager.RemoveEntity(e);

        }

        public void Register(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        
    }
}
