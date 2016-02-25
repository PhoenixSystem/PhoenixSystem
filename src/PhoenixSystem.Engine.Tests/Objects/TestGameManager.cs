using PhoenixSystem.Engine.Channel;
using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.Tests.Objects
{
    internal class TestGameManager : BaseGameManager
    {
        public TestGameManager(IEntityAspectManager entityAspectManager, IEntityManager entityManager,
            IChannelManager channelManager) : base(entityAspectManager, entityManager, channelManager)
        {
        }
    }
}