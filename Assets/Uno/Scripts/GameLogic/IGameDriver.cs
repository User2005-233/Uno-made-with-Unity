public interface IGameMode
{
    public abstract void Init();
    public abstract void StartGame(int number);
    public abstract void Update();
    public abstract void DrawCard(int playerId);
    public abstract void TryPlayCard(int id, CardInfo card);
    public abstract bool AutoTryPlayCard(int id, CardInfo card);    
    public abstract void GameOver();
}
