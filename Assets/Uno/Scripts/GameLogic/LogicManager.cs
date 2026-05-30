using System.Collections;

using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class LogicManager : MonoSingleton<LogicManager>

{
    //runtime variables
    CardType CurrentType = CardType.None;
    CardColor CurrentColor = CardColor.None;
    int CurrentNum = -1;
    float gameTime = 300f;
    float roundTime = 15f;
    TickTimer gameTimer;
    TickTimer roundTimer;
    bool isGameStart = false;
    int currentPlayerRound;

    //Players data/setting variables

    public Dictionary<int, bool> playerDict = new Dictionary<int, bool>();
    public List<int> playerLoop = new List<int>();

    CardColor GetRandomColor()
    {
        CardColor color = (CardColor)Random.Range(0, 3);
        return color;

    }
    protected override void OnSingletonAwake()
    {
        EventManager.Instance.AddActionListener<InitAction>(InitCallBack);
    }

    void InitCallBack()
    {

        AddListeners();

    }

    void AddListeners()
    {
        EventManager.Instance.AddListener<PlayerPlayCardEvent>(OnPlayCardEvent);
        EventManager.Instance.AddActionListener<StartGameAction>(StartGameCallback);
    }

    //player's play try event
    private void OnPlayCardEvent(IEventMessage message)
    {
        var msg = message as PlayerPlayCardEvent;
        if (CardAbleToPlay(msg.Card))
        {
            EventManager.Instance.TriggerEvent<PlayerFailPlayCardAction>();
            roundTimer.Skip();
        }
    }

    //check if the card able to be played
    public bool CardAbleToPlay(CardInfo card)
    {
        if (card == null)
            return false;

        CardType cardType = card.type;

        CardColor cardColor = card.color;

        int cardNum = card.number;

        // 数字卡牌规则
        if (cardType == CardType.Number)
        {
            if (cardColor == CurrentColor || cardNum == CurrentNum)
                return true;
        }

        // 万能卡牌规则
        else if (cardType == CardType.Wild || cardType == CardType.PlusFour)
        {
            return true;
        }

        // 功能卡牌规则：Skip, Reverse, PlusTwo
        else
        {
            if (cardColor == CurrentColor)
                return true;
        }

        return false;

    }

    void StartGameCallback()
    {
        //start game initialization...?
        isGameStart = true;
        //timers set
        gameTimer = new TickTimer(gameTime);
        roundTimer = new TickTimer(roundTime);
        gameTimer.OnTimerComplete += GameOver;
        roundTimer.OnTimerComplete += NextRound;

        //network synchronization


        //
    }


    // Update is called once per frame
    void Update()
    {
        //check if game started
        if (isGameStart)
        {
            //regular delta time feed
            gameTimer.Tick(Time.deltaTime);
            roundTimer.Tick(Time.deltaTime);
            //check if the game finish
            if (gameTimer.IsCompleted)
            {
                gameTimer.Reset();
                roundTimer.Reset();
            }
            else
            {
                //check if the round end
                if (roundTimer.IsCompleted)
                {
                    roundTimer.Skip();
                }
            }
        }
    }

    void NextRound()
    {
        if (!roundTimer.IsCompleted)
        {
            roundTimer.ManualTriggerCallback();
            roundTimer.Skip();
        }
    }

    void NextRoundLimit()
    {
        //play auto-selected card or draw card
        EventManager.Instance.SendMessage(new AutoPlayEvent(currentPlayerRound));
        //get next player started
        currentPlayerRound = NextPlayerCode();
        //change all visual
        EventManager.Instance.SendMessage(new StartPlayerRoundEvent(currentPlayerRound));
        //send net events

    }

    int NextPlayerCode()
    {
        return 0;
    }

    void GameOver()
    {
        //stop all game logic and reset all

        //get all final score and print to screen

        //reset all state and runtime data
        
    }



    //for external use
    public CardInfo GetCurrentCardInfo()
    {
        CardInfo toret = new CardInfo();
        toret.type = CurrentType;
        toret.color = CurrentColor;
        toret.number = CurrentNum;
        return toret;
    }
}
