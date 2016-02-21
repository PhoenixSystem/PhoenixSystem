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
        BasicEntityAspectManager _eam;
        public EntityAspectManagerTests()
        {
            var cm = new BasicChannelManager();
            var em = new EntityManager(cm);
            _eam = new BasicEntityAspectManager(cm, em);
        }

        [Fact]
        public void GetAspectList_Should_Return_Collection_Of_Appropriate_Type_Of_Aspect()
        {
            
        }
        
    }
}
