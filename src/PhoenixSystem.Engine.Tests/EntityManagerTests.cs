using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityManagerTests
    {
        private readonly IEntityManager _entityManager;

        public EntityManagerTests()
        {
            var channelManager = new ChannelManager();
            _entityManager = new EntityManager(channelManager, new EntityPool());
        }

        [Fact]
        public void Entity_Count_Should_Be_Zero_By_Default()
        {
            Assert.Equal(0, _entityManager.Entities.Count);
        }

        [Fact]
        public void Get_Should_Create_An_Entity()
        {
            var e = _entityManager.Get();
            Assert.NotNull(e);
            Assert.Equal(_entityManager.Entities.Count, 1);
        }

        [Fact]
        public void Entity_Should_Have_Correct_Name_And_Channels()
        {
            var name = "Test Name";
            string[] channels = {"chOne", "chTwo"};
            var e = _entityManager.Get(name, channels);
            Assert.Equal(name, e.Name);
            Assert.Contains(channels[0], e.Channels);
            Assert.Contains(channels[1], e.Channels);
        }
    }
}