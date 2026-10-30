using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerEntity : MonoBehaviour

{
    int code;
    Camera mainCamera;
    public bool isSelf = false;
    GameObject cardPrefab;
    HandCardManager handCardManager;
    public List<CardInfo> playerCards;

    void Awake(){
        EventManager.Instance.AddListener<CardDealEvent>(OnCardDeal);
        EventManager.Instance.AddListener<CardAddEvent>(OnCardAdd);
        EventManager.Instance.AddListener<TurnEvent>(OnTurn);
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        handCardManager = transform.Find("HandCard").GetComponent<HandCardManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void SetCode(int code)
    {
        this.code = code;
    }

    public void MakeNotSelf()
    {
        mainCamera.enabled = false;
        isSelf = false;
    }
    
    public void MakeMeSelf()
    {
        mainCamera.enabled = true;
        isSelf = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTurn(IEventMessage message){
        var msg = (TurnEvent)message;
        if(msg.playerId == code)
        {
            if(isSelf)
            {
                // TODO: wait for play card action from user
            }
        }
    }
    
    //for accepting a list of card to add into hand, basically for net mode
    void OnCardDeal(IEventMessage message)
    {
        var msg = (CardDealEvent)message;
        playerCards = new List<CardInfo>(msg.Cards[code]);
        //spawn all card gameobjects
        for (int i = 0; i < playerCards.Count; i++)
        {
            if (cardPrefab == null)
            {
                Debug.LogError("Card prefab is not assigned!");
                return;
            }
            GameObject card = Instantiate(cardPrefab);
            handCardManager.InsertHandCard(card.GetComponent<Card>());
        }
        Debug.Log($"Player {code} received {playerCards.Count} cards");
    }
    
    //for add 1 card to hand, for single player mode
    void OnCardAdd(IEventMessage message)
    {
        var msg = (CardAddEvent)message;
        playerCards.Add(msg.card);
        bool selectable = isSelf;
        Card card = CardPoolManager.Instance.GetCard();
        card.ChangeProperties(msg.card);
        card.inMyHand = selectable;
        handCardManager.InsertHandCard(card);
        Debug.Log($"Player {code} received card {msg.card}");
    }

    
}
