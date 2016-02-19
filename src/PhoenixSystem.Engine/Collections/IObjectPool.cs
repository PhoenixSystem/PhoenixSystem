namespace PhoenixSystem.Engine.Collections
{
    public interface IObjectPool<T>
    {
        int Count { get; }
        T Get();
        void Put(T item);
    }
}