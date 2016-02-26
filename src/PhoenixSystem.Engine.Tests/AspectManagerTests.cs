using System.Linq;
using PhoenixSystem.Engine.Aspect;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class AspectManagerTests
    {
        private readonly IAspectManager _aspectManager;

        public AspectManagerTests()
        {
            var channelManager = new ChannelManager();
            _aspectManager = new AspectManager(channelManager, new AspectPool<LabelAspect>());
        }

        [Fact]
        public void Get_Should_Return_A_New_Aspect_Of_Appropriate_Type()
        {
            var expected = typeof (LabelAspect);
            var e = new DefaultEntity("Test", "all");
            e.CreateLabelAspect("Test", 0, 0);

            var actual = _aspectManager.Get(e);
            Assert.Equal(expected, actual.GetType());
            Assert.Equal(actual, _aspectManager.Aspects.First());
        }

        [Fact]
        public void Get_Should_Add_To_ChannelAspects_If_Entity_Is_In_Current_Channel()
        {
            Assert.Equal(0, _aspectManager.Aspects.Count());
            var e = new DefaultEntity("Test", "default").CreateLabelAspect("Test", 0, 0);
            _aspectManager.Get(e);
            Assert.Equal(1, _aspectManager.ChannelAspects.Count());
        }

        [Fact]
        public void Get_Should_Not_Add_To_ChannelAspects_If_Entity_Is_Not_In_Current_Channel()
        {
            Assert.Equal(0, _aspectManager.Aspects.Count());
            var e = new DefaultEntity("test", "not_default").CreateLabelAspect("test", 0, 0);
            _aspectManager.Get(e);
            Assert.Equal(0, _aspectManager.ChannelAspects.Count());
            Assert.Equal(1, _aspectManager.Aspects.Count());
        }

        [Fact]
        public void Get_Should_Add_To_Aspects()
        {
            Assert.Equal(0, _aspectManager.Aspects.Count());
            var e = new DefaultEntity("Test", "irrelevant").CreateLabelAspect("Test", 0, 0);
            _aspectManager.Get(e);
            Assert.Equal(1, _aspectManager.Aspects.Count());
        }

        [Fact]
        public void Aspect_Should_Be_Removed_From_Channel_List_When_In_Channel_And_Deleted()
        {
            var e = new DefaultEntity("Test", "default").CreateLabelAspect("Test", 0, 0);
            var aspect = _aspectManager.Get(e);
            Assert.Equal(1, _aspectManager.ChannelAspects.Count());
            Assert.Equal(aspect, _aspectManager.ChannelAspects.First());
            aspect.Delete();
            Assert.Equal(0, _aspectManager.ChannelAspects.Count());
        }

        [Fact]
        public void Aspect_Should_Be_Removed_From_Aspects_List_When_Deleted()
        {
            var e = new DefaultEntity("Test", "not_channel").CreateLabelAspect("Test", 0, 0);
            Assert.Equal(0, _aspectManager.ChannelAspects.Count());
            var aspect = _aspectManager.Get(e);
            Assert.Equal(1, _aspectManager.Aspects.Count());
            Assert.Equal(aspect, _aspectManager.Aspects.First());
            aspect.Delete();
            Assert.Equal(0, _aspectManager.Aspects.Count());
        }
    }
}