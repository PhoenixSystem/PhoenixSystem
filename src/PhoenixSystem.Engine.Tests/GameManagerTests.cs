using System;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;
using System.Linq;

namespace PhoenixSystem.Engine.Tests
{
    public class GameManagerTests
    {
        private readonly TestGameManager _gm;
        IChannelManager channelManager = new BasicChannelManager();
        public GameManagerTests()
        {
            var em = new EntityManager(channelManager);
            _gm = new TestGameManager(new BasicEntityAspectManager(channelManager, em), em,
                channelManager);
        }

        [Fact]
        public void Entity_Count_Should_Be_Zero()
        {
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
        }

        [Fact]
        public void AddEntity_Should_Increase_Entity_Count()
        {
            int expected = 1;
            var entity = new Entity();
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
            _gm.AddEntity(entity);
            Assert.Equal(expected, _gm.EntityManager.Entities.Count);
        }

        [Fact]
        public void AddEntity_Should_Notify_EntityAdded()
        {
            bool notified = false;
            var entity = new Entity();
            _gm.EntityAdded += (s, e) => notified = true;
            _gm.AddEntity(entity);
            Assert.True(notified);
        }

        [Fact]
        public void Entity_Should_Be_In_Entities_After_Added()
        {
            var entity = new Entity();
            _gm.AddEntity(entity);
            Assert.True(_gm.EntityManager.Entities.ContainsKey(entity.ID));
        }

        [Fact]
        public void RemoveEntity_Should_Decrease_Entity_Count()
        {
            var entity = new Entity();
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
            _gm.AddEntity(entity);
            Assert.Equal(1, _gm.EntityManager.Entities.Count);
            _gm.RemoveEntity(entity);
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
        }

        [Fact]
        public void RemoveEntity_Should_Notify_EntityRemoved()
        {
            bool notified = false;
            var entity = new Entity();            
            _gm.AddEntity(entity);
            _gm.EntityRemoved += (s, e) => notified = true;
            _gm.RemoveEntity(entity);
            Assert.True(notified);
        }

        [Fact]
        public void Entity_Should_Not_Be_In_Entities_After_Removed()
        {
            var entity = new Entity();
            _gm.AddEntity(entity);
            Assert.True(_gm.EntityManager.Entities.ContainsKey(entity.ID));
            _gm.RemoveEntity(entity);
            Assert.False(_gm.EntityManager.Entities.ContainsKey(entity.ID));
        }

        [Fact]
        public void Remove_All_Entities_Should_Result_In_0_Remaining_Entities()
        {
            int count = 5;
            for(var i = 0; i< count; i++)
            {
                _gm.AddEntity(new Entity());
            }
            Assert.Equal(count, _gm.EntityManager.Entities.Count);
            _gm.RemoveAllEntities();
            Assert.Equal(0, _gm.EntityManager.Entities.Count);
        }

        [Fact]
        public void AddSystem_Should_Increase_Count_Of_Systems()
        {
            int expected = 1;
            var system = new LabelSystem(channelManager, 10);
            Assert.Equal(0, _gm.Systems.Count());
            _gm.AddSystem(system);
            Assert.Equal(expected, _gm.Systems.Count());
        }

        [Fact]
        public void System_Should_Be_Available_After_Added_To_Game_Manager()
        {
            var system = new LabelSystem(channelManager, 10);
            _gm.AddSystem(system);
            Assert.True(_gm.Systems.Any(s => s.GetType() == system.GetType()));
        }

        [Fact]
        public void SystemAdded_Should_Notify_After_System_Is_Added()
        {
            bool notified = false;
            var system = new LabelSystem(channelManager, 10);
            _gm.SystemAdded += (s, e) => notified = true;
            _gm.AddSystem(system);
            Assert.True(notified);
        }

        [Fact]
        public void RemoveSystem_Should_Reduce_Count_Of_Systems()
        {
            var system = new LabelSystem(channelManager, 10);
            Assert.Equal(0, _gm.Systems.Count());
            _gm.AddSystem(system);
            Assert.Equal(1, _gm.Systems.Count());
            _gm.RemoveSystem<LabelSystem>(false);
            Assert.Equal(0, _gm.Systems.Count());
        }

        [Fact]
        public void System_Should_Not_Be_Available_After_Removed_From_Game_Manager()
        {
            var system = new LabelSystem(channelManager, 10);
            _gm.AddSystem(system);
            Assert.True(_gm.Systems.Any(s => s.GetType() == system.GetType()));
            _gm.RemoveSystem<LabelSystem>(false);
            Assert.True(_gm.Systems.All(s => s.GetType() != typeof(LabelSystem)));
        }

        [Theory,
         InlineData(true),
         InlineData(false),
        ]
        public void SystemRemoved_Should_Notify_Only_If_Flag_Is_Set(bool shouldNotify)
        {
            bool notified = false;
            var system = new LabelSystem(channelManager, 10);
            _gm.AddSystem(system);
            _gm.SystemRemoved += (s, e) => notified = true;
            _gm.RemoveSystem<LabelSystem>(shouldNotify);
            Assert.Equal(shouldNotify, notified);

        }
    }
}