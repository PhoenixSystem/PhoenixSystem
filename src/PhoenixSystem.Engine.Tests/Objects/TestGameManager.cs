using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Tests.Objects
{
    class TestGameManager : BaseGameManager
    {
        public TestGameManager(IEntityAspectManager entityAspectManager, IEntityManager entityManager, IChannelManager channelManager) : base(entityAspectManager,entityManager, channelManager)
        {

        }
    }
}
