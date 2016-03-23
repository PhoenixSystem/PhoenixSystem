using System;
using System.Collections.Generic;
using System.Linq;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Events;
using PhoenixSystem.Engine.Extensions;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.System
{
    public class SystemManager : ISystemManager
    {
        private IGameManager _gameManager;
        private readonly IChannelManager _channelManager;
        private readonly List<ISystem> _systems = new List<ISystem>();
        private readonly List<IDrawableSystem> _drawableSystems = new List<IDrawableSystem>();
        
        public SystemManager(IChannelManager channelManager)
        {
            _channelManager = channelManager;
        }

        public event EventHandler SystemRemoved;
        public event EventHandler SystemAdded;
        public event EventHandler SystemStarted;
        public event EventHandler SystemStopped;

        public void SetGameManager(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Add(ISystem system)
        {
            if (ContainsSystem(system))
                throw new InvalidOperationException($"System {system.GetType().Name} already added to game manager.");

            _systems.Add(system);
            _systems.Sort();

            var drawableSystem = system as IDrawableSystem;

            if (drawableSystem != null)
            {
                _drawableSystems.Add(drawableSystem);
                _drawableSystems.Sort();
            }

            system.AddToGameManager(_gameManager);

            SystemAdded?.Invoke(this, new SystemChangedEventArgs(system));
        }

        public void Stop(Type systemType)
        {
            var system = GetBySystemType(systemType);

            if (system == null) return;

            system.Stop();

            SystemStopped?.Invoke(this, new SystemSuspendedEventArgs(system));
        }

        public ISystem GetBySystemType(Type systemType)
        {
            return _systems.SingleOrDefault(s => s.GetType() == systemType);
        }

        public void Update(ITickEvent tickEvent)
        {
            var systems = GetSystemsByChannels(_channelManager.Channel, "all");

            foreach (var system in systems)
            {
                system.Update(tickEvent);
            }
        }

        public void Draw(ITickEvent tickEvent)
        {
            var systems = GetDrawableSystemsByChannels(_channelManager.Channel);

            foreach (var system in systems)
            {
                system.Draw(tickEvent);
            }
        }

        public void Clear(bool shouldNotify = false)
        {
            while (_systems.Count > 0)
            {
                Remove(_systems.First().GetType(), shouldNotify);
            }
        }

        public int Count()
        {
            return _systems.Count;
        }

        public void Remove(Type systemType, bool shouldNotify = false)
        {
            var system = GetBySystemType(systemType);

            if (system == null) return;

            _systems.Remove(system);
            _systems.Sort();

            var drawableSystem = system as IDrawableSystem;

            if (drawableSystem != null)
            {
                _drawableSystems.Remove(drawableSystem);
                _drawableSystems.Sort();
            }

            system.RemoveFromGameManager(_gameManager);

            if (!shouldNotify) return;

            SystemRemoved?.Invoke(this, new SystemRemovedEventArgs(system));
        }

        public void Start(Type systemType)
        {
            var system = GetBySystemType(systemType);

            if (system == null) return;

            system.Start();

            SystemStarted?.Invoke(this, new SystemStartedEventArgs(system));
        }

        private bool ContainsSystem(ISystem system)
        {
            return _systems.Any(s => s.ID == system.ID);
        }

        private IEnumerable<ISystem> GetSystemsByChannels(params string[] channels)
        {
            return _systems.Where(system => system.IsInChannel(channels));
        }

        private IEnumerable<IDrawableSystem> GetDrawableSystemsByChannels(params string[] channels)
        {
            return _drawableSystems.Where(system => system.IsInChannel(channels));
        }
    }
}