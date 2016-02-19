using System;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class SystemTests
    {
        private readonly IChannelManager cm;

        public SystemTests()
        {
            cm = new BasicChannelManager();
        }

        [Fact]
        public void Should_Be_Active_On_Start()
        {
            var system = new LabelSystem(cm, 10, new[] {"default"});
            Assert.False(system.IsActive);
            system.Start();
            Assert.True(system.IsActive);
        }

        [Fact]
        public void Should_Be_Not_Active_On_Stop()
        {
            var system = new LabelSystem(cm, 10, new[] {"default"});
            Assert.False(system.IsActive);
            system.Start();
            Assert.True(system.IsActive);
            system.Stop();
            Assert.False(system.IsActive);
        }

        [Fact]
        public void Should_Notify_AddedToGameManager_When_Added_To_GameManager()
        {
            var raised = false;
            var expected = true;
            var tgm = new TestGameManager(new BasicEntityAspectManager(cm), new EntityManager(cm), cm);
            var system = new LabelSystem(cm, 10, new[] {"default"});
            system.AddedToGameManager += (s, e) => raised = true;
            system.AddToGameManager(tgm);
            Assert.Equal(expected, raised);
        }

        [Fact]
        public void Should_Notify_RemovedFromGameManager_When_Removed_From_GameManager()
        {
            var raised = false;
            var expected = true;
            var tgm = new TestGameManager(new BasicEntityAspectManager(cm), new EntityManager(cm), cm);
            var system = new LabelSystem(cm, 10, new[] {"default"});

            system.AddToGameManager(tgm);
            system.RemovedFromGameManager += (s, e) => raised = true;
            system.RemoveFromGameManager(tgm);
            Assert.Equal(expected, raised);
        }

        [Theory]
        public void System_CompareTo_Should_Sort_Correctly()
        {
            throw new NotImplementedException();
        }
    }
}