using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public abstract class BaseSystem
    {
        private bool _active = false;

        public Guid ID { get; private set; }
        public BaseSystem(int Priority)
        {
            this.ID = Guid.NewGuid();
            this.Priority = Priority;

        }

        public int Priority { get; private set; }
        public bool IsActive
        {
            get { return _active; }
        }
        public void Start()
        {
            _active = true;
        }

        public void Stop()
        {
            _active = false;
        }

        protected abstract void Update();

        public event EventHandler<GameManagerChangedEventArgs> AddedToGameManager;
        protected virtual void OnAddedToGameManager(BaseGameManager gameManager)
        {
            if(AddedToGameManager != null)
            {
                AddedToGameManager(this, new GameManagerChangedEventArgs() { GameManager = gameManager });
            }
        }

        public event EventHandler RemovedFromGameManager;
        protected virtual void OnRemovedFromGameManager()
        {
            if (RemovedFromGameManager != null)
            {
                RemovedFromGameManager(this, null);
            }
        }




    }

    public class GameManagerChangedEventArgs : EventArgs
    {
        public BaseGameManager GameManager { get; set; }
    }
}
