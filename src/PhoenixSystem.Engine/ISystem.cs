using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface ISystem
    {
        Guid ID { get; }
        void Start();
        void Stop();
        int Priority { get; }
        void AddToGameManager(IGameManager gameManager);
        void RemoveFromGameManager(IGameManager gameManager);

        void Update(ITickEvent tickEvent);

        IList<string> Channels { get; }

        bool IsInChannel(params string[] channel);
    }
}