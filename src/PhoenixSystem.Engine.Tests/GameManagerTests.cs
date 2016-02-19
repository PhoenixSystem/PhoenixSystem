using PhoenixSystem.Engine.Tests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class GameManagerTests
    {
        TestGameManager _gm;
        public GameManagerTests()
        {
            BasicChannelManager channelManager = new BasicChannelManager();
            _gm = new TestGameManager(new BasicEntityAspectManager(channelManager), new EntityManager(channelManager), channelManager);
        }
    
        [Fact]
        public void Entity_Count_Should_Be_Zero()
        {
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
        }

        
    }
}
