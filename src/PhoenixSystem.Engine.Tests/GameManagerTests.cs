using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class GameManagerTests
    {
        private readonly TestGameManager _gm;

        public GameManagerTests()
        {
            var channelManager = new BasicChannelManager();
            _gm = new TestGameManager(new BasicEntityAspectManager(channelManager), new EntityManager(channelManager),
                channelManager);
        }

        [Fact]
        public void Entity_Count_Should_Be_Zero()
        {
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
        }
    }
}