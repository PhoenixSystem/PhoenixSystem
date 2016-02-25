using System;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine.Channel
{
    public class ChannelManager : IChannelManager
    {
        private string _channel = "default";

        public string Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
                ChannelChanged?.Invoke(this, new ChannelChangedEventArgs(_channel));
            }
        }

        public event EventHandler<ChannelChangedEventArgs> ChannelChanged;
    }
}