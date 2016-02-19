using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSystem.Engine.Collections
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        Func<T> _creatorFunction;
        Action<T> _resetFunction;
        private ConcurrentBag<T> _items;

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public ObjectPool(Func<T> creatorFunction, Action<T> resetFunction = null)
        {
            if(creatorFunction == null)
            {
                throw new ArgumentException("creatorFunction can't be null");
            }

            _creatorFunction = creatorFunction;
            if (resetFunction != null)
                _resetFunction = resetFunction;
            _items = new ConcurrentBag<T>();
        }
        public T Get()
        {
            T item;
            if (_items.TryTake(out item)) return item;
            return _creatorFunction();
        }

        public void Put(T item)
        {
            _resetFunction?.Invoke(item);
            _items.Add(item);
        }
    }
}
