using System;

namespace PhoenixSystem.Engine.Events
{
    public class GameManagerChangedEventArgs : EventArgs
    {
        public IGameManager GameManager { get; set; }
    }
}