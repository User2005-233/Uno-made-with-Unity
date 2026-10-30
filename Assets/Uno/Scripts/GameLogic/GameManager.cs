using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using FishNet;
using FishNet.Transporting;

using Random = UnityEngine.Random;


public class GameManager : MonoSingleton<GameManager>
{
    
    #region Variables
    
    //game setting variables

    public enum GameMode { ServerOnly, ClientOnly, Host, Singleplayer }
    public GameMode currentGameMode;
    public IGameMode gameMode;
    

    //card info loading usage
    public CardType cardType;
    public CardColor cardColor;
    public int cardNum = -1;
    Color hovered = new Color(0.75f, 0.75f, 0.75f);
    private Dictionary<int, List<CardInfo>> playerHands = new Dictionary<int, List<CardInfo>>();
    bool isHost = true;
    GameObject PlayerParent;

    //all ui assets loading presets

    //Players data/setting variables
    public List<int> roomPlayerLoop;
    public List<int> playerLoop;

    #endregion


    //all initialization code here
    protected override void Awake()
    {
        base.Awake();
        PlayerParent = GameObject.FindWithTag("Player");
        AddListeners();
        
    }

    void Start()
    {
        OnBootUp();
    }


    #region Member Managing
    public void OnJoinRoom(S_PlayerJoinMessage message, Channel channel)
    {
        //TODO: implement join room logic
        if (message.IsJoin)
        {
            EventManager.Instance.SendMessage(new AddPlayerItem(message.PlayerCode, message.PlayerName));
        }
    }
    public void KickPlayer(int playerId)
    {
        //TODO: implement kick player logic
        
        EventManager.Instance.SendMessage(new RemovePlayerItem(playerId));
        InstanceFinder.ServerManager.Broadcast(new S_PlayerKickMessage(playerId));
    }
    public void HostExit()
    {
        //TODO: implement host exit logic
    }
    public void ExitRoom()
    {
        //TODO: implement exit room logic
    }

    #endregion

    #region Init

    void AddListeners()
    {
        InstanceFinder.ClientManager.RegisterBroadcast<S_PlayerJoinMessage>(OnJoinRoom);
        InstanceFinder.ClientManager.RegisterBroadcast<S_PlayerLeaveMessage>(OnPlayerLeft);
        InstanceFinder.ClientManager.RegisterBroadcast<S_PlayerKickMessage>(OnKickPlayer);

        InstanceFinder.ClientManager.RegisterBroadcast<S_LoopSyncMessage>(OnPlayerLoopSync);
        InstanceFinder.ClientManager.RegisterBroadcast<S_GameStartMessage>(OnGameStart);
        InstanceFinder.ClientManager.RegisterBroadcast<S_GameEndMessage>(OnGameEnd);
        InstanceFinder.ClientManager.RegisterBroadcast<S_CardPlayMessage>(OnPostCardPlay);
        InstanceFinder.ClientManager.RegisterBroadcast<S_CardDrawMessage>(OnPostCardDraw);
        InstanceFinder.ClientManager.RegisterBroadcast<S_TurnChangeMessage>(OnChangeTurn);

        InstanceFinder.ServerManager.RegisterBroadcast<C_PlayerLeftMessage>(OnPlayerLeave);
        InstanceFinder.ServerManager.RegisterBroadcast<C_CardOpMessage>(OnCardOp);

        //server widgets
        InstanceFinder.ServerManager.RegisterBroadcast<C_ChatMessage>(OnChatAttempt);
        InstanceFinder.ServerManager.RegisterBroadcast<C_AnimMessage>(OnAnimAttempt);

        //client widgets
        InstanceFinder.ClientManager.RegisterBroadcast<S_ChatMessage>(OnChatMessage);
        InstanceFinder.ClientManager.RegisterBroadcast<S_AnimMessage>(OnAnimMessage);
    }
    
    void OnInit()
    {

    }

    #endregion

    #region BootUp
    void OnBootUp()
    {
        //show main panel
        UIManager.Instance.ShowPanel<MainMenuPanel>();

    }
    #endregion

    #region Game Mode

    public void SetGameMode(GameMode mode)
    {
        currentGameMode = mode;
        switch (mode)
        {
            case GameMode.Singleplayer:
                gameMode = new SinglePlayerGameMode();
                break;
            case GameMode.Host:
            case GameMode.ServerOnly:
            case GameMode.ClientOnly:
                gameMode = new NetworkGameMode();
                break;
        }
    }

    #endregion

    #region Local Logic

    bool CheckCardInHand(int id, CardInfo info){
        //TODO: 检查卡牌是否在手牌中
        return false;
    }

    void PlayCard(CardInfo card)
    {
        // TODO: 处理出牌
    }

    Dictionary<int, int> CalculateScore()
    {
        // TODO: 计算分数
        Dictionary<int, int> scoreBoard = new();
        for (int i = 0; i < roomPlayerLoop.Count; i++)
        {
            for (int j = 0; j < playerHands[i].Count; j++)
            {
                if (playerHands[i][j].type == CardType.Number)
                {
                    scoreBoard.Add(roomPlayerLoop[i], playerHands[i][j].number);
                }
            }
        }
        return scoreBoard;
    }


    #endregion

    #region Server Code

    public void StartGame()
    {
        InstanceFinder.ServerManager.Broadcast(new S_GameStartMessage(playerHands));
    }
    private void Update()
    {
        
    }


    






    private void PlayCardSuccess(int id, CardInfo card)
    {
        //TODO: changes after played card
        // if (card.type == CardType.Wild || card.type == CardType.WildDrawFour)
        // {
            
        // }
        // if (card.type == currentCardInfo.type || card.color == currentCardInfo.color || card.number == currentCardInfo.number)
        // {
            
        // }
        // InstanceFinder.ServerManager.Broadcast(new S_CardPlayMessage(id, card));
    }



    private void OnPlayerLeave(FishNet.Connection.NetworkConnection connection, C_PlayerLeftMessage message, FishNet.Transporting.Channel channel)
    {
        // TODO: 处理玩家离开
    }

    private void OnCardOp(FishNet.Connection.NetworkConnection connection, C_CardOpMessage message, FishNet.Transporting.Channel channel)
    {
        // TODO: 处理卡牌操作
        // if(message.isPlay){
        //     // TODO: 处理出牌
        //     if(CheckCardInHand(message.PlayerCode, message.Card))
        //     {
        //         if(CheckPlayable(message.Card))
        //         {
        //             PlayCardSuccess(message.PlayerCode, message.Card);
        //         }
        //     }
        // }
        // else{
        //     // TODO: 处理抽牌
        //     DrawCard(message.PlayerCode);
        // }
    }

    void DrawCard(int id)
    {
        // TODO: 处理抽牌
        // CardInfo info = GetRandomCard();
        // playerHands[id].Add(info);
        // InstanceFinder.ServerManager.Broadcast(new S_CardDrawMessage(id,info));
    }
    

    #endregion

    #region Client Code

    public void OnGameStart(S_GameStartMessage message, Channel channel)
    {
        // TODO: 初始化游戏状态
        // isGameStart = true;
        // Put1stRandomCard();
    }

    void OnGameEnd(S_GameEndMessage message, Channel channel)
    {
        // TODO: 游戏结束逻辑
        // isGameStart = false;
    }

    void OnPlayerLoopSync(S_LoopSyncMessage message, Channel channel)
    {
        // TODO: 处理玩家循环同步
        playerLoop = new List<int>(message.playerLoop);
    }

    void OnPlayerLeft(S_PlayerLeaveMessage message, Channel channel)
    {
        // TODO: 处理玩家离开
    }

    void OnKickPlayer(S_PlayerKickMessage message, Channel channel)
    {
        // TODO: 处理被踢出房间
    }
    
    void OnChangeTurn(S_TurnChangeMessage message, Channel channel)
    {
        // TODO: 处理换牌
    }

    void OnPostCardPlay(S_CardPlayMessage message, Channel channel)
    {
        // TODO: 卡牌播放后逻辑
    }

    void OnPostCardDraw(S_CardDrawMessage message, Channel channel)
    {
        // TODO: 卡牌抽取后逻辑
    }


    #endregion

    #region Widget Code

    void OnChatAttempt(FishNet.Connection.NetworkConnection connection, C_ChatMessage message, FishNet.Transporting.Channel channel)
    {
        // TODO: 处理聊天
        S_ChatMessage msg = new S_ChatMessage(message.id, message.msg);
        InstanceFinder.ServerManager.Broadcast(msg);
    }

    void OnChatMessage(S_ChatMessage message, Channel channel)
    {
        // TODO: 处理聊天消息
    }

    void OnAnimAttempt(FishNet.Connection.NetworkConnection connection, C_AnimMessage message, FishNet.Transporting.Channel channel)
    {
        // TODO: 处理表情
        S_AnimMessage msg = new S_AnimMessage(message.id, message.animId);
        InstanceFinder.ServerManager.Broadcast(msg);
    }

    void OnAnimMessage(S_AnimMessage message, Channel channel)
    {
        // TODO: 处理表情消息
    }
    

    #endregion

}
