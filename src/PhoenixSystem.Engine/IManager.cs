namespace PhoenixSystem.Engine
{
    public interface IManager
    {
        void Update();
        void Register(IGameManager gameManager);
    }
}