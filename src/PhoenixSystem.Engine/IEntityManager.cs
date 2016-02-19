using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public interface IEntityManager
    {
        IDictionary<Guid, IEntity> Entities { get; }
        void Register(IGameManager gameManager);
        IEntity Get(string name = "", string[] channels = null);
    }
}