using System.Linq;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class AspectManagerTests
    {
        private readonly IAspectManager _am;
        private readonly IChannelManager _cm;

        public AspectManagerTests()
        {
            _cm = new BasicChannelManager();
            _am = new AspectManager<LabelAspect>(_cm);
        }

        [Fact]
        public void Get_Should_Return_A_New_Aspect_Of_Appropriate_Type()
        {
            var expected = typeof(LabelAspect);
            var e = new Entity("Test", "all");
            e.CreateLabelAspect("Test", 0, 0);

            var actual = _am.Get(e);
            Assert.Equal(expected, actual.GetType());
            Assert.Equal(actual, _am.Aspects.First());
        }

        [Fact]
        public void Get_Should_Add_To_ChannelAspects_If_Entity_Is_In_Current_Channel()
        {
            Assert.Equal(0, _am.Aspects.Count());
            var e = new Entity("Test", "default").CreateLabelAspect("Test", 0, 0);
            _am.Get(e);
            Assert.Equal(1, _am.ChannelAspects.Count());
        }

        [Fact]
        public void Get_Should_Not_Add_To_ChannelAspects_If_Entity_Is_Not_In_Current_Channel()
        {
            Assert.Equal(0, _am.Aspects.Count());
            var e = new Entity("test", "not_default").CreateLabelAspect("test", 0, 0);
            _am.Get(e);
            Assert.Equal(0, _am.ChannelAspects.Count());
            Assert.Equal(1, _am.Aspects.Count());
        }

        [Fact]
        public void Get_Should_Add_To_Aspects()
        {
            Assert.Equal(0, _am.Aspects.Count());
            var e = new Entity("Test", "irrelevant").CreateLabelAspect("Test", 0, 0);
            var aspect = _am.Get(e);
            Assert.Equal(1, _am.Aspects.Count());
        }

        [Fact]
        public void Aspect_Should_Be_Removed_From_Channel_List_When_In_Channel_And_Deleted()
        {
            var e = new Entity("Test", "default").CreateLabelAspect("Test", 0, 0);
            var aspect = _am.Get(e);
            Assert.Equal(1, _am.ChannelAspects.Count());
            Assert.Equal(aspect, _am.ChannelAspects.First());
            aspect.Delete();
            Assert.Equal(0, _am.ChannelAspects.Count());
        }

        [Fact]
        public void Aspect_Should_Be_Removed_From_Aspects_List_When_Deleted()
        {
            var e = new Entity("Test", "not_channel").CreateLabelAspect("Test", 0, 0);
            Assert.Equal(0, _am.ChannelAspects.Count());
            var aspect = _am.Get(e);
            Assert.Equal(1, _am.Aspects.Count());
            Assert.Equal(aspect, _am.Aspects.First());
            aspect.Delete();
            Assert.Equal(0, _am.Aspects.Count());
        }

    }
}
