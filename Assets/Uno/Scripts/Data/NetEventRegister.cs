using FishNet;
using FishNet.Broadcast;
using FishNet.Transporting;
using System.Collections.Generic;


#region Game Util

//游戏开始后卡牌初始化
public struct S_GameStartMessage : IBroadcast
{
    public Dictionary<int, List<CardInfo>> playerCards;
    
    public S_GameStartMessage(Dictionary<int, List<CardInfo>> playerCards)
    {
        this.playerCards = playerCards;
    }
}
//游戏结束消息
public struct S_GameEndMessage : IBroadcast
{
    public Dictionary<int, int> scores;

    public S_GameEndMessage(Dictionary<int, int> scores)
    {
        this.scores = scores;
    }
}

// 玩家退出的事件消息
public struct S_PlayerLeaveMessage : IBroadcast
{
    public int id;
    public S_PlayerLeaveMessage(int id)
    {
        this.id = id;
    }
}

//同步房间内成员信息
public struct S_PlayerJoinMessage : IBroadcast
{
    public int PlayerCode;
    public string PlayerName;
    public bool IsJoin;
    public S_PlayerJoinMessage(int clientId, string playerName, bool isJoin)
    {
        //player code(assigned by server)
        PlayerCode = clientId;
        //player predefined
        PlayerName = playerName;
        //join or exit
        IsJoin = isJoin;
    }
}

public struct C_PlayerLeftMessage : IBroadcast
{
    public int PlayerCode;
    public C_PlayerLeftMessage(int playerCode)
    {
        PlayerCode = playerCode;
    }
}

public struct S_PlayerKickMessage : IBroadcast {
    public int id;
    public S_PlayerKickMessage(int id){
        this.id = id;
    }
}

public struct TestMessage: IBroadcast {
    public int id;
    public TestMessage(int id){
        this.id = id;
    }
}

#endregion

#region Game Logic

public struct C_CardPlayMessage : IBroadcast
{
    public int PlayerCode;
    public CardInfo Card;
    public C_CardPlayMessage(int playerCode, CardInfo card)
    {
        PlayerCode = playerCode;
        Card = card;
    }
}

public struct C_CardOpMessage : IBroadcast
{
    /// <summary>
    /// true: play card, false: draw card
    /// </summary>
    public bool isPlay;
    public int PlayerCode;
    public CardInfo Card;
    public C_CardOpMessage(int playerCode, CardInfo card, bool isPlay)
    {
        PlayerCode = playerCode;
        Card = card;
        this.isPlay = isPlay;
    }
}

public struct S_CardPlayMessage : IBroadcast{
    public int PlayerCode;
    public CardInfo Card;
    public S_CardPlayMessage(int code, CardInfo card)
    {
        this.PlayerCode = code;
        Card = card;
    }
}


public struct S_CardDrawMessage : IBroadcast{
    public int PlayerCode;
    public CardInfo Card;
    public S_CardDrawMessage(int code, CardInfo card)
    {
        this.PlayerCode = code;
        Card = card;
    }
}


public struct S_TurnChangeMessage : IBroadcast{
    public int id;
    public S_TurnChangeMessage(int id){
        this.id = id;
    }
}

public struct DynamicHostMessage : IBroadcast{
    public int id;
    public DynamicHostMessage(int id){
        this.id = id;
    }
}

public struct S_LoopSyncMessage : IBroadcast{
    public int[] playerLoop;
    public S_LoopSyncMessage(int[] playerLoop){
        this.playerLoop = playerLoop;
    }
}

#endregion


#region Widgets

public struct C_ChatMessage : IBroadcast{
    public int id;
    public string msg;
    public C_ChatMessage(int id, string msg){
        this.id = id;
        this.msg = msg;
    }
}

public struct S_ChatMessage : IBroadcast{
    public int id;
    public string msg;
    public S_ChatMessage(int id, string msg){
        this.id = id;
        this.msg = msg;
    }
}

public struct C_AnimMessage : IBroadcast{
    public int id;
    public int animId;
    public C_AnimMessage(int id, int animId){
        this.id = id;
        this.animId = animId;
    }
}

public struct S_AnimMessage : IBroadcast{
    public int id;
    public int animId;
    public S_AnimMessage(int id, int animId){
        this.id = id;
        this.animId = animId;
    }
}

#endregion
