using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine
{
    public interface IEntityManager
    {
        void Register(IGameManager gameManager);
        IEntity Get(string name, string[] channels);
        IDictionary<Guid, IEntity> Entities { get; }

       

    }
}


