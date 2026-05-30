using System.Collections.Generic;

public class InitAction : IAction { }

/// <summary>
/// 开始主机网络事件
/// </summary>

// public class StartHostNetEvent : INetEventMessage

// {

//     string IP_Addrerss;

//     string IP_Port;

//     public StartHostNetEvent(string iP_Addrerss, string iP_Port)
//     {

//         IP_Addrerss = iP_Addrerss;

//         IP_Port = iP_Port;

//     }

// }

/// <summary>
/// 开始主机成功事件
/// </summary>
public class StartHostSucceededAction : IAction { }

//start game action
public class StartGameAction : IAction { }

/// <summary>
/// 玩家抽牌请求事件
/// </summary>
public class PlayerGetCardReqEvent : IEventMessage

{
}


/// <summary>
/// 玩家加牌事件
/// </summary>
public class PlayerPlusCardEvent : IEventMessage

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
public class PlayerPlayCardEvent : IEventMessage

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
/// 玩家出牌失败事件
/// </summary>
public class PlayerFailPlayCardAction : IAction { }

/// <summary>
/// 时间截至自动出牌
/// </summary>
public class AutoPlayEvent : IEventMessage
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
public class StartPlayerRoundEvent : IEventMessage
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
public class PlayerRemoveCardEvent : IEventMessage
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
/// 同步所有事件
/// </summary>
public class SyncAllEvent : IEventMessage

{
}

/// <summary>
/// 超时事件
/// </summary>
public class TimeOutEvent : IEventMessage

{
}

/// <summary>
/// 洗牌事件
/// </summary>
public class WashDeckEvent : IEventMessage

{
}

/// <summary>
/// 测试
/// </summary>
public class TestEvent : IEventMessage
{
    public int data;

    public TestEvent(int d)
    {
        data = d;
    }
}

