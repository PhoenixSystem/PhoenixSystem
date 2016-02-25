using PhoenixSystem.Engine.Channel;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class ChannelManagerTests
    {
        [Fact]
        public void Should_Notify_When_Channel_Changes()
        {
            var cm = new ChannelManager();
            var notify = false;
            cm.ChannelChanged += (s, e) => notify = true;
            cm.Channel = "different";
            Assert.True(notify);
        }
    }
}