using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// DataPacket使用示例类
/// 演示如何使用DataPacket进行各种数据类型的序列化和反序列化
/// </summary>
public class DataPacketExample : MonoBehaviour

{
    void Start()
    {
        // 示例1: 基本数据类型
        BasicDataTypesExample();

        // 示例2: 复杂数据类型
        ComplexDataTypesExample();

        // 示例3: 自定义对象
        CustomObjectExample();

        // 示例4: 网络消息
        NetworkMessageExample();
    }

    /// <summary>
    /// 示例1: 基本数据类型序列化演示
    /// </summary>
    void BasicDataTypesExample()
    {
        Debug.Log("========== 基本数据类型序列化示例 ==========");

        DataPacket packet = new DataPacket();

        // 写入各种基本数据类型
        packet.WriteBoolean(true);           // 布尔值
        packet.WriteByte(255);               // 字节
        packet.WriteShort(32767);            // 短整型
        packet.WriteInt(2147483647);         // 整型
        packet.WriteLong(9223372036854775807); // 长整型
        packet.WriteFloat(3.14159f);         // 浮点型
        packet.WriteDouble(2.71828);         // 双精度浮点型
        packet.WriteString("Hello World!");  // 字符串

        // 重置读取位置准备读取数据
        packet.ResetReadPosition();

        // 读取数据并验证
        bool boolValue = packet.ReadBoolean();
        byte byteValue = packet.ReadByte();

        short shortValue = packet.ReadShort();

        int intValue = packet.ReadInt();
        long longValue = packet.ReadLong();

        float floatValue = packet.ReadFloat();
        double doubleValue = packet.ReadDouble();
        string stringValue = packet.ReadString();
        Debug.Log($"閸愩劎姣: {boolValue}");
        Debug.Log($"閻: {byteValue}");
        Debug.Log($"闁婚柡: {shortValue}");
        Debug.Log($"闁: {intValue}");
        Debug.Log($"闂傞幐搴㈡: {longValue}");
        Debug.Log($"濞村鎮: {floatValue}");
        Debug.Log($"闁告瑥鐬肩花鍖￠檮鐠囩偤鎮: {doubleValue}");
        Debug.Log($"閻庢稓澧: {stringValue}");
    }

    /// <summary>
    /// 缂佹潪鎵浼2: 濠㈣泛绉靛煎懘寮閻楀牏鐚剧拠鑼椋庢畱閸曠花闁告帗鐎垫煡宕鐏炶棄鍐閸ㄩ柛
    /// </summary>
    void ComplexDataTypesExample()
    {
        Debug.Log("\n========== 濠㈣泛绉靛煎懘寮閻楀牏鐚剧拠鑼椋庣矆鏉炴壆浼 ==========");

        DataPacket packet = new DataPacket();

        // 闁告劖鐟ラ崣鍝ector
        Vector3 position = new Vector3(10, 20, 30);

        Vector2 direction = new Vector2(1, 1);

        packet.WriteVector3(position);

        packet.WriteVector2(direction);

        // 闁告劖鐟ラ崣鍡涘礆濡ゅ嫨
        List<int> intList = new List<int> { 1, 2, 3, 4, 5 };

        List<string> stringList = new List<string> { "Alice", "Bob", "Charlie" };

        packet.WriteIntList(intList);

        packet.WriteStringList(stringList);

        // 闂佹彃绉堕悿鍡涚嵁閹鎷屾彃绲
        packet.ResetReadPosition();

        Vector3 readPosition = packet.ReadVector3();

        Vector2 readDirection = packet.ReadVector2();

        List<int> readIntList = packet.ReadIntList();
        List<string> readStringList = packet.ReadStringList();
        Debug.Log($"濞: {readPosition}");
        Debug.Log($"闁: {readDirection}");
        Debug.Log($"闁轰礁鐡ㄩ弳鐔煎礆: {string.Join(", ", readIntList)}");
        Debug.Log($"閻庢稓澧濋敂鑳鍡涘礆: {string.Join(", ", readStringList)}");
    }

    /// <summary>
    /// 缂佹潪鎵浼3: 闁奸攱鐭缁犵喓鍨宓侀挅鍕鐏欓柛
    /// </summary>
    void CustomObjectExample()
    {
        Debug.Log("\n========== 闁奸攱鐭缁犵喓鍨宓侀挅鍕鐏欓柛鏍ㄧ墱閵囨碍绗 ==========");

        DataPacket packet = new DataPacket();

        // 闁告帗绋戠紓鎾跺负鐠虹儤宓侀挅鍕鐛鐠鸿櫣纰嶉柛鎺撶
        PlayerData player = new PlayerData

        {
            PlayerID = 1,
            Name = "瀵",
            Health = 100,
            Position = new Vector3(5, 0, 10)

        };

        packet.WriteSerializable(player);

        // 闁告瑥绉寸花闁告帗鐎
        packet.ResetReadPosition();

        PlayerData deserializedPlayer = packet.ReadSerializable<PlayerData>();

        Debug.Log($"閻溾晝婊: {deserializedPlayer.PlayerID}");
        Debug.Log($"閻溾晞娉涢幃: {deserializedPlayer.Name}");
        Debug.Log($"閻: {deserializedPlayer.Health}");
        Debug.Log($"濞: {deserializedPlayer.Position}");
    }

    /// <summary>
    /// 缂佹潪鎵浼4: 缂傚啯鍨圭划璺衡槈閸鐔肺熼幁鐎
    /// 婵￠幁鐎氭瑤绗夋稉宥呮湰閺嗭絿娈戠紞澶岀磼濠婂嫮啸闁挎稒姘ㄧ敮楦挎硾閸ら悧灞藉旀穱濠囧箒
    /// </summary>
    void NetworkMessageExample()
    {
        Debug.Log("\n========== 缂傚啯鍨圭划璺衡槈閸鐔虹矆鏉炴壆浼 ==========");

        // 闁告瑦鍨挎笟濠勭獥闁告帗绋戠紓鎾冲毉閾忓湱澧濇繛鎴濈墛
        DataPacket messagePacket = new DataPacket();

        messagePacket.PacketID = 1001; // 婵炴垵鐗奍D

        // 闁告劖鐟ラ崣鍡楀毉閾忓湱澧濇穱鈩冧紖
        int playerCode = 2;

        string cardName = "Draw Two - Red";

        int cardValue = 2;

        string cardColor = "Red";

        messagePacket.WriteInt(playerCode);

        messagePacket.WriteString(cardName);

        messagePacket.WriteInt(cardValue);

        messagePacket.WriteString(cardColor);

        byte[] messageData = messagePacket.GetData();

        Debug.Log($"婵炴垵鐗婂: {messageData.Length} 閻");
        // 闁规亽鍎查弫褰掓晬濮樻壙鎺楀几閸旂喎姣夐悧灞芥湰缁夌兘骞
        DataPacket receivedPacket = new DataPacket(messageData);

        int receivedPlayerCode = receivedPacket.ReadInt();
        string receivedCardName = receivedPacket.ReadString();
        int receivedCardValue = receivedPacket.ReadInt();
        string receivedCardColor = receivedPacket.ReadString();
        Debug.Log($"闁规亽鍎查弫鍦娈戠敮鐑樻构閸: {receivedPlayerCode}");
        Debug.Log($"闁规亽鍎查弫鍦娈戦崟瀹曡京澧濈仦鑺ュ: {receivedCardName}");
        Debug.Log($"闁规亽鍎查弫鍦娈戦崟瀹曡京澧濈仦鐐娈: {receivedCardValue}");
        Debug.Log($"闁规亽鍎查弫鍦娈戦崟瀹曡京澧濆畝: {receivedCardColor}");
    }
}

/// <summary>
/// 缂佹潪鎵浼愰悳鈺勫煐閺嗙喖骞戠紒 - 閻愰潧绠汭Serializable闁规亽鍎辫ぐ
/// </summary>
public class PlayerData : ISerializable

{
    public int PlayerID { get; set; }

    public string Name { get; set; }

    public int Health { get; set; }

    public Vector3 Position { get; set; }

    public void Serialize(DataPacket packet)
    {
        packet.WriteInt(PlayerID);

        packet.WriteString(Name);

        packet.WriteInt(Health);

        packet.WriteVector3(Position);
    }

    public void Deserialize(DataPacket packet)
    {
        PlayerID = packet.ReadInt();

        Name = packet.ReadString();

        Health = packet.ReadInt();

        Position = packet.ReadVector3();
    }
}