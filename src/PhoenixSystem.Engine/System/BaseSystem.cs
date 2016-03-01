using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.System
{
    public abstract class BaseSystem : ISystem
    {
        protected BaseSystem(IChannelManager channelManager, int priority, IEnumerable<string> channels = null)
        {
            ID = Guid.NewGuid();
            Priority = priority;

            if (channels == null)
            {
                Channels.Add(channelManager.Channel);
                return;
            }

            foreach (var c in channels)
            {
                Channels.Add(c);
            }
        }

        public bool IsActive { get; private set; }

        public int CompareTo(ISystem other)
        {
            return Priority.CompareTo(other.Priority);
        }

        public Guid ID { get; }

        public int Priority { get; }

        public IList<string> Channels { get; } = new List<string>();

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

        public override bool Equals(object obj)
        {
            if (!(obj is BaseSystem)) return base.Equals(obj);

            var other = (BaseSystem) obj;
            return ID == other.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}