using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface ISystem
    {
        Guid ID { get; }
        int Priority { get; }
        IList<string> Channels { get; }
        void Start();
        void Stop();
        void AddToGameManager(IGameManager gameManager);
        void RemoveFromGameManager(IGameManager gameManager);
        void Update(ITickEvent tickEvent);
        bool IsInChannel(params string[] channel);
    }
}