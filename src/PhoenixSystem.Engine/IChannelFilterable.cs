using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IChannelFilterable
    {
        IList<string> Channels { get; }
    }
}