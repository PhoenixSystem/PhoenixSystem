using PhoenixSystem.Engine.Tests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class AspectTests
    {
        public AspectTests()
        {

        }

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
            var expected = new string[] { "one", "two", "three" };
            var e = new Entity("Test", expected).CreateLabelAspect("Label", 0, 0);
            var aspect = new LabelAspect();
            aspect.Init(e);
            Assert.True(aspect.Channels.All(s => expected.Contains(s)));
        }

        [Fact]
        public void Init_Should_Create_Pointers_To_Entity_Components_From_Aspect()
        {
            var e = new Entity("Test", "default").CreateLabelAspect("Label", 0, 0);
            var aspect = new LabelAspect();
            aspect.Init(e);
            foreach(var component in e.Components.Values)
            {
                Assert.Equal<IComponent>(component, aspect.Components[component.GetType().Name]);
            }
        }

        
    }
}
