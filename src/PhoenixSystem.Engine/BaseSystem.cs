using System;
using PhoenixSystem.Engine.Events;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixSystem.Engine
{
    public abstract class BaseSystem : ISystem, IComparable<BaseSystem>
    {
        private IChannelManager _channelManager;
        protected BaseSystem(IChannelManager channelManager, int priority, IEnumerable<string> channels = null)
        {
            _channelManager = channelManager;
            ID = Guid.NewGuid();
            Priority = priority;
            if (channels != null)
            {
                foreach (var c in channels)
                {
                    Channels.Add(c);
                }
            }
            else
                Channels.Add(_channelManager.Channel);
        }

        public bool IsActive { get; private set; }

        public Guid ID { get; }

        public int Priority { get; }

        public IList<string> Channels { get; private set; }
        public abstract void AddToGameManager(IGameManager gameManager);

        public abstract void RemoveFromGameManager(IGameManager gameManager);

        public abstract void Update(ITickEvent tickEvent);

        public void Start()
        {
            IsActive = true;
        }

        public void Stop()
        {
            IsActive = false;
        }

        protected abstract void Update();

        public event EventHandler<GameManagerChangedEventArgs> AddedToGameManager;

        protected virtual void OnAddedToGameManager(IGameManager gameManager)
        {
            AddedToGameManager?.Invoke(this, new GameManagerChangedEventArgs(gameManager));
        }

        public event EventHandler RemovedFromGameManager;

        protected virtual void OnRemovedFromGameManager()
        {
            RemovedFromGameManager?.Invoke(this, null);
        }

        public int CompareTo(BaseSystem other)
        {
            return this.Priority.CompareTo(other.Priority);   
        }

        public bool IsInChannel(params string[] channels)
        {
            foreach(var c in channels)
            {
                if (Channels.Contains(c))
                    return true;
            }
            return false;
        }
    }
}