using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IManager
    {
        int Priority { get; }
        void Update();
        void Register(IGameManager gameManager);
        IList<string> Channels { get; }
        bool IsInChannel(params string[] channel);
    }
}