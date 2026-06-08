using System;

using System.Collections.Generic;

using System.Text;

using UnityEngine;

using Random = UnityEngine.Random;


public class LogicManager : MonoSingleton<LogicManager>

{
    
    #region Variables
    
    //game setting variables



    //card info loading usage
    public CardType cardType;
    public CardColor cardColor;
    public int cardNum = -1;
    Color hovered = new Color(0.75f, 0.75f, 0.75f);
    private Dictionary<int, List<CardInfo>> playerHands = new Dictionary<int, List<CardInfo>>();
    bool isHost = true;
    List<Card> Deck = new List<Card>();
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
    int currentPlayerRound;


    //all ui assets loading presets



    #endregion

    //Players data/setting variables
    public List<int> playerLoop = new List<int>();

    //all initialization code here
    protected override void Awake()
    {
        base.Awake();
        PlayerParent = GameObject.FindWithTag("Player");
        AddListeners();
        DeckInit();
    }

    void Start()
    {
        OnBootUp();
    }

    void Update()
    {
        if(isGameStart) GameLoop();

    }


    #region Init

    void AddListeners()
    {
        EventManager.Instance.AddNetListener<PlayerJoinedMessage>(OnPlayerJoined);
        EventManager.Instance.AddNetListener<PlayerLeftMessage>(OnPlayerLeft);
    }
    
    void OnPlayerJoined(INetEventMessage message)
    {
        //1.check if i am host
        //2.if host, update player loop and send to all clients
        //3.if not host, do nothing(for now)

        //4.when at in-game state, dynamicly move the host to the next player
        
        //and maybe some network error handling?
        var msg = (PlayerJoinedMessage)message;
        if(isHost)
        {
            playerLoop.Add(msg.ClientId);
            EventManager.Instance.SendMessage(new AddPlayerItem(msg.PlayerName));
        }
        else
        {
        }
    }

    void OnPlayerLeft(INetEventMessage message)
    {
        var msg = (PlayerLeftMessage)message;
        if(isHost)
        {
            playerLoop.Remove(msg.ClientId);
            //EventManager.Instance.SendMessage(new RemovePlayerItem(msg.ClientId));
        }
        else
        {
        }
    }
    
    void OnInit()
    {

    }

    void DeckInit()
    {

    }
    void RuntimeInit()
    {
        gameTimer = new TickTimer(gameTime);
        roundTimer = new TickTimer(roundTime);
    }

    void InitiateDeck()
    {
        // 添加数字卡牌
        for (int i = 0; i < 2; ++i)
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                for (int j = 1; j < 10; ++j)
                {
                    CardInfo info = new CardInfo(color, CardType.Number, j);

                    CardInfoList.Add(info);
                }
            }
        }

        // 添加0点卡牌        
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            CardInfo info = new CardInfo(color, CardType.Number, 0);

            CardInfoList.Add(info);
        }

        // 添加功能卡牌
        for (int i = 0; i < 2; ++i)
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                foreach (ColorFunc func in Enum.GetValues(typeof(ColorFunc)))

                {
                    CardInfo info = new CardInfo(color, func);

                    CardInfoList.Add(info);
                }
            }
        }

        // 添加特殊卡牌
        for (int i = 0; i < 4; ++i)
        {
            foreach (WildFunc func in Enum.GetValues(typeof(WildFunc)))
            {
                CardInfo info = new CardInfo(func);

                CardInfoList.Add(info);
            }
        }
    }

    //initializa deck randq
    void WashDeck()
    {
        if (!isHost) return;

        List<CardInfo> DeckTemp = new List<CardInfo>(CardInfoList);
        for (int i = 0; i < DeckSize; i++)
        {
            int index = Random.Range(0, DeckSize - i);
            CardInfo card = DeckTemp[index];

            randCardQ.Enqueue(card);

            DeckTemp.Remove(card);
        }
    }

    

    #endregion

    #region BootUp
    void OnBootUp()
    {
        //show main panel
        UIManager.Instance.ShowPanel<MainMenuPanel>();

    }
    #endregion



    #region GameRuntime
    void OnGameStart()
    {
        // TODO: 初始化游戏状态
        isGameStart = true;
        currentCardInfo = GetCurrentCardInfo();

    }
    void DealCards()
    {
        // TODO: 发牌逻辑
        for (int i = 0; i < playerLoop.Count; i++)
        {
            //extract multiple cards from randomq
            //send cards to players using network
        }
    }

    void GameLoop()
    {
        gameTimer.Tick(Time.deltaTime);
        roundTimer.Tick(Time.deltaTime);

        if (gameTimer.IsCompleted)
        {
            // 游戏结束
        }

        if (roundTimer.IsCompleted)
        {
            roundTimer.Next();
        }
    }
    
    

    //put the first random card on the table
    void Put1stRandomCard()
    {
        currentCardInfo = randCardQ.Dequeue();

    }
    CardInfo GetCurrentCardInfo()
    {
        return currentCardInfo;
    }
    void PlayCardSuccess(CardInfo card)
    {
        //TODO: changes after played card
        if (card.type == CardType.Wild || card.type == CardType.WildDrawFour)
        {
            
        }
        if (card.type == currentCardInfo.type || card.color == currentCardInfo.color || card.number == currentCardInfo.number)
        {
            
        }
    }
    void SetCurrentCardInfo(CardInfo cardInfo)
    {
        currentCardInfo = cardInfo;
    }
    bool CheckPlayable(CardInfo card)
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

    #endregion


}