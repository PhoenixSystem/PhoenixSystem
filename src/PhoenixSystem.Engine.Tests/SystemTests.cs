using System;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;
using System.Collections.Generic;

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

        [Fact]
        public void System_CompareTo_Should_Sort_Correctly()
        {
            List<BaseSystem> systems = new List<BaseSystem>();

            var sys1 = new LabelSystem(cm, 30);
            var sys2 = new LabelSystem(cm, 10);
            var sys3 = new LabelSystem(cm, 40);

            systems.Add(sys1);
            systems.Add(sys2);
            systems.Add(sys3);

            systems.Sort();

            Assert.Equal(sys2.Priority, systems[0].Priority);
            Assert.Equal(sys1.Priority, systems[1].Priority);
            Assert.Equal(sys3.Priority, systems[2].Priority);
        }

        
    }
}