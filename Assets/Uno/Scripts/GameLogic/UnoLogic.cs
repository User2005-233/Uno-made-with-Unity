using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

using Random = UnityEngine.Random;


public class UnoLogic
{
    #region Variables

    //card info loading usage
    public CardType cardType;
    public CardColor cardColor;
    public int cardNum = -1;
    Color hovered = new Color(0.75f, 0.75f, 0.75f);
    private Dictionary<int, List<CardInfo>> playerHands = new Dictionary<int, List<CardInfo>>();
    List<CardInfo> CardInfoList = new List<CardInfo>();
    Queue<CardInfo> randCardQ = new Queue<CardInfo>();
    protected int DeckSize = 108;
    GameObject PlayerParent;

    //runtime variables
    CardType CurrentType = CardType.None;
    CardColor CurrentColor = CardColor.None;
    int CurrentNum = -1;
    CardInfo currentCardInfo;
    float gameTime = 300f;
    float roundTime = 15f;
    TickTimer gameTimer;
    TickTimer roundTimer;
    bool isGameStart = false;
    bool clockWise = true;
    int currentPlayerRound;


    //all ui assets loading presets

    //Players data/setting variables
    public List<int> playerLoop;
    public IGameMode gameMode;
    public int selfCode;

    #endregion

    #region Init

    /// <summary>
    /// for now: initialize game timer
    /// </summary>
    public void Init()
    {
        // TODO: initialize single player game
        gameTimer = new TickTimer(gameTime);
        roundTimer = new TickTimer(roundTime);
    }

    /// <summary>
    /// Initialize the deck
    /// </summary>
    public void InitializeDeck()
    {
        // 添加数字卡牌
        for (int i = 0; i < 2; ++i)
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                for (int j = 1; j < 10; ++j)
                {
                    CardInfo info = new(color, CardType.Number, j);

                    CardInfoList.Add(info);
                }
            }
        }

        // 添加0点卡牌        
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            CardInfo info = new(color, CardType.Number, 0);

            CardInfoList.Add(info);
        }

        // 添加功能卡牌
        for (int i = 0; i < 2; ++i)
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                foreach (ColorFunc func in Enum.GetValues(typeof(ColorFunc)))

                {
                    CardInfo info = new(color, func);

                    CardInfoList.Add(info);
                }
            }
        }

        // 添加特殊卡牌
        for (int i = 0; i < 4; ++i)
        {
            foreach (WildFunc func in Enum.GetValues(typeof(WildFunc)))
            {
                CardInfo info = new(func);

                CardInfoList.Add(info);
            }
        }
    }

    /// <summary>
    /// Initialize the deck random queue
    /// </summary>
    public void WashDeck()
    {
        List<CardInfo> DeckTemp = new(CardInfoList);
        for (int i = 0; i < DeckSize; i++)
        {
            int index = Random.Range(0, DeckSize - i);
            CardInfo card = DeckTemp[index];

            randCardQ.Enqueue(card);

            DeckTemp.Remove(card);
        }
    }

    public Dictionary<int, List<CardInfo>> DealCards(List<int> roomPlayerLoop)
    {
        foreach(int code in roomPlayerLoop)
        {
            // 发牌给每个玩家
            for (int i = 0; i < 7; i++)
            {
                var card = GetRandomCard();
                playerHands[code].Add(card);
                EventManager.Instance.SendMessage(new CardAddEvent(code, card));
            }
        }
        return playerHands;
    }

    public void AddPlayerEntity(){
        foreach(var player in playerHands.Keys)
        {
            if(player != selfCode)
            {
                EventManager.Instance.SendMessage(new PlayerEntityAddEvent(player));
            }
            else{
                EventManager.Instance.SendMessage(new SelfPlayerAddEvent(player));
            }
        }
    }

    public List<int> ShufflePlayerLoop(){
        playerLoop.Clear();
        playerLoop = new List<int>((gameMode as SinglePlayerGameMode).roomPlayerLoop);
        Shuffler.Shuffle<int>(playerLoop);
        return playerLoop;
    }

    public void ReadPlayerLoop(List<int> list){
        //tell playermanager to set player loop
        playerLoop = list;
    }

    #endregion

    #region Runtime Logic

    public void StartGame(List<int> roomPlayerLoop)
    {
        // TODO: start game
        isGameStart = true;
        gameTimer.Start();
        // EventManager.Instance.SendMessage(new SetPlayerLoopEvent(playerLoop));
        // UIManager.Instance.ShowPanel<GamePanel>();
    }

    public void Update()
    {
        // TODO: update game logic
        if (isGameStart)
        {
            gameTimer.Tick(Time.deltaTime);
            roundTimer.Tick(Time.deltaTime);
            if(gameTimer.IsCompleted){
                EndGame(true);
                return;
            }
            if(roundTimer.IsCompleted){
                AutoPlay();
            }
        }
    }

    public void ReverseDirection()
    {
        clockWise = !clockWise;
    }

    public bool TryPlayCard(int playerId, CardInfo card)
    {
        // TODO: request to play card
        if(playerId == currentPlayerRound)
        {
            playerHands[playerId].Remove(card);
            if(CheckWin())
            {
                EndGame(false);
                return true;
            }
        }
        return false;
    }

    public int GetCurrentPlayer(){
        return currentPlayerRound;
    }

    bool CheckCardInHand(int id, CardInfo info){
        //TODO: 检查卡牌是否在手牌中
        return playerHands[id].Contains(info);
    }

    public bool CheckPlayable(CardInfo card)
    {
        if(card.type == CardType.Wild || card.type == CardType.WildDrawFour)
        {
            return true;
        }
        if(card.type == currentCardInfo.type || card.color == currentCardInfo.color || card.number == currentCardInfo.number)
        {
            return true;
        }
        return false;
    }

    public void DrawCard(int playerId)
    {
        // TODO: request to draw card
        CardInfo card = GetRandomCard();

    }

    public void Draw2CardToNow(){
        // TODO: draw 2 cards to current player
    }
    
    CardInfo GetRandomCard(){
        CardInfo info = randCardQ.Dequeue();
        //check if the randq is empty, if is wash the deck
        if(randCardQ.Count == 0)
        {
            WashDeck();
        }
        return info;
    }

    public void AutoPlay()
    {
        // TODO: auto play
        NextTurn();
        roundTimer.Next();
    }

    public void EndGame(bool timeUp)
    {
        // TODO: end game
        if(timeUp)
        {
            // TODO: time up
            UIManager.Instance.ShowPanel<EndGamePanel>();
        }
        else
        {
            // TODO: win
            UIManager.Instance.ShowPanel<EndGamePanel>();
        }
        isGameStart = false;
    }

    public void NextTurn()
    {
        // TODO: change turn
        if(clockWise)
        {
            int nextIndex = (currentPlayerRound + 1) % playerLoop.Count;
            currentPlayerRound = nextIndex;
        }
        else
        {
            int nextIndex = (currentPlayerRound - 1 + playerLoop.Count) % playerLoop.Count;
            currentPlayerRound = nextIndex;
        }
        roundTimer.Next();
    }

    public void SkipTurn()
    {
        // TODO: skip turn
        NextTurn();
        NextTurn();
    }
    
    public bool CheckWin()
    {
        // TODO: check win
        return false;
    }

    public void CallUno()
    {
        // TODO: call uno
    }

    #endregion

    #region Util

    public void SuspendGame()
    {
        // TODO: suspend game
    }

    public void ResumeGame()
    {
        // TODO: resume game
    }

    #endregion
    
}