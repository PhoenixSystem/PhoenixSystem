using PhoenixSystem.Engine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public interface IGameChannelManager
    {
        string Channel { get; set; }
        event EventHandler<ChannelChangedEventArgs> ChannelChanged;
    }
}
