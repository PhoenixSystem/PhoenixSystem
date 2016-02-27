using System;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Extensions;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Is_Deleted_Should_Be_False_After_Delete()
        {
            var e = new DefaultEntity("Test", "test");
            e.Delete();
            Assert.Equal(true, e.IsDeleted);
        }

        [Fact]
        public void Entity_Should_Throw_Exception_If_Channel_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() => { new DefaultEntity("Test", new string[1]); });
        }

        [Fact]
        public void AddComponent_Should_Increase_Component_Count()
        {
            var e = new DefaultEntity("Test", "teset");
            Assert.Equal(0, e.Components.Count);
            e.AddComponent(new StringComponent {Value = "test"});
            Assert.Equal(1, e.Components.Count);
        }

        [Fact]
        public void AddComponent_Should_Throw_Exception_If_Component_Exists_Already()
        {
            var e = new DefaultEntity("test", "tests");
            e.AddComponent(new StringComponent {Value = "label"});
            Assert.Throws<InvalidOperationException>(() => e.AddComponent(new StringComponent {Value = "who cares"}));
        }

        [Fact]
        public void AddComponent_Should_Overwrite_Component_If_Parameter_Set()
        {
            var labelOne = "label one";
            var labelTwo = "label two";
            var e = new DefaultEntity("test", "test");
            e.AddComponent(new StringComponent {Value = labelOne});

            Assert.Equal(labelOne, ((StringComponent) e.Components[typeof (StringComponent)]).Value);

            e.AddComponent(new StringComponent {Value = labelTwo}, true);

            Assert.Equal(labelTwo, ((StringComponent) e.Components[typeof (StringComponent)]).Value);
        }

        [Fact]
        public void AddComponent_Should_Raise_OnComponentAdded()
        {
            var called = false;
            var e = new DefaultEntity("name", "test");
            e.ComponentAdded += (s, arg) => { called = true; };
            e.AddComponent(new StringComponent {Value = "test"});
            Assert.True(called);
        }

        [Fact]
        public void IsInChannel_Should_Validate_Entity_Channel()
        {
            var channelName = "channelName";
            var e = new DefaultEntity("test", channelName);
            Assert.True(e.IsInChannel(channelName));
        }

        [Fact]
        public void IsInChannel_Should_Be_False_For_Incorrect_Channel()
        {
            var channelName = "channelName";
            var notChannelName = "notChannelname";
            var e = new DefaultEntity("test", channelName);
            Assert.False(e.IsInChannel(notChannelName));
        }

        [Fact]
        public void Can_Find_Component_By_Type()
        {
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent(typeof (StringComponent)));
        }

        [Fact]
        public void Can_Find_Component_By_String()
        {
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent(typeof (StringComponent)));
        }

        [Fact]
        public void Can_Find_Multiple_Components_By_Type()
        {
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            e.AddComponent(new XYComponent());
            Assert.True(e.HasComponents(new[] {typeof (StringComponent), typeof (XYComponent)}));
        }

        [Fact]
        public void Can_Find_Multiple_Components_By_String()
        {
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            e.AddComponent(new XYComponent());
            Assert.True(e.HasComponents(new[] {typeof (StringComponent), typeof (XYComponent)}));
        }

        [Fact]
        public void Should_Raise_Deleted_Event_When_Deletedd()
        {
            var e = new DefaultEntity("name", "channel");
            var called = false;
            e.Deleted += (s, ea) => { called = true; };
            e.Delete();
            Assert.True(called);
        }

        [Fact]
        public void Should_Remove_A_Component_By_Type()
        {
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent<StringComponent>());
            Assert.True(e.RemoveComponent<StringComponent>());
            Assert.False(e.HasComponent<StringComponent>());
        }

        [Fact]
        public void Should_Remove_A_Component_By_String()
        {
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent<StringComponent>());
            Assert.True(e.RemoveComponent(typeof (StringComponent)));
            Assert.False(e.HasComponent<StringComponent>());
        }

        [Fact]
        public void Should_Raise_Component_Removed_Event()
        {
            var raised = false;
            var e = new DefaultEntity("name", "channel");
            e.AddComponent(new StringComponent());
            Assert.True(e.HasComponent<StringComponent>());
            e.ComponentRemoved += (s, ea) =>
            {
                raised = true;
                Assert.IsType<StringComponent>(ea.Component);
                Assert.Same(e, s);
            };
            Assert.True(e.RemoveComponent<StringComponent>());
            Assert.True(raised);
        }

        [Fact]
        public void Clone_Should_Create_a_Copy()
        {
            var name = "name";
            string[] channels = {"channel1", "channel2"};
            var e = new DefaultEntity(name, channels);
            e.AddComponent(new StringComponent()).AddComponent(new XYComponent());
            var clone = e.Clone();
            Assert.NotSame(clone, e);
            Assert.NotSame(e.GetComponent<StringComponent>(), clone.GetComponent<StringComponent>());
            Assert.NotSame(e.GetComponent<XYComponent>(), clone.GetComponent<XYComponent>());
            Assert.NotSame(e.Channels, clone.Channels);
        }
    }
}