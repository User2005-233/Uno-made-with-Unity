





public struct NetEventRegister: INetEventMessage
{
    public int code;
    public NetEventRegister(int code)
    {
        this.code = code;
    }
}

// 玩家加入的事件消息
public struct PlayerJoinedMessage:INetEventMessage
{
    public int ClientId;       // FishNet 分配的唯一客户端 ID
    public string PlayerName;  // 玩家自定义名字（可选）

    public PlayerJoinedMessage(int clientId, string playerName)
    {
        ClientId = clientId;
        PlayerName = playerName;
    }
}

// 玩家退出的事件消息
public struct PlayerLeftMessage:INetEventMessage
{
    public int ClientId;       // 哪个 ID 的玩家退出了

    public PlayerLeftMessage(int clientId)
    {
        ClientId = clientId;
    }
}