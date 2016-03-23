using System.Collections.Generic;

namespace PhoenixSystem.Engine.Game
{
    public class Managers : IManagers
    {
        private readonly List<IManager> _managers = new List<IManager>();
        private IGameManager _gameManager;

        public void Register(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Add(IManager manager)
        {
            manager.Register(_gameManager);

            _managers.Add(manager);
            _managers.Sort();
        }

        public void Update(ITickEvent tickEvent)
        {
            foreach (var manager in _managers)
            {
                manager.Update();
            }
        }
    }
}