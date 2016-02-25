using System;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.System
{
    public interface ISystem : IChannelFilterable
    {
        Guid ID { get; }
        int Priority { get; }
        void Start();
        void Stop();
        void AddToGameManager(IGameManager gameManager);
        void RemoveFromGameManager(IGameManager gameManager);
        void Update(ITickEvent tickEvent);
    }
}