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

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            WashDeck();
            Debug.Log(randCardQ.Count);
        }
    }

    void AddListeners()
    {

        EventManager.Instance.AddNetListener<PlayerPlusCardNetEvent>(OnPlayerPlusCardNetEvent);
    }

    private void OnPlayerPlusCardNetEvent(INetEventMessage message)
    {
        var msg = message as PlayerPlusCardNetEvent;
        
    }

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




}
