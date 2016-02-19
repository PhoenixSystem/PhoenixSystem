using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityManagerTests
    {
        IEntityManager _em;
        public EntityManagerTests()
        {
            BasicChannelManager cm = new BasicChannelManager();
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
            string name = "Test Name";
            string[] channels = new string[] { "chOne", "chTwo" };
            var e = _em.Get(name, channels);
            Assert.Equal(name, e.Name);
            Assert.Contains(channels[0],e.Channels);
            Assert.Contains(channels[1], e.Channels);
            
        }

        
        
    }
}
