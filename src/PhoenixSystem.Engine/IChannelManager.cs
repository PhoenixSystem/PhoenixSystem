using System;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine
{
    public interface IChannelManager
    {
        string Channel { get; set; }
        event EventHandler<ChannelChangedEventArgs> ChannelChanged;
    }
}