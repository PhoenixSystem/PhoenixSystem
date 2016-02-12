using System;

namespace PhoenixSystem.Engine.Events
{
    public class ChannelChangedEventArgs : EventArgs
    {
        public string Channel { get; set; }
    }
}