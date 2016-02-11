using System;

namespace PhoenixSystem.Engine
{
    public interface ISystem
    {
        Guid ID { get; }
        int Priority { get; }
        void AddToGameManager(IGameManager gameManager);
    }
}