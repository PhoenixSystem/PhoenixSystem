using System.Linq;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class AspectTests
    {
        [Fact]
        public void Delete_Should_Notify_Deleted_Event()
        {
            var expected = true;
            var notified = false;
            var aspect = new LabelAspect();
            aspect.Deleted += (s, args) => notified = true;
            aspect.Delete();
            Assert.Equal(expected, notified);
        }

        [Fact]
        public void Delete_Should_Set_IsDeleted()
        {
            var expected = true;
            var aspect = new LabelAspect();
            Assert.False(aspect.IsDeleted);
            aspect.Delete();
            Assert.Equal(expected, aspect.IsDeleted);
        }

        [Fact]
        public void Init_Should_Apply_Entity_Channels_To_Aspect()
        {
            var expected = new[] {"one", "two", "three"};
            var e = new DefaultEntity("Test", expected).CreateLabelAspect("Label", 0, 0);
            var aspect = new LabelAspect();
            aspect.Init(e);
            Assert.True(aspect.Channels.All(s => expected.Contains(s)));
        }

        [Fact]
        public void Init_Should_Create_Pointers_To_Entity_Components_From_Aspect()
        {
            var e = new DefaultEntity("Test", "default").CreateLabelAspect("Label", 0, 0);
            var aspect = new LabelAspect();
            aspect.Init(e);
            foreach (var component in e.Components.Values)
            {
                Assert.Equal(component, aspect.Components[component.GetType()]);
            }
        }

        [Fact]
        public void Reset_Should_Clear_Components_and_Channels()
        {
            var e = new DefaultEntity("Test", "default").CreateLabelAspect("Label", 0, 0);
            var aspect = new LabelAspect();
            aspect.Init(e);
            Assert.True(aspect.Components.Count > 0);
            Assert.True(aspect.Channels.Count > 0);
            aspect.Reset();
            Assert.Equal(0, aspect.Components.Count);
            Assert.Equal(0, aspect.Channels.Count);
        }
    }
}