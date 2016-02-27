using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Game;
using System;

namespace PhoenixSystem.Engine
{
    public interface IManager : IChannelFilterable, IComparable<IManager>
    {
        
        int Priority { get; }
        void Update();
        void Register(IGameManager gameManager);
    }
}