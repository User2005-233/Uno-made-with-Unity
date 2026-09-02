using UnityEngine;
using System.Collections.Generic;
public class SinglePlayerGameMode : MonoBehaviour, IGameMode
{

 #region Variables

    private readonly UnoLogic _logic = new();
    public List<int> roomPlayerLoop;
    private bool isGameStart = false;
    private int codeMe = 0;

    #endregion

    #region Events

    /// <summary>
    /// Register event listeners
    /// </summary>
    private void Awake()
    {
        _logic.gameMode = this;
        AddListeners();
    }

    void AddListeners(){
        EventManager.Instance.AddListener<CardPlayEvent>(OnCardPlayEvent);
        EventManager.Instance.AddActionListener<PauseGameEvent>(OnPauseGameEvent);
        
    }

    void OnCardPlayEvent(IEventMessage message){
        var msg = (CardPlayEvent)message;
        Debug.Log($"Card played: {msg.Card}");
    }

    void OnPauseGameEvent(){
        Debug.Log("Game paused");
    }

    #endregion


    #region StartGame
    
    /// <summary>
    /// Add players to the game. This will be called later when we have a proper player system.
    /// For now, just add the player codes.
    /// </summary>
    /// <param name="number">Number of players to add</param>
    public void AddPlayer(int number)
    {
        roomPlayerLoop.Clear();
        for (int i = 0; i < number; i++)
        {
            roomPlayerLoop.Add(i);
        }
        Debug.Log($"Added {number} players");
    }

    /// <summary>
    /// Start the game with the specified number of players.
    /// </summary>
    /// <param name="number">Number of players</param>
    public void StartGame(int number)
    {
        AddPlayer(number);
        _logic.ShufflePlayerLoop();
        _logic.AddPlayerEntity();
        _logic.DealCards(roomPlayerLoop);
        _logic.StartGame(roomPlayerLoop);
        Debug.Log("Cards dealt");
        Debug.Log("Game started");
        isGameStart = true;
    }

    /// <summary>
    /// Initialize the game logic. This will be called when the game starts.
    /// </summary>
    public void Init()
    {
        _logic.Init();
        _logic.InitializeDeck();
        _logic.WashDeck();
    }

    #endregion

    #region Runtime

    public void Update()
    {
        if (isGameStart)
        {
            _logic.Update();
        }
    }
    
    //main logic play card interface
    public void TryPlayCard(int id, CardInfo card)
    {
        if(_logic.GetCurrentPlayer() == id && _logic.CheckPlayable(card))
        {
            // TODO: play card
            if(_logic.TryPlayCard(id, card))
                CardFunction(card);
            else
                EventManager.Instance.SendMessage(new ShowHintEvent("Error", "Play card failed"));
        }
        else
        {
            // TODO: show error
            EventManager.Instance.SendMessage(new ShowHintEvent("Error", "Card is not playable"));
        }
    }

    //let the card function
    void CardFunction(CardInfo card)
    {
        switch(card.type)
        {
            case CardType.Number:
                break;
            case CardType.Skip:
                _logic.SkipTurn();
                break;
            case CardType.Reverse:
                _logic.ReverseDirection();
                break;
            case CardType.PlusTwo:
                _logic.Draw2CardToNow();
                break;
            case CardType.Wild:
                // TODO: choose color
                break;
            case CardType.WildDrawFour:
                // TODO: choose color
                break;
        }
    }




    public void DrawCard(int playerId)
    {
        _logic.DrawCard(playerId);
        Debug.Log($"Player {playerId} drew a card");
    }

    public void GameOver()
    {
        isGameStart = false;
        // _logic.GameOver(); // TODO: implement game over logic
        Debug.Log("Game over");
    }

    #endregion
}
