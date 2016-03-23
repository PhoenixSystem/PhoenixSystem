using PhoenixSystem.Engine.Entity;
using PhoenixSystem.Engine.Game;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Tests.Objects
{
    internal class TestGameManager : BaseGameManager
    {
        public TestGameManager(IEntityAspectManager entityAspectManager, IEntityManager entityManager, ISystemManager systemManager)
            : base(entityAspectManager, entityManager, systemManager)
        {
        }
    }
}