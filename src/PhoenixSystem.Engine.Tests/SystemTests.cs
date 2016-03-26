using System.Collections.Generic;
using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Collections;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.System;
using PhoenixSystem.Engine.Tests.Objects;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class SystemTests
    {
        private readonly IChannelManager _channelManager;
        private readonly ISystemManager _systemManager;

        public SystemTests()
        {
            _channelManager = new ChannelManager();
            _systemManager = new SystemManager(_channelManager);
        }

        [Fact]
        public void Should_Be_Active_On_Start()
        {
            var system = new LabelSystem(_channelManager, 10, new[] {"default"});
            Assert.False(system.IsActive);
            system.Start();
            Assert.True(system.IsActive);
        }

        [Fact]
        public void Should_Be_Not_Active_On_Stop()
        {
            var system = new LabelSystem(_channelManager, 10, new[] {"default"});
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
            const bool expected = true;
            var em = new EntityManager(_channelManager, new EntityPool());
            var tgm = new TestGameManager(new DefaultEntityAspectManager(_channelManager, em), em, _systemManager);
            var system = new LabelSystem(_channelManager, 10, new[] {"default"});
            system.AddedToGameManager += (s, e) => raised = true;
            system.AddToGameManager(tgm);
            Assert.Equal(expected, raised);
        }

        [Fact]
        public void Should_Notify_RemovedFromGameManager_When_Removed_From_GameManager()
        {
            var raised = false;
            const bool expected = true;
            var em = new EntityManager(_channelManager, new EntityPool());
            var tgm = new TestGameManager(new DefaultEntityAspectManager(_channelManager, em), em, _systemManager);
            var system = new LabelSystem(_channelManager, 10, new[] {"default"});

            system.AddToGameManager(tgm);
            system.RemovedFromGameManager += (s, e) => raised = true;
            system.RemoveFromGameManager(tgm);
            Assert.Equal(expected, raised);
        }

        [Fact]
        public void System_CompareTo_Should_Sort_Correctly()
        {
            var systems = new List<BaseSystem>();

            var sys1 = new LabelSystem(_channelManager, 30);
            var sys2 = new LabelSystem(_channelManager, 10);
            var sys3 = new LabelSystem(_channelManager, 40);

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