using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Collections
{
    public interface IObjectPool<T>
    {
        T Get();
        void Put(T item);

        int Count { get; }
    }
}
