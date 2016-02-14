using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixSystem.Engine.Events;

namespace PhoenixSystem.Engine
{
    public class GameChannelManager : IGameChannelManager
    {
        string _channel = "default";
        public string Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
                ChannelChanged?.Invoke(this, new ChannelChangedEventArgs() { Channel = _channel });
            }
        }

        public event EventHandler<ChannelChangedEventArgs> ChannelChanged;

        private GameChannelManager() { }


        //TODO: replace this singleton with some IOC/DI framework goodness
        private static GameChannelManager _Instance;
        public static GameChannelManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new GameChannelManager();
                return _Instance;
            }
        }
    }
}
