using System.Collections.Generic;

namespace PhoenixSystem.Engine.Channel
{
    public interface IChannelFilterable
    {
        IList<string> Channels { get; }
    }
}