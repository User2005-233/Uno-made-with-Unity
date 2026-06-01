using System;

using System.Collections.Generic;

using System.Text;

using UnityEngine;

using Random = UnityEngine.Random;


public class LogicManager : MonoSingleton<LogicManager>

{
    
    #region Variables
    
    //game setting variables

    /// <summary>
    /// Player code list, could be used with game loop
    /// </summary>
    List<int> playerCodeList = new List<int>();

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
    Sprite Sprite;
    List<Card> cardsPool = new List<Card>();
    protected int DeckSize = 108;



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
    void Awake()
    {
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
        // EventManager.Instance.AddListener<>();
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
        UIManager.Instance.ShowPanel<MainMenu>();

    }



    #endregion

    #region Network
    void StartHost()
    {
        UIManager.Instance.ShowPanel<HostPanel>();
    }
    void StartClient()
    {
        
    }
    void JoinRoom()
    {
        UIManager.Instance.ShowPanel<RoomPanel>();
    }
    
    #endregion


    #region GameRuntime
    void OnGameStart()
    {
        // TODO: 初始化游戏状态
        isGameStart = true;
        currentCardInfo = GetCurrentCardInfo();
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
    //for handcardmanagers to evaluate
    CardInfo GetCurrentCardInfo()
    {
        return currentCardInfo;
    }

    #endregion


}