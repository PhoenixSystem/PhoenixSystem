using System;

namespace PhoenixSystem.Engine.Events
{
    public class GameManagerChangedEventArgs : EventArgs
    {
        public BaseGameManager GameManager { get; set; }
    }
}