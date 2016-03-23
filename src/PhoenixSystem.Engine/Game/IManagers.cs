namespace PhoenixSystem.Engine.Game
{
    public interface IManagers
    {
        void Register(IGameManager gameManager);
        void Add(IManager manager);
        void Update(ITickEvent tickEvent);
    }
}