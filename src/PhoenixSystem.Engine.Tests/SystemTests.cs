using PhoenixSystem.Engine.Tests.Objects;
using System;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class SystemTests
    {
        IChannelManager cm;

        public SystemTests()
        {
            cm = new BasicChannelManager();
        }

        [Fact]
        public void Should_Be_Active_On_Start()
        {
            LabelSystem system = new LabelSystem(cm, 10, new string[] { "default" });
            Assert.False(system.IsActive);
            system.Start();
            Assert.True(system.IsActive);
        }

        [Fact]
        public void Should_Be_Not_Active_On_Stop()
        {
            LabelSystem system = new LabelSystem(cm, 10, new string[] { "default" });
            Assert.False(system.IsActive);
            system.Start();
            Assert.True(system.IsActive);
            system.Stop();
            Assert.False(system.IsActive);
        }

        [Fact]
        public void Should_Notify_AddedToGameManager_When_Added_To_GameManager()
        {
            bool raised = false;
            bool expected = true;
            TestGameManager tgm = new TestGameManager(new BasicEntityAspectManager(cm), new EntityManager(cm), cm);
            LabelSystem system = new LabelSystem(cm, 10, new string[] { "default" });
            system.AddedToGameManager += (s,e) => raised = true;
            system.AddToGameManager(tgm);
            Assert.Equal(expected, raised);
        }

        [Fact]
        public void Should_Notify_RemovedFromGameManager_When_Removed_From_GameManager()
        {
            bool raised = false;
            bool expected = true;
            TestGameManager tgm = new TestGameManager(new BasicEntityAspectManager(cm), new EntityManager(cm), cm);
            LabelSystem system = new LabelSystem(cm, 10, new string[] { "default" });
            
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
