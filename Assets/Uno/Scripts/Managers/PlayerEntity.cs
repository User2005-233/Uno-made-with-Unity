using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerEntity : MonoBehaviour

{
    //player code, used to identify the player in the game
    [SerializeField]int code;
    GameObject mainCamera;
    // AudioListener audioListener;
    //whether this player is the local player
    public bool isTruePlayer = false;
    GameObject cardPrefab;
    HandCardManager handCardManager;
    public List<CardInfo> playerCards;

    void Awake(){
        EventManager.Instance.AddListener<CardDealEvent>(OnCardDeal);
        EventManager.Instance.AddListener<CardAddEvent>(OnCardAdd);
        EventManager.Instance.AddListener<TurnEvent>(OnTurn);
        mainCamera = transform.Find("Main Camera").gameObject;
        handCardManager = transform.Find("HandCard").GetComponent<HandCardManager>();
        mainCamera.SetActive(false);
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
        mainCamera.SetActive(false);
        // audioListener.enabled = false;
        isTruePlayer = false;
    }
    
    public void MakeMeSelf()
    {
        mainCamera.SetActive(true);
        // audioListener.enabled = true;
        isTruePlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTruePlayer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit) &&
                hit.collider is BoxCollider)
            {
                if(!handCardManager.selected)
                    handCardManager.selected = hit.collider.GetComponent<Card>();
                hit.collider.GetComponent<Card>().onDrag = true;
                if(Input.GetMouseButtonUp(0))
                {
                    handCardManager.selected.onDrag = false;
                    handCardManager.selected = null;
                }
            }
        }
    }

    void OnTurn(IEventMessage message){
        var msg = (TurnEvent)message;
        if(msg.playerId == code)
        {
            if(isTruePlayer)
            {
                // TODO: wait for play card action from user
            }
        }
    }
    
    //for accepting a list of card to add into hand
    void OnCardDeal(IEventMessage message)
    {
        var msg = (CardDealEvent)message;
        playerCards = new List<CardInfo>(msg.Cards[code]);
        // get all card gameobjects
        for (int i = 0; i < playerCards.Count; i++)
        {
            Card card = CardPoolManager.Instance.GetCard();
            card.ChangeProperties(playerCards[i]);
            card.inTruePlayerHand = isTruePlayer;
            handCardManager.InsertHandCard(card);
        }
        Debug.Log($"Player {code} received {playerCards.Count} cards");
    }
    
    //for add 1 card to hand, for local logic use
    void OnCardAdd(IEventMessage message)
    {
        var msg = (CardAddEvent)message;
        if(msg.playerCode == code){
            playerCards.Add(msg.card);
            bool selectable = isTruePlayer;
            Card card = CardPoolManager.Instance.GetCard();
            card.ChangeProperties(msg.card);
            card.inTruePlayerHand = selectable;
            handCardManager.InsertHandCard(card);
            Debug.Log($"Player {code} received card {msg.card}");
        }
    }

    
}
