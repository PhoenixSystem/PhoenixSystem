using System;

namespace PhoenixSystem.Engine.Events
{
    public class TickEventArgs : EventArgs
    {
        public TickEventArgs(ITickEvent tickEvent)
        {
            TickEvent = tickEvent;
        }

        public ITickEvent TickEvent { get; set; }
    }
}