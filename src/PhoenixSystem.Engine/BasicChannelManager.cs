using System;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine
{
    public class BasicChannelManager : IChannelManager
    {
        private string _channel = "default";

        public string Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
                ChannelChanged?.Invoke(this, new ChannelChangedEventArgs {Channel = _channel});
            }
        }

        public event EventHandler<ChannelChangedEventArgs> ChannelChanged;
    }
}