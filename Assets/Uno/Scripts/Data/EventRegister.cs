using System.Collections.Generic;



/// <summary>
/// 玩家加牌事件
/// </summary>
public struct PlayerPlusCardEvent : IEventMessage

{
    public Card card;
    public int playerCode;
    public PlayerPlusCardEvent(int code, Card cardIn)
    {
        playerCode = code;

        card = cardIn;
    }
}


/// <summary>
/// 玩家出牌事件
/// </summary>
public struct PlayerPlayCardEvent : IEventMessage

{
    public CardInfo Card;
    public int Code;
    public PlayerPlayCardEvent(int code, CardInfo card)
    {
        Code = code;

        Card = card;
    }
}



/// <summary>
/// 时间截至自动出牌
/// </summary>
public struct AutoPlayEvent : IEventMessage
{
    int code;
    public AutoPlayEvent(int code)
    {
        this.code = code;
    }
}

/// <summary>
/// 开始下一玩家的出牌
/// </summary>
public struct StartPlayerRoundEvent : IEventMessage
{
    int code;
    public StartPlayerRoundEvent(int code)
    {
        this.code = code;
    }
}

/// <summary>
/// 玩家移除牌事件
/// </summary>
public struct PlayerRemoveCardEvent : IEventMessage
{
    public CardInfo Card;
    public int Code;
    public PlayerRemoveCardEvent(int code, CardInfo card)
    {
        Code = code;

        Card = card;
    }
}



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
    string str;
    public AddPlayerItem(string str)
    {
        this.str = str;
    }
}
