using System.Collections.Generic;
using FishNet.Transporting;
using FishNet;
using System.Drawing;
using Unity.VisualScripting;



//sent by gamemode
//to hand card
public struct CardDealEvent : IEventMessage
{
    public Dictionary<int, List<CardInfo>> Cards;
    public CardDealEvent(Dictionary<int, List<CardInfo>> cards)
    {
        Cards = cards;
    }
}

public struct PlayerEntityCreateEvent : IEventMessage
{
    public int PlayerId;
    public bool isSelf;
    public PlayerEntityCreateEvent(int playerId, bool isSelf)
    {
        PlayerId = playerId;
        this.isSelf = isSelf;
    }
}

public readonly struct SetPlayerLoopEvent : IEventMessage
{
    public readonly List<int> PlayerLoop;
    public SetPlayerLoopEvent(List<int> playerLoop)
    {
        PlayerLoop = playerLoop;
    }
}

/// <summary>
/// event for updating player turn animation
/// </summary>
public struct TurnChangedEvent : IEventMessage
{
    public int CurrentPlayerRound;
    public TurnChangedEvent(int currentPlayerRound)
    {
        CurrentPlayerRound = currentPlayerRound;
    }
}

public struct PauseGameEvent : IAction { }

public struct ResumeGameEvent : IAction { }

#region Logic

/// <summary>
/// 玩家加牌事件
/// </summary>
public struct CardAddEvent : IEventMessage
{
    public CardInfo card;
    public int playerCode;
    public CardAddEvent(int code, CardInfo cardIn)
    {
        playerCode = code;
        card = cardIn;
    }
}

/// <summary>
/// 玩家请求抽牌事件
/// </summary>
public struct CardDrawRequestEvent : IEventMessage
{
    public int Code;
    public CardDrawRequestEvent(int code)
    {
        Code = code;
    }
}

/// <summary>
/// need to exist for both local and net game
/// </summary>
public struct CardPlayEvent : IEventMessage
{
    public CardInfo card;
    public int Code;
    public CardPlayEvent(int code, CardInfo card)
    {
        Code = code;

        this.card = card;
    }
}

public struct UpdateCardPlayVisualEvent : IEventMessage
{
    public int id;
    public CardInfo info;
    public UpdateCardPlayVisualEvent(int id, CardInfo info)
    {
        this.id = id;
        this.info = info;
    }
}

/// <summary>
/// 玩家改变颜色事件
/// </summary>
public struct ChangeColorEvent : IEventMessage
{
    public CardColor color;
    public ChangeColorEvent(CardColor color)
    {
        this.color = color;
    }
}

/// <summary>
/// wild draw send event
/// </summary>
public struct WildDrawSendEvent : IEventMessage
{
    public int Code;
    public WildDrawSendEvent(int code)
    {
        Code = code;
    }
}

/// <summary>
/// wild draw back event
/// </summary>
public struct WildDrawBackEvent : IEventMessage
{
    public CardColor card;
    public WildDrawBackEvent(CardColor card)
    {
        this.card = card;
    }
}

/// <summary>
/// 开始下一玩家的出牌
/// </summary>
public struct StartPlayerRoundEvent : IEventMessage
{
    public readonly int code;
    public StartPlayerRoundEvent(int code)
    {
        this.code = code;
    }
}

/// <summary>
/// 玩家移除牌事件
/// </summary>
public readonly struct CardRemoveEvent : IEventMessage
{
    public readonly CardInfo Card;
    public readonly int Code;
    public CardRemoveEvent(int code, CardInfo card)
    {
        Code = code;

        Card = card;
    }
}

#endregion

/// <summary>
/// 测试
/// </summary>
public struct TestEvent : IEventMessage
{
    public int data;
    public TestEvent(int d)
    {
        data = d;
    }
}

//tell hint panel to show hint with message
public struct ShowHintEvent : IEventMessage
{
    public string title;
    public string content;
    public ShowHintEvent(string title, string content)
    {
        this.title = title;
        this.content = content;
    }
}

public class PlayCardFailAction : IAction { }

public struct LoadRoomDataEvent: IEventMessage
{
    //todo: add room data
    public readonly string roomName;
    public readonly Dictionary<int, string> playerList;
    public LoadRoomDataEvent(string roomName, Dictionary<int, string> playerList)
    {
        this.roomName = roomName;
        this.playerList = playerList;
    }
}

public struct AddPlayerItem :IEventMessage
{
    int id;
    string str;
    public AddPlayerItem(int id, string str)
    {
        this.id = id;
        this.str = str;
    }
}

public struct RemovePlayerItem : IEventMessage
{
    int id;
    public RemovePlayerItem(int id)
    {
        this.id = id;
    }
}

public struct TurnEvent : IEventMessage
{
    public int playerId;
    public TurnEvent(int playerId)
    {
        this.playerId = playerId;
    }
}

#region Widgets

public struct ChatEvent : IEventMessage
{
    public int id;
    public string content;
    public ChatEvent(int id, string content)
    {
        this.id = id;
        this.content = content;
    }
}

public struct TriggerAnimEvent : IEventMessage
{
    public int animId;
    public TriggerAnimEvent(int animId)
    {
        this.animId = animId;
    }
}

#endregion
