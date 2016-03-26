namespace PhoenixSystem.Engine.Game
{
    public interface IManagerManager
    {
        void Register(IGameManager gameManager);
        void Add(IManager manager);
        void Update(ITickEvent tickEvent);
    }
}