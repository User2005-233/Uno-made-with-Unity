using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

using Random = UnityEngine.Random;
using System.Data;


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
    CardInfo currentCardInfo;
    float gameTime = 300f;
    float roundTime = 15f;
    TickTimer gameTimer;
    TickTimer roundTimer;
    bool isGameRunning = false;
    bool clockWise = true;
    int currentPlayerRound;
    bool isNextSkip = false;
    bool isNextWildDrawFour = false;


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

    /// <summary>
    /// add visiable player entity
    /// </summary>
    public void AddPlayerEntity()
    {
        foreach (var player in playerHands.Keys)
        {
            if (player != selfCode)
            {
                EventManager.Instance.SendMessage(new PlayerEntityCreateEvent(player));
            }
            else
            {
                EventManager.Instance.SendMessage(new SelfPlayerCreateEvent(player));
            }
        }
    }

    /// <summary>
    /// shuffle player loop
    /// </summary>
    public List<int> ShufflePlayerLoop()
    {
        playerLoop.Clear();
        playerLoop = new List<int>((gameMode as SinglePlayerGameMode).roomPlayerLoop);
        Shuffler.Shuffle<int>(playerLoop);
        return playerLoop;
    }

    /// <summary>
    /// read player loop
    /// </summary>
    public void ReadPlayerLoop(List<int> list)
    {
        //tell playermanager to set player loop
        playerLoop = list;
    }

    #endregion

#region Runtime Logic

    public void StartGame(List<int> roomPlayerLoop)
    {
        // TODO: start game
        isGameRunning = true;
        gameTimer.Start();
        // EventManager.Instance.SendMessage(new SetPlayerLoopEvent(playerLoop));
        // UIManager.Instance.ShowPanel<GamePanel>();
    }

    public void Update()
    {
        // TODO: update game logic
        if (isGameRunning)
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

    /// <summary>
    /// try to see if the card can be played, return true if it can be played
    /// </summary>
    public bool TryPlayCard(int playerId, CardInfo card)
    {
        // TODO: request to play card
        if (playerId == currentPlayerRound)
        {
            playerHands[playerId].Remove(card);
            if (CheckWin())
            {
                EndGame(false);
                return true;
            }
        }
        return false;
    }

    public int GetCurrentPlayer()
    {
        return currentPlayerRound;
    }

    public int GetLastPlayer()
    {
        int currentId = currentPlayerRound;
        if (clockWise)
        {
            return playerLoop[(playerLoop.IndexOf(currentId) - 1 + playerLoop.Count) % playerLoop.Count];
        }
        else
        {
            return playerLoop[(playerLoop.IndexOf(currentId) + 1) % playerLoop.Count];
        }
    }

    /// <summary>
    /// 获取当前玩家最常出现的卡牌颜色
    /// </summary>
    public CardColor GetCurrentMostCardColor()
    {
        int code = GetCurrentPlayer();
        Dictionary<CardColor, int> colorCount = new Dictionary<CardColor, int>();
        for (int i = 0; i < playerHands[code].Count; i++)
        {
            if (colorCount.ContainsKey(playerHands[code][i].color))
            {
                colorCount[playerHands[code][i].color]++;
            }
            else
            {
                colorCount[playerHands[code][i].color] = 1;
            }
        }
        CardColor mostColor = CardColor.None;
        int maxCount = 0;
        foreach (var pair in colorCount)
        {
            if (pair.Value > maxCount)
            {
                mostColor = pair.Key;
                maxCount = pair.Value;
            }
        }
        return mostColor;
    }

    public bool CheckLastPossessColor(CardColor color)
    {
        int lastId = GetLastPlayer();
        return playerHands[lastId].Any(card => card.color == color);
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
        playerHands[playerId].Add(card);
        EventManager.Instance.SendMessage(new CardAddEvent(playerId, card));
    }

    public void Draw2CardToNext()
    {
        // TODO: draw 2 cards to current player
        NextTurn();
        DrawCard(currentPlayerRound);
        DrawCard(currentPlayerRound);
    }

    public void ChangeColor(CardColor color)
    {
        currentCardInfo.color = color;
        EventManager.Instance.SendMessage(new ChangeColorEvent(color));
    }

    /// <summary>
    /// 发送下一个玩家WildDraw的请求
    /// </summary>
    public void WildDrawSend()
    {
        if (!isGameRunning) return;
        isNextWildDrawFour = true;
        EventManager.Instance.SendMessage(new WildDrawSendEvent());
    }

    /// <summary>
    /// WildDraw的结果返回
    /// </summary>
    public void WildDrawBack(CardColor color)
    {
        if (playerHands[currentPlayerRound].Count == 0) return;
        if (CheckLastPossessColor(color))
        {
            for (int i = 0; i < 8; i++)
            {
                gameMode.DrawCard(currentPlayerRound);
            }
            NextTurn();
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                gameMode.DrawCard(currentPlayerRound);
            }
        }
        isNextWildDrawFour = false;
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
        if (!isGameRunning) return;
        if (playerHands[currentPlayerRound].Count == 0) return;
        if (isNextWildDrawFour)
        {
            CardColor color = Random.Range(0, 4) switch
            {
                0 => CardColor.green,
                1 => CardColor.blue,
                2 => CardColor.yellow,
                3 => CardColor.red,
                _ => CardColor.wild,
            };
            EventManager.Instance.SendMessage(new WildDrawBackEvent(color));
            return;
        }
        for (int i = 0; i < playerHands[currentPlayerRound].Count; i++)
        {
            if (gameMode.AutoTryPlayCard(currentPlayerRound, playerHands[currentPlayerRound][i]))
            {
                return;
            }
        }
        DrawCard(currentPlayerRound);
    }

    public void UpdateCardInfo(CardInfo info)
    {
        currentCardInfo = info;
        EventManager.Instance.SendMessage(new UpdateCardInfoEvent(info));
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
        isGameRunning = false;
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
    isGameRunning = false;
}

public void ResumeGame()
{
    // TODO: resume game
    isGameRunning = true;
}

#endregion
    
}
