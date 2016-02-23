using PhoenixSystem.Engine.Tests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityAspectManagerTests
    {
        IEntityManager em;
        BasicEntityAspectManager _eam;
        public EntityAspectManagerTests()
        {
            var cm = new BasicChannelManager();
            em = new EntityManager(cm);
            _eam = new BasicEntityAspectManager(cm, em);
        }

        [Fact]
        public void GetAspectList_Should_Return_Collection_Of_Appropriate_Type_Of_Aspect()
        {
            var expected = typeof(LabelAspect);
            var entity = new Entity("TestEntity", "all");
            entity.CreateLabelAspect("Test", 0, 0);
            em.Entities.Add(entity.ID,entity);
            _eam.RegisterEntity(entity);
            var aspectList = _eam.GetAspectList<LabelAspect>();
            Assert.Equal(expected, aspectList.First().GetType());
        }

        
    }
}
