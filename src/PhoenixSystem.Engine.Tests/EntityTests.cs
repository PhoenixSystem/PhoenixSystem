using PhoenixSystem.Engine.Tests.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityTests
    {
        public EntityTests()
        {

        }

        [Fact]
        public void Is_Deleted_Should_Be_False_After_Delete()
        {
            var e = new Entity("Test", new string[] { "test" });
            e.Delete();
            Assert.Equal(true, e.IsDeleted);
        }

        [Fact]
        public void Entity_Should_Throw_Exception_If_Channels_Array_Is_Null()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var e = new Entity("Test", null);
            });
        }

        [Fact]
        public void Entity_Should_Throw_Exception_If_Channel_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var e = new Entity("Test", new string[1]);
            });
        }

        [Fact]
        public void AddComponent_Should_Increase_Component_Count()
        {
            var e = new Entity("Test", new string[] { "teset" });
            Assert.Equal(0, e.Components.Count);
            e.AddComponent(new StringComponent() { Value = "test" });
            Assert.Equal(1, e.Components.Count);
        }

        [Fact]
        public void AddComponent_Should_Throw_Exception_If_Component_Exists_Already()
        {
            var e = new Entity("test", new string[] { "tests" });
            e.AddComponent(new StringComponent() { Value = "label" });
            Assert.Throws<ApplicationException>(() => e.AddComponent(new StringComponent() { Value = "who cares" }));
        }

        [Fact]
        public void AddComponent_Should_Overwrite_Component_If_Parameter_Set()
        {
            var labelOne = "label one";
            var labelTwo = "label two";
            var e = new Entity("test", new string[] { "test" });
            e.AddComponent(new StringComponent() { Value = labelOne });

            Assert.Equal(labelOne, ((StringComponent)e.Components[typeof(StringComponent).Name]).Value);

            e.AddComponent(new StringComponent() { Value = labelTwo }, true);

            Assert.Equal(labelTwo, ((StringComponent)e.Components[typeof(StringComponent).Name]).Value);
           
        }

        [Fact]
        public void AddComponent_Should_Raise_OnComponentAdded()
        {
            bool called = false;
            var e = new Entity("name", new string[] { "test" });
            e.ComponentAdded += (s, arg) =>
            {
                called = true;
            };
            e.AddComponent(new StringComponent() { Value = "test" });
            Assert.True(called);
        }

        [Fact]
        public void IsInChannel_Should_Validate_Entity_Channel()
        {
            string channelName = "channelName";
            var e = new Entity("test", new string[] { channelName });
            Assert.True(e.IsInChannel(channelName));
        }

        [Fact]
        public void IsInChannel_Should_Be_False_For_Incorrect_Channel()
        {
            string channelName = "channelName";
            string notChannelName = "notChannelname";
            var e = new Entity("test", new string[] { channelName });
            Assert.False(e.IsInChannel(notChannelName));
        }

        [Fact]
        public void Can_Find_Component_By_Type()
        {
            var e = new Entity("name", new string[] { "channel" });
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent(typeof(StringComponent)));
        }

        [Fact]
        public void Can_Find_Component_By_String()
        {
            var e = new Entity("name", new string[] { "channel" });
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent(typeof(StringComponent).Name));

        }
        
        [Fact]
        public void Can_Find_Multiple_Components_By_Type()
        {
            var e = new Entity("name", new string[] { "channel" });
            e.AddComponent(new StringComponent());
            e.AddComponent(new XYComponent());
            Assert.True(e.HasComponents(new Type[] { typeof(StringComponent), typeof(XYComponent) }));
        }

        [Fact]
        public void Can_Find_Multiple_Components_By_String()
        {
            var e = new Entity("name", new string[] { "channel" });
            e.AddComponent(new StringComponent());
            e.AddComponent(new XYComponent());
            Assert.True(e.HasComponents(new String[] { typeof(StringComponent).Name, typeof(XYComponent).Name }));
        }

        [Fact]
        public void Should_Raise_Deleted_Event_When_Deletedd()
        {
            var e = new Entity("name", new string[] { "channel" });
            var called = false;
            e.Deleted += (s, ea) =>
            {
                called = true;
            };
            e.Delete();
            Assert.True(called);
        }
    }
}
