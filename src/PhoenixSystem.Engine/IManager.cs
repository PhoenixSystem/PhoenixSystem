using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IManager
    {
        int Priority { get; }
        IList<string> Channels { get; }
        void Update();
        void Register(IGameManager gameManager);
        bool IsInChannel(params string[] channel);
    }
}