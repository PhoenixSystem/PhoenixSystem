using System;
using Xunit;
using PhoenixSystem.Engine;
public class ChannelManagerTests
{
    public ChannelManagerTests() { }

    [Fact]
    public void Should_Notify_When_Channel_Changes()
    {
        IChannelManager cm = new BasicChannelManager();
        bool notify = false;
        cm.ChannelChanged += (s, e) => notify = true;
        cm.Channel = "different";
        Assert.True(notify);
    }
}
