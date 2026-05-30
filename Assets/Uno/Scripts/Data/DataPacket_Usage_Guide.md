# DataPacket 数据包序列化工具 - 使用说明





## 概述





`DataPacket` 是一个功能完整的序列化和反序列化工具类，用于在网络通信、数据存储等场景中转换各种数据类型为字节流。





---





## 核心特性





✅ 支持基础数据类型（bool, byte, short, int, long, float, double）


✅ 支持字符串和字节数组


✅ 支持Vector2、Vector3等Unity类型


✅ 支持列表容器（List<int>、List<string>）


✅ 支持自定义对象序列化（通过ISerializable接口）


✅ 完整的错误检查和边界验证





---





## 快速开始





### 1. 基础使用





```csharp


// 创建数据包


DataPacket packet = new DataPacket();





// 添加数据


packet.WriteInt(100);


packet.WriteString("Hello");


packet.WriteFloat(3.14f);





// 获取字节数据（用于网络传输）


byte[] data = packet.GetData();





// -------- 接收端 --------





// 从字节数据创建新数据包


DataPacket receivedPacket = new DataPacket(data);





// 按相同顺序读取数据


int myInt = receivedPacket.ReadInt();           // 100


string myString = receivedPacket.ReadString();  // "Hello"


float myFloat = receivedPacket.ReadFloat();     // 3.14


```





---





## 支持的数据类型详解





### 基础类型





| 方法 | 参数类型 | 说明 |


|------|--------|------|


| `WriteBoolean(bool)` / `ReadBoolean()` | bool | 布尔值 (1字节) |


| `WriteByte(byte)` / `ReadByte()` | byte | 字节 (1字节) |


| `WriteShort(short)` / `ReadShort()` | short | 短整数 (2字节) |


| `WriteInt(int)` / `ReadInt()` | int | 整数 (4字节) |


| `WriteLong(long)` / `ReadLong()` | long | 长整数 (8字节) |


| `WriteFloat(float)` / `ReadFloat()` | float | 单精度浮点 (4字节) |


| `WriteDouble(double)` / `ReadDouble()` | double | 双精度浮点 (8字节) |





### 字符串和字节数组





```csharp


// 字符串 - 自动包含长度信息


packet.WriteString("UNO游戏");


string text = packet.ReadString();





// 字节数组 - 自动包含长度信息


byte[] imageData = new byte[] { 0xFF, 0xD8, 0xFF };


packet.WriteBytes(imageData);


byte[] readData = packet.ReadBytes();


```





### Vector 类型





```csharp


// Vector3 - 三个浮点数


Vector3 pos = new Vector3(10, 20, 30);


packet.WriteVector3(pos);


Vector3 readPos = packet.ReadVector3(); // (10, 20, 30)





// Vector2 - 两个浮点数


Vector2 dir = new Vector2(1, 0);


packet.WriteVector2(dir);


Vector2 readDir = packet.ReadVector2(); // (1, 0)


```





### 列表类型





```csharp


// 整数列表


List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };


packet.WriteIntList(numbers);


List<int> readNumbers = packet.ReadIntList();





// 字符串列表


List<string> names = new List<string> { "Alice", "Bob", "Charlie" };


packet.WriteStringList(names);


List<string> readNames = packet.ReadStringList();


```





---





## 自定义对象序列化





### 步骤1: 实现 ISerializable 接口





```csharp


public class PlayerCard : ISerializable


{


    public int ID { get; set; }


    public string Name { get; set; }


    public int Value { get; set; }


    public string Color { get; set; }


    public Vector2 Position { get; set; }





    // 序列化 - 写入数据


    public void Serialize(DataPacket packet)


    {


        packet.WriteInt(ID);


        packet.WriteString(Name);


        packet.WriteInt(Value);


        packet.WriteString(Color);


        packet.WriteVector2(Position);


    }





    // 反序列化 - 读取数据


    public void Deserialize(DataPacket packet)


    {


        ID = packet.ReadInt();


        Name = packet.ReadString();


        Value = packet.ReadInt();


        Color = packet.ReadString();


        Position = packet.ReadVector2();


    }


}


```





### 步骤2: 使用序列化





```csharp


// 创建卡牌对象


PlayerCard card = new PlayerCard 


{ 


    ID = 1, 


    Name = "Draw Two", 


    Value = 2, 


    Color = "Red",


    Position = new Vector2(5, 5)


};





// 序列化


DataPacket packet = new DataPacket();


packet.WriteSerializable(card);





// 反序列化


packet.ResetReadPosition();


PlayerCard receivedCard = packet.ReadSerializable<PlayerCard>();


```





---





## 实际应用示例





### 示例1: 玩家加入游戏消息





```csharp


// 发送端


DataPacket packet = new DataPacket();


packet.WriteInt(1);              // 玩家ID


packet.WriteString("张三");      // 玩家昵称


packet.WriteInt(2);              // 玩家代码


packet.WriteVector3(new Vector3(0, 0, 0)); // 玩家位置





// 接收端


packet.ResetReadPosition();


int playerId = packet.ReadInt();


string nickname = packet.ReadString();


int playerCode = packet.ReadInt();


Vector3 position = packet.ReadVector3();


```





### 示例2: 玩家出牌消息





```csharp


// 发送端


DataPacket packet = new DataPacket();


packet.WriteInt(2);                    // 出牌玩家代码


packet.WriteString("Draw Two");        // 卡牌名称


packet.WriteInt(2);                    // 卡牌数值


packet.WriteString("Red");             // 卡牌颜色


packet.WriteInt(System.DateTime.Now.Millisecond); // 时间戳





// 接收端


packet.ResetReadPosition();


int playerCode = packet.ReadInt();


string cardName = packet.ReadString();


int cardValue = packet.ReadInt();


string cardColor = packet.ReadString();


int timestamp = packet.ReadInt();


```





### 示例3: 玩家手牌初始化





```csharp


// 发送端 - 多个玩家的初始手牌


DataPacket packet = new DataPacket();





// 玩家1的手牌


List<string> player1Cards = new List<string> { "Red 1", "Blue 2", "Green Skip" };


packet.WriteStringList(player1Cards);





// 玩家2的手牌


List<string> player2Cards = new List<string> { "Yellow 3", "Red Draw Two" };


packet.WriteStringList(player2Cards);





// 接收端


packet.ResetReadPosition();


List<string> myCards = packet.ReadStringList();


List<string> otherPlayerCards = packet.ReadStringList();


```





---





## 工具方法





### 数据包信息查询





```csharp


// 获取缓冲区数据


byte[] data = packet.GetData();





// 获取缓冲区长度（字节数）


int length = packet.GetLength();





// 获取剩余可读字节数


int remaining = packet.GetRemainingBytes();





// 重置读取位置到开始


packet.ResetReadPosition();





// 清空所有数据


packet.Clear();


```





### 消息ID管理





```csharp


// 设置消息ID


packet.PacketID = 1001;





// 获取消息ID


int id = packet.PacketID;


```





---





## 重要注意事项





### ✅ 正确用法





```csharp


// 1. 写入和读取顺序必须一致


DataPacket packet = new DataPacket();


packet.WriteInt(100);


packet.WriteString("test");


packet.ResetReadPosition();


int i = packet.ReadInt();      // 正确：100


string s = packet.ReadString(); // 正确："test"





// 2. 自定义对象必须实现 ISerializable


public class MyClass : ISerializable { ... }





// 3. 反序列化泛型需要 new() 约束


public T Read<T>() where T : ISerializable, new() { ... }


```





### ❌ 常见错误





```csharp


// 错误1: 读取顺序不对


packet.WriteInt(100);


packet.WriteString("test");


packet.ResetReadPosition();


string s = packet.ReadString(); // 错误！应该先读Int


int i = packet.ReadInt();





// 错误2: 没有重置读取位置


packet.WriteInt(100);


int value = packet.ReadInt(); // 错误！没有重置





// 错误3: null 对象处理


packet.WriteString(null);  // 写入null


string text = packet.ReadString(); // 返回null，需要检查


if (text != null) { /* 使用 */ }


```





---





## 性能优化建议





1. **避免频繁创建数据包**


   ```csharp


   // 不好：每次循环创建新对象


   for (int i = 0; i < 1000; i++)


   {


       DataPacket p = new DataPacket();


       // ...


   }





   // 好：重用数据包


   DataPacket packet = new DataPacket();


   for (int i = 0; i < 1000; i++)


   {


       packet.Clear();


       // ...


   }


   ```





2. **大量数据建议使用字节数组**


   ```csharp


   // 适合大数据


   packet.WriteBytes(largeImageData);


   ```





3. **考虑数据压缩**


   ```csharp


   // 对于网络传输的大型数据包，考虑压缩


   byte[] compressedData = CompressData(packet.GetData());


   ```





---





## 扩展示例：完整的网络消息系统





```csharp


public abstract class NetworkMessage


{


    public int MessageType { get; set; }


    


    public abstract void Serialize(DataPacket packet);


    public abstract void Deserialize(DataPacket packet);


    


    public byte[] ToBytes()


    {


        DataPacket packet = new DataPacket();


        packet.WriteInt(MessageType);


        Serialize(packet);


        return packet.GetData();


    }


    


    public static T FromBytes<T>(byte[] data) where T : NetworkMessage, new()


    {


        DataPacket packet = new DataPacket(data);


        int msgType = packet.ReadInt();


        T message = new T();


        message.MessageType = msgType;


        message.Deserialize(packet);


        return message;


    }


}





// 具体消息类型


public class PlayerJoinMessage : NetworkMessage


{


    public int PlayerId { get; set; }


    public string PlayerName { get; set; }


    


    public PlayerJoinMessage()


    {


        MessageType = 101;


    }


    


    public override void Serialize(DataPacket packet)


    {


        packet.WriteInt(PlayerId);


        packet.WriteString(PlayerName);


    }


    


    public override void Deserialize(DataPacket packet)


    {


        PlayerId = packet.ReadInt();


        PlayerName = packet.ReadString();


    }


}


```





---





## 常见问题





**Q: 为什么字符串和字节数组前面要添加长度？**


A: 这样接收端知道要读取多少字节，防止数据越界。





**Q: 支持null值吗？**


A: 字符串、字节数组和自定义对象支持null。列表返回null表示未初始化。





**Q: 如何处理大型复杂数据结构？**


A: 将其分解为多个子对象，每个子对象实现ISerializable接口。





**Q: 数据包有大小限制吗？**


A: 理论上支持到int.MaxValue字节。实际限制取决于网络条件。





---





## 总结





DataPacket 提供了一个强大而易用的序列化框架。只需遵循以下简单规则：





1. 写入和读取顺序保持一致


2. 自定义对象实现 ISerializable 接口


3. 检查null值并进行适当处理


4. 使用 ResetReadPosition() 重新读取数据





这样就能轻松地处理任何复杂的数据序列化需求！


