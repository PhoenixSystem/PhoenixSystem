using System;
using System.Collections.Concurrent;

namespace PhoenixSystem.Engine.Collections
{
    public class ObjectPool : IObjectPool
    {
        private readonly Func<object> _creatorFunction;
        private readonly ConcurrentBag<object> _items;
        private readonly Action<object> _resetFunction;

        public ObjectPool(Func<object> creatorFunction, Action<object> resetFunction = null)
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

            _items = new ConcurrentBag<object>();
        }

        public int Count => _items.Count;

        public object Get()
        {
            object item;
            return _items.TryTake(out item) ? item : _creatorFunction();
        }

        public void Put(object item)
        {
            _resetFunction?.Invoke(item);
            _items.Add(item);
        }

        public T Get<T>()
        {
            return (T) Get();
        }
    }
}