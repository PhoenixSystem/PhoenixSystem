namespace PhoenixSystem.Engine.Collections
{
    public interface IObjectPool
    {
        int Count { get; }
        object Get();
        T Get<T>();
        void Put(object item);
    }
}