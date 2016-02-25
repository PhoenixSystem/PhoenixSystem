using System;
using System.Collections.Generic;
using PhoenixSystem.Engine.Game;

namespace PhoenixSystem.Engine.Entity
{
    public interface IEntityManager
    {
        IDictionary<Guid, IEntity> Entities { get; }
        void Register(IGameManager gameManager);
        IEntity Get(string name = "", string[] channels = null);
    }
}