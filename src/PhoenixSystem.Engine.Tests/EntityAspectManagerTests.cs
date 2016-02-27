using System;
using System.Linq;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityAspectManagerTests
    {
        private readonly IEntityAspectManager _entityAspectManager;
        private readonly IEntityManager _entityManager;

        public EntityAspectManagerTests()
        {
            var channelManager = new ChannelManager();
            _entityManager = new EntityManager(channelManager, new EntityPool());
            _entityAspectManager = new DefaultEntityAspectManager(channelManager, _entityManager);
        }

        [Fact]
        public void GetAspectList_Should_Return_Collection_Of_Appropriate_Type_Of_Aspect()
        {
            var expected = typeof (LabelAspect);
            var aspectList = _entityAspectManager.GetAspectList<LabelAspect>();
            Assert.Equal(expected, aspectList.GetType().GenericTypeArguments.First());
        }

        [Fact]
        public void GetAspectList_Should_Register_Existing_Entities()
        {
            var entity = new DefaultEntity("TestEntity", "all");
            entity.CreateLabelAspect("Test", 0, 0);
            _entityManager.Entities.Add(entity.ID, entity);
            _entityAspectManager.RegisterEntity(entity);
            var aspectList = _entityAspectManager.GetAspectList<LabelAspect>();
            Assert.Equal(1, aspectList.Count());
        }

        [Fact]
        public void GetAspectList_Should_Return_The_Same_List_For_A_Given_Aspect_Type()
        {
            var aspectList1 = _entityAspectManager.GetAspectList<LabelAspect>();
            var aspectList2 = _entityAspectManager.GetAspectList<LabelAspect>();
            Assert.Equal(aspectList1, aspectList2);
        }

        [Fact]
        public void GetUnfilteredAspectList_Should_Throw_If_ActiveAspectList_Is_Not_Initialized()
        {
            Assert.Throws<InvalidOperationException>(() => { _entityAspectManager.GetUnfilteredAspectList<LabelAspect>(); });
        }

        [Fact]
        public void ReleaseAspectList_Should_Throw_If_AspectFamily_Does_Not_Exist()
        {
            Assert.Throws<InvalidOperationException>(() => { _entityAspectManager.ReleaseAspectList<LabelAspect>(); });
        }

        [Fact]
        public void ReleaseAspectList_Should_Remove_All_Aspects_And_Remove_AspectFamily()
        {
            var entity = new DefaultEntity("TestEntity", "all");
            entity.CreateLabelAspect("Test", 0, 0);
            _entityManager.Entities.Add(entity.ID, entity);
            _entityAspectManager.RegisterEntity(entity);
            var aspectList = _entityAspectManager.GetAspectList<LabelAspect>();
            Assert.Equal(1, aspectList.Count());
            _entityAspectManager.ReleaseAspectList<LabelAspect>();
            Assert.Equal(0, aspectList.Count());
            var aspectList2 = _entityAspectManager.GetAspectList<LabelAspect>();
            Assert.NotEqual(aspectList, aspectList2);
        }
    }
}