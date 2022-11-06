public interface IGameManager
{
    ManagerStatus status { get; }

    public void Startup();
}
