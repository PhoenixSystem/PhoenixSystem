using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Extensions
{
    public static class ChannelFilterableExtensions
    {
        public static bool IsInChannel(this IChannelFilterable obj, params string[] channels)
        {
         
            return channels.Any(c => obj.Channels.Contains(c));
        
    }
    }
}
