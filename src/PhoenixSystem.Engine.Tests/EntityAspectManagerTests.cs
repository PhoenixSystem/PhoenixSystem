using System.Linq;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;
using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityAspectManagerTests
    {
        private readonly DefaultEntityAspectManager _eam;
        private readonly IEntityManager _entityManager;

        public EntityAspectManagerTests()
        {
            var cm = new DefaultChannelManager();
            _entityManager = new EntityManager(cm);
            _eam = new DefaultEntityAspectManager(cm, _entityManager);
        }

        [Fact]
        public void GetAspectList_Should_Return_Collection_Of_Appropriate_Type_Of_Aspect()
        {
            var expected = typeof(LabelAspect);
            var aspectList = _eam.GetAspectList<LabelAspect>();            
            Assert.Equal(expected, aspectList.GetType().GenericTypeArguments.First());
        }

        [Fact]
        public void GetAspectList_Should_Register_Existing_Entities()
        {
            
            var entity = new DefaultEntity("TestEntity", "all");
            entity.CreateLabelAspect("Test", 0, 0);
            _entityManager.Entities.Add(entity.ID, entity);
            _eam.RegisterEntity(entity);
            var aspectList = _eam.GetAspectList<LabelAspect>();
            Assert.Equal(1, aspectList.Count());
        }

        [Fact]
        public void GetAspectList_Should_Return_The_Same_List_For_A_Given_Aspect_Type()
        {
            var aspectList1 = _eam.GetAspectList<LabelAspect>();
            var aspectList2 = _eam.GetAspectList<LabelAspect>();
            Assert.Equal(aspectList1, aspectList2);
        }

        [Fact]
        public void GetUnfilteredAspectList_Should_Throw_If_ActiveAspectList_Is_Not_Initialized()
        {
            Assert.Throws<ApplicationException>(() =>
            {
                _eam.GetUnfilteredAspectList<LabelAspect>();
            });
        }

        [Fact]
        public void ReleaseAspectList_Should_Throw_If_AspectFamily_Does_Not_Exist()
        {
            Assert.Throws<ApplicationException>(() =>
            {
                _eam.ReleaseAspectList<LabelAspect>();
            });
        }

        [Fact]
        public void ReleaseAspectList_Should_Remove_All_Aspects_And_Remove_AspectFamily()
        {
            var entity = new DefaultEntity("TestEntity", "all");
            entity.CreateLabelAspect("Test", 0, 0);
            _entityManager.Entities.Add(entity.ID, entity);
            _eam.RegisterEntity(entity);
            var aspectList = _eam.GetAspectList<LabelAspect>();
            Assert.Equal(1, aspectList.Count());
            _eam.ReleaseAspectList<LabelAspect>();
            Assert.Equal(0, aspectList.Count());
            var aspectList2 = _eam.GetAspectList<LabelAspect>();
            Assert.NotEqual<IEnumerable<LabelAspect>>(aspectList, aspectList2);
        }

    }
}