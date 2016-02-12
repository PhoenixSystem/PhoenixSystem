using System;

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
    }
}