using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
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