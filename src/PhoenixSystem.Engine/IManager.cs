using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IManager :IChannelFilterable
    {
        int Priority { get; }
        void Update();
        void Register(IGameManager gameManager);
    }
}