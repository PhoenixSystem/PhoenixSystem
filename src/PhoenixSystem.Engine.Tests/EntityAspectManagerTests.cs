using System.Linq;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

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
            var expected = typeof (LabelAspect);
            var entity = new Entity.Entity("TestEntity", "all");
            entity.CreateLabelAspect("Test", 0, 0);
            _entityManager.Entities.Add(entity.ID, entity);
            _eam.RegisterEntity(entity);
            var aspectList = _eam.GetAspectList<LabelAspect>();
            Assert.Equal(expected, aspectList.First().GetType());
        }
    }
}