
public class NetworkGameMode : IGameMode
{

    public void BeClient(){

    }

    public void BeServer(){

    }

    public void BeHost(){

    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void StartGame(int number)
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void DrawCard(int playerId)
    {
        throw new System.NotImplementedException();
    }

    public void TryPlayCard(int id, CardInfo card)
    {
        throw new System.NotImplementedException();
    }

    public void GameOver()
    {
        throw new System.NotImplementedException();
    }

    public bool AutoTryPlayCard(int id, CardInfo card)
    {
        throw new System.NotImplementedException();
    }
}