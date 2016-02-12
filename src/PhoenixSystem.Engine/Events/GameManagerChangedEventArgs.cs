using System;

namespace PhoenixSystem.Engine.Events
{
    public class GameManagerChangedEventArgs : EventArgs
    {
        public GameManagerChangedEventArgs(IGameManager gameManager)
        {
            GameManager = gameManager;
        }

        public IGameManager GameManager { get; set; }
    }
}