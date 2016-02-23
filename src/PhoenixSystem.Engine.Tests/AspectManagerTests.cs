using PhoenixSystem.Engine;
using PhoenixSystem.Engine.Tests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class AspectManagerTests
    {
        IChannelManager cm;
        IAspectManager<LabelAspect> am;
        public AspectManagerTests()
        {
            cm = new BasicChannelManager();
            am = new AspectManager<LabelAspect>(cm);
        }

        [Fact]
        public void Get_Should_Return_A_New_Aspect_Of_Appropriate_Type()
        {
            var expected = typeof(LabelAspect);
            var e = new Entity("Test", "all");
            e.CreateLabelAspect("Test", 0, 0);
            
            var actual = am.Get(e);
            Assert.Equal(expected, actual.GetType());
            Assert.Equal(actual, am.Aspects.First());
        }

        [Fact]
        public void Get_Should_Add_To_ChannelAspects_If_Entity_Is_In_Current_Channel()
        {
            Assert.Equal(0, am.Aspects.Count());
            var e = new Entity("Test", "default").CreateLabelAspect("Test", 0, 0);
            am.Get(e);
            Assert.Equal(1, am.ChannelAspects.Count());
        }

        [Fact]
        public void Get_Should_Add_To_Aspects()
        {
            Assert.Equal(0, am.Aspects.Count());
            var e = new Entity("Test", "irrelevant").CreateLabelAspect("Test", 0, 0);
            var aspect = am.Get(e);
            Assert.Equal(1, am.Aspects.Count());
        }
        
    }
}
