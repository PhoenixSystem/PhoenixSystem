using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine
{
    public interface IManager : IChannelFilterable
    {
        int Priority { get; }
        void Update();
        void Register(IGameManager gameManager);
    }
}