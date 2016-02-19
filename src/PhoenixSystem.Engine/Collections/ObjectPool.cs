using System;
using System.Collections.Concurrent;

namespace PhoenixSystem.Engine.Collections
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly Func<T> _creatorFunction;
        private readonly ConcurrentBag<T> _items;
        private readonly Action<T> _resetFunction;

        public ObjectPool(Func<T> creatorFunction, Action<T> resetFunction = null)
        {
            if (creatorFunction == null)
            {
                throw new ArgumentException("creatorFunction can't be null");
            }

            _creatorFunction = creatorFunction;

            if (resetFunction != null)
            {
                _resetFunction = resetFunction;
            }

            _items = new ConcurrentBag<T>();
        }

        public int Count => _items.Count;

        public T Get()
        {
            T item;
            return _items.TryTake(out item) ? item : _creatorFunction();
        }

        public void Put(T item)
        {
            _resetFunction?.Invoke(item);
            _items.Add(item);
        }
    }
}