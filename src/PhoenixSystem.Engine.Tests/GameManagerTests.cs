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

        [Fact]
        public void AddEntity_Should_Increase_Entity_Count()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void AddEntity_Should_Notify_EntityAdded()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Entity_Should_Be_In_Entities_After_Added()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RemoveEntity_Should_Decrease_Entity_Count()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RemoveEntity_Should_Notify_EntityRemoved()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Entity_Should_Not_Be_In_Entities_After_Removed()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Remove_All_Entities_Should_Result_In_0_Remaining_Entities()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void AddSystem_Should_Increase_Count_Of_Systems()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void System_Should_Be_Available_After_Added_To_Game_Manager()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void SystemAdded_Should_Notify_After_System_Is_Added()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RemoveSystem_Should_Reduce_Count_Of_Systems()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void System_Should_Not_Be_Available_After_Removed_From_Game_Manager()
        {
            throw new NotImplementedException();
        }

        [Theory,
         InlineData(true),
         InlineData(false)]
        public void SystemRemoved_Should_Notify_Only_If_Flag_Is_Set(bool shouldNotify)
        {
            
        }
        

    }
}
