using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class GameManagerTests
    {
        private readonly IChannelManager _channelManager = new ChannelManager();
        private readonly IGameManager _gameManager;

        public GameManagerTests()
        {
            var systemManager = new SystemManager(_channelManager);
            var entityManager = new EntityManager(_channelManager, new EntityPool());
            _gameManager = new TestGameManager(new DefaultEntityAspectManager(_channelManager, entityManager), entityManager, systemManager);
        }

        [Fact]
        public void Entity_Count_Should_Be_Zero()
        {
            Assert.Equal(0, _gameManager.EntityManager.Entities.Count);
        }

        [Fact]
        public void AddEntity_Should_Increase_Entity_Count()
        {
            var expected = 1;
            var entity = new DefaultEntity();
            Assert.Equal(0, _gameManager.EntityManager.Entities.Count);
            _gameManager.AddEntity(entity);
            Assert.Equal(expected, _gameManager.EntityManager.Entities.Count);
        }

        [Fact]
        public void AddEntity_Should_Notify_EntityAdded()
        {
            var notified = false;
            var entity = new DefaultEntity();
            _gameManager.EntityAdded += (s, e) => notified = true;
            _gameManager.AddEntity(entity);
            Assert.True(notified);
        }

        [Fact]
        public void Entity_Should_Be_In_Entities_After_Added()
        {
            var entity = new DefaultEntity();
            _gameManager.AddEntity(entity);
            Assert.True(_gameManager.EntityManager.Entities.ContainsKey(entity.ID));
        }

        [Fact]
        public void RemoveEntity_Should_Decrease_Entity_Count()
        {
            var entity = new DefaultEntity();
            Assert.Equal(0, _gameManager.EntityManager.Entities.Count);
            _gameManager.AddEntity(entity);
            Assert.Equal(1, _gameManager.EntityManager.Entities.Count);
            _gameManager.RemoveEntity(entity);
            Assert.Equal(0, _gameManager.EntityManager.Entities.Count);
        }

        [Fact]
        public void RemoveEntity_Should_Notify_EntityRemoved()
        {
            var notified = false;
            var entity = new DefaultEntity();
            _gameManager.AddEntity(entity);
            _gameManager.EntityRemoved += (s, e) => notified = true;
            _gameManager.RemoveEntity(entity);
            Assert.True(notified);
        }

        [Fact]
        public void Entity_Should_Not_Be_In_Entities_After_Removed()
        {
            var entity = new DefaultEntity();
            _gameManager.AddEntity(entity);
            Assert.True(_gameManager.EntityManager.Entities.ContainsKey(entity.ID));
            _gameManager.RemoveEntity(entity);
            Assert.False(_gameManager.EntityManager.Entities.ContainsKey(entity.ID));
        }

        [Fact]
        public void Remove_All_Entities_Should_Result_In_0_Remaining_Entities()
        {
            const int count = 5;
            for (var i = 0; i < count; i++)
            {
                _gameManager.AddEntity(new DefaultEntity());
            }
            Assert.Equal(count, _gameManager.EntityManager.Entities.Count);
            _gameManager.RemoveAllEntities();
            Assert.Equal(0, _gameManager.EntityManager.Entities.Count);
        }

        [Fact]
        public void AddSystem_Should_Increase_Count_Of_Systems()
        {
            const int expected = 1;
            var system = new LabelSystem(_channelManager, 10);
            Assert.Equal(0, _gameManager.Systems.Count());
            _gameManager.AddSystem(system);
            Assert.Equal(expected, _gameManager.Systems.Count());
        }

        [Fact]
        public void System_Should_Be_Available_After_Added_To_Game_Manager()
        {
            var system = new LabelSystem(_channelManager, 10);
            _gameManager.AddSystem(system);
            Assert.True(_gameManager.Systems.GetBySystemType(system.GetType()) != null);
        }

        [Fact]
        public void SystemAdded_Should_Notify_After_System_Is_Added()
        {
            var notified = false;
            var system = new LabelSystem(_channelManager, 10);
            _gameManager.SystemAdded += (s, e) => notified = true;
            _gameManager.AddSystem(system);
            Assert.True(notified);
        }

        [Fact]
        public void RemoveSystem_Should_Reduce_Count_Of_Systems()
        {
            var system = new LabelSystem(_channelManager, 10);
            Assert.Equal(0, _gameManager.Systems.Count());
            _gameManager.AddSystem(system);
            Assert.Equal(1, _gameManager.Systems.Count());
            _gameManager.RemoveSystem<LabelSystem>(false);
            Assert.Equal(0, _gameManager.Systems.Count());
        }

        [Fact]
        public void System_Should_Not_Be_Available_After_Removed_From_Game_Manager()
        {
            var system = new LabelSystem(_channelManager, 10);
            _gameManager.AddSystem(system);
            Assert.True(_gameManager.Systems.GetBySystemType(system.GetType()) != null);
            _gameManager.RemoveSystem<LabelSystem>(false);
            Assert.True(_gameManager.Systems.GetBySystemType(typeof (LabelSystem)) == null);
        }

        [Theory,
         InlineData(true),
         InlineData(false)
        ]
        public void SystemRemoved_Should_Notify_Only_If_Flag_Is_Set(bool shouldNotify)
        {
            var notified = false;
            var system = new LabelSystem(_channelManager, 10);
            _gameManager.AddSystem(system);
            _gameManager.SystemRemoved += (s, e) => notified = true;
            _gameManager.RemoveSystem<LabelSystem>(shouldNotify);
            Assert.Equal(shouldNotify, notified);
        }

        [Fact]
        public void DrawableSystem_Should_Be_Added_To_DrawableSystems()
        {
            var system = new DrawableLabelSystem(_channelManager, 10);
            _gameManager.AddSystem(system);
            Assert.Equal(system, _gameManager.Systems.GetBySystemType(system.GetType()));
            Assert.Equal(1, _gameManager.Systems.Count());
        }

        [Fact]
        public void DrawableSystem_Should_Be_Removed_From_DrawableSystems()
        {
            var system = new DrawableLabelSystem(_channelManager, 10);
            _gameManager.AddSystem(system);
            Assert.Equal(system, _gameManager.Systems.GetBySystemType(system.GetType()));
            _gameManager.RemoveSystem<DrawableLabelSystem>(false);
            Assert.Equal(0, _gameManager.Systems.Count());
        }
    }
}