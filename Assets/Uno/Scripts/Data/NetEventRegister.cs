using System.Collections.Generic;


/// <summary>
/// 停止主机网络事件
/// </summary>
public class StopHostNetEvent : INetEventMessage

{
}

/// <summary>
/// 客户端连接事件
/// 包含客户端信息：
///     playerID
///     nickname
/// </summary>
public class ConnectedByClientNetEvent : INetEventMessage

{
    int playerID;

    string nickname;

    public ConnectedByClientNetEvent(int ID, string name)
    {
        playerID = ID;

        nickname = name;
    }
}

/// <summary>
/// 连接到主机成功事件
/// </summary>
public class ConnectionToHostAccomplished : INetEventMessage

{
}

/// <summary>
/// 玩家加入网络事件
/// </summary>
public class PlayerJoinedNetEvent : INetEventMessage

{
}

/// <summary>
/// 玩家退出网络事件
/// </summary>
public class PlayerExitedNetEvent : INetEventMessage

{
}

/// <summary>
/// 游戏开始网络事件
/// </summary>
public class GameStartNetEvent : INetEventMessage

{
}

/// <summary>
/// 游戏暂停网络事件
/// </summary>
public class GamePauseNetEvent : INetEventMessage

{
}

/// <summary>
/// 更改昵称图标请求网络事件
/// </summary>
public class ChangeNickname_IconReqNetEvent : INetEventMessage

{
}

/// <summary>
/// 游戏结束网络事件
/// </summary>
public class GameOverNetEvent : INetEventMessage

{
}

/// <summary>
/// 初始化手牌网络事件，包含playerCode等信息
/// </summary>
public class InitHandCardNetEvent : INetEventMessage

{
    public Dictionary<int, List<Card>> cardWrapper;

    public InitHandCardNetEvent(Dictionary<int, List<Card>> dic)
    {
        cardWrapper = new Dictionary<int, List<Card>>(dic);
    }
}

/// <summary>
/// 玩家抽牌请求网络事件
/// </summary>
public class PlayerGetCardReqNetEvent : INetEventMessage

{
}

/// <summary>
/// 玩家加牌网络事件
/// </summary>
public class PlayerPlusCardNetEvent : INetEventMessage

{
}

/// <summary>
/// 玩家出牌网络事件
/// </summary>
public class PlayerPlayCardNetEvent : INetEventMessage

{
    public CardInfo Card;

    public int Code;

    public PlayerPlayCardNetEvent(int code, CardInfo card)
    {
        Code = code;

        Card = card;
    }
}

/// <summary>
/// 同步所有网络事件
/// </summary>
public class SyncAllNetEvent : INetEventMessage

{
}

/// <summary>
/// 超时网络事件
/// </summary>
public class TimeOutNetEvent : INetEventMessage

{
}

/// <summary>
/// 洗牌网络事件
/// </summary>
public class WashDeckNetEvent : INetEventMessage

{
}