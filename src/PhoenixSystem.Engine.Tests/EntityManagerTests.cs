using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Entity;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityManagerTests
    {
        private readonly IEntityManager _em;

        public EntityManagerTests()
        {
            var cm = new DefaultChannelManager();
            _em = new EntityManager(cm);
        }

        [Fact]
        public void Entity_Count_Should_Be_Zero_By_Default()
        {
            Assert.Equal(0, _em.Entities.Count);
        }

        [Fact]
        public void Get_Should_Create_An_Entity()
        {
            var e = _em.Get();
            Assert.NotNull(e);
            Assert.Equal(_em.Entities.Count, 1);
        }

        [Fact]
        public void Entity_Should_Have_Correct_Name_And_Channels()
        {
            var name = "Test Name";
            string[] channels = {"chOne", "chTwo"};
            var e = _em.Get(name, channels);
            Assert.Equal(name, e.Name);
            Assert.Contains(channels[0], e.Channels);
            Assert.Contains(channels[1], e.Channels);
        }
    }
}