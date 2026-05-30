using System;

using System.Collections;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using Unity.VisualScripting;

using UnityEngine;

using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;

using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour

{
    public CardType cardType;

    public CardColor cardColor;

    public int cardNum = -1;

    Color hovered = new Color(0.75f, 0.75f, 0.75f);

    string unoAllSprite = "UNO_ALL.png";

    StringBuilder sb = new StringBuilder();

    Sprite cardSprite = null;

    string prefabAddress = "Card.prefab";

    //GameObject temp=null;

    private Dictionary<int, List<CardInfo>> playerHands = new Dictionary<int, List<CardInfo>>();
    bool isHost = true;

    List<Card> Deck = new List<Card>();
    List<CardInfo> CardInfoList = new List<CardInfo>();
    Queue<CardInfo> randCardQ = new Queue<CardInfo>();

    Sprite Sprite;

    List<Card> cardsPool = new List<Card>();
    protected int DeckSize = 108;

    private void Awake()
    {
        EventManager.Instance.AddActionListener<InitAction>(InitCallBack);
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

    void InitCallBack()
    {
        AddListeners();

        InitiateDeck();

        //Temp!!!!
        playerHands.Add(0, new List<CardInfo>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K is pressed");
            AddCard(0);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            WashStack();
            Debug.Log(randCardQ.Count);
        }
    }

    void AddListeners()
    {
        EventManager.Instance.AddListener<PlayerPlayCardEvent>(OnPlayCardEvent);

        EventManager.Instance.AddNetListener<PlayerPlusCardNetEvent>(OnPlayerPlusCardNetEvent);
    }

    private void OnPlayerPlusCardNetEvent(INetEventMessage message)
    {
        var msg = message as PlayerPlusCardNetEvent;
        
    }

    void WashStack()
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

    Card GetRandomCard()

    {
        if (randCardQ.Count == 0)

        {
            return null;
        }

        Card card = GetCardVisual(randCardQ.Dequeue());

        return card;
    }

    /// <summary>
    /// 初始化牌堆
    /// </summary>
    void OnPlayCardEvent(IEventMessage message)
    {
        var msg = message as PlayerPlayCardEvent;
    }

    Card GetCardVisual(CardInfo info)

    {
        Card card = CardPoolManager.Instance.GetCard();

        card.ChangeProperties(info);

        return card;
    }

    /// <summary>
    /// 洗牌算法
    /// </summary>
    public void AddCard(int playerCode)
    {
        if (!playerHands.ContainsKey(playerCode)) Debug.Log("Nonexisting player called!");

        CardInfo info = randCardQ.Dequeue();
        Card cardVisual = CardPoolManager.Instance.GetCard();
        cardVisual.ChangeProperties(info);
        EventManager.Instance.SendMessage(new PlayerPlusCardEvent(playerCode, cardVisual));
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    public bool RemoveCard(int playerCode, CardInfo card)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            return playerHands[playerCode].Remove(card);
        }

        return false;
    }

    /// <summary>
    /// 获取指定玩家的手牌
    /// </summary>
    public IReadOnlyList<CardInfo> GetHand(int playerCode)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            return playerHands[playerCode].AsReadOnly();
        }

        return new List<CardInfo>().AsReadOnly();
    }

    /// <summary>
    /// 获取指定玩家的手牌数量
    /// </summary>
    public int GetHandCount(int playerCode)
    {
        return playerHands.ContainsKey(playerCode) ? playerHands[playerCode].Count : 0;
    }

    /// <summary>
    /// 对指定玩家的手牌进行排序
    /// </summary>
    public void SortHand(int playerCode)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            playerHands[playerCode].Sort();
        }
    }

    /// <summary>
    /// 对所有玩家的手牌进行排序
    /// </summary>
    public void SortAllHands()
    {
        foreach (var hand in playerHands.Values)

        {
            hand.Sort();
        }
    }

    /// <summary>
    /// 使用LINQ查询获取指定颜色的卡牌
    /// </summary>
    public void SortHandCustom(int playerCode)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            playerHands[playerCode] = playerHands[playerCode]

                .OrderBy(card => card.color)

                .ThenBy(card => card.type)

                .ThenBy(card => card.number)

                .ToList();
        }
    }

    /// <summary>
    /// 按颜色排序指定玩家的手牌
    /// </summary>
    public void SortHandByColor(int playerCode)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            playerHands[playerCode] = playerHands[playerCode]

                .OrderBy(card => card.color)

                .ThenBy(card => card.type)

                .ThenBy(card => card.number)

                .ToList();
        }
    }

    /// <summary>
    /// 检查指定玩家是否拥有指定的卡牌
    /// </summary>
    public bool HasCard(int playerCode, CardInfo card)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            return playerHands[playerCode].Contains(card);
        }

        return false;
    }

    /// <summary>
    /// 清空指定玩家的手牌
    /// </summary>
    public void ClearHand(int playerCode)
    {
        if (playerHands.ContainsKey(playerCode))

        {
            playerHands[playerCode].Clear();
        }
    }

    /// <summary>
    /// 清空所有玩家的手牌
    /// </summary>
    public void ClearAllHands()
    {
        playerHands.Clear();
    }

    /// <summary>
    /// 获取所有玩家的代码
    /// </summary>
    public IEnumerable<int> GetAllPlayerCodes()
    {
        return playerHands.Keys;
    }
}
