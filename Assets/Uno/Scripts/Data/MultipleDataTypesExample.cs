using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// 数据包序列化示例类
/// 演示如何使用DataPacket进行各种数据类型的序列化
/// </summary>
public class MultipleDataTypesExample : MonoBehaviour

{
    void Start()
    {
        // 示例1: 数组序列化
        ArrayExample();

        // 示例2: 字典序列化
        DictionaryExample();

        // 示例3: 二维数组序列化
        Array2DExample();

        // 示例4: 嵌套容器序列化
        NestedContainerExample();

        // 示例5: 卡牌游戏数据序列化
        CardGameExample();

        // 示例6: 同步玩家手牌数据
        SyncPlayerHandsExample();
    }

    /// <summary>
    /// 示例1: 数组序列化 - 演示如何序列化数组数据
    /// </summary>
    void ArrayExample()
    {
        Debug.Log("========== 数组序列化示例 ==========");

        DataPacket packet = new DataPacket();

        // 创建玩家得分数组
        int[] playerScores = new int[] { 100, 250, 180, 350, 200 };

        packet.WriteIntArray(playerScores);

        // 创建伤害值数组
        float[] damageValues = new float[] { 10.5f, 20.3f, 15.7f };

        packet.WriteFloatArray(damageValues);

        // 创建卡牌名称数组
        string[] cardNames = new string[] { "Red 1", "Blue 2", "Green Skip", "Yellow Draw Two" };

        packet.WriteStringArray(cardNames);

        // 创建能力激活状态数组
        bool[] powerUpsActive = new bool[] { true, false, true, true, false };

        packet.WriteBooleanArray(powerUpsActive);

        // 重置读取位置
        packet.ResetReadPosition();

        // 读取数据
        int[] readScores = packet.ReadIntArray();

        float[] readDamages = packet.ReadFloatArray();

        string[] readCards = packet.ReadStringArray();

        bool[] readPowerUps = packet.ReadBooleanArray();

        Debug.Log($"读取的分数: {string.Join(", ", readScores)}");
        Debug.Log($"读取的伤害: {string.Join(", ", readDamages)}");
        Debug.Log($"读取的卡牌: {string.Join(", ", readCards)}");
        Debug.Log($"读取的能力: {string.Join(", ", readPowerUps)}");
    }

    /// <summary>
    /// 缂佹潪鎵浼2: 閻庢稒閸氬㈣埖鍊 - 闂佸潡宕愰悡搴℃贡濠㈣埖鐭闁叉粓寮閻
    /// </summary>
    void DictionaryExample()
    {
        Debug.Log("\n========== 閻庢稒閸氬㈣埖鍊炵紒鏉炴壆浼 ==========");

        DataPacket packet = new DataPacket();

        // 缂佹潪鎵浼 1: 閻溾晝婊 -> 闁告帒妫欓弳 闁哄嫬宕遍惃
        Dictionary<int, int> playerScores = new Dictionary<int, int>

        {
            { 1, 500 },

            { 2, 750 },

            { 3, 600 },

            { 4, 800 }

        };

        packet.WriteDictionary(playerScores);

        // 缂佹潪鎵浼 2: 闁告嬬磿閻楀苯鎮撳ュ泦 -> 闁告嬬磿閻楀矂骞撹箛姘鐗 闁哄嫬宕遍惃
        Dictionary<string, string> cardDescriptions = new Dictionary<string, string>

        {
            { "Draw Two", "閹惰姤鍨濈悮鍗炵磻" },

            { "Skip", "閻犲搫鐤囩换鍐х瑝鐎ｂ晝缈犵瑝閻" },

            { "Reverse", "闁告瑥绉撮幃婊冨毉閾忓湱澧濆" }

        };

        packet.WriteDictionary(cardDescriptions);

        // 缂佹潪鎵浼 3: 閻溾晝婊 -> 閻溾晞鍩栧Ο缂 闁哄嫬宕遍惃
        Dictionary<int, string> playerNames = new Dictionary<int, string>

        {
            { 1, "Alice" },

            { 2, "Bob" },

            { 3, "Charlie" }

        };

        packet.WriteDictionary(playerNames);

        // 缂佹潪鎵浼 4: 闁告嬬磿閻楀苯鎮撳ュ泦 -> 闁告挴鏅欑紞鎴﹀极娴兼潙娅 闁哄嫬宕遍惃
        Dictionary<string, int> cardCounts = new Dictionary<string, int>

        {
            { "Red 1", 4 },

            { "Blue 2", 2 },

            { "Green Skip", 1 }

        };

        packet.WriteDictionary(cardCounts);

        // 缂佹潪鎵浼 5: 閻溾晝婊 -> 闁稿ㄥ劚閹宥夊磹 闁哄嫬宕遍惃
        Dictionary<int, float> playerHealth = new Dictionary<int, float>

        {
            { 1, 100f },

            { 2, 85.5f },

            { 3, 92.3f }

        };

        packet.WriteDictionary(playerHealth);

        // 闂佹彃绉堕悿鍡氱柉鎻掔悼
        packet.ResetReadPosition();

        // 鐠囩柉鎻掔悼閻庢稒閸
        Dictionary<int, int> readScores = packet.ReadDictionaryIntInt();

        Dictionary<string, string> readDescriptions = packet.ReadDictionaryStringString();

        Dictionary<int, string> readNames = packet.ReadDictionaryIntString();

        Dictionary<string, int> readCounts = packet.ReadDictionaryStringInt();

        Dictionary<int, float> readHealth = packet.ReadDictionaryIntFloat();

        Debug.Log($"閻溾晞娉涢崹: {string.Join(", ", readScores)}");
        Debug.Log($"闁告嬬磿閻楀矂骞: {string.Join(", ", readDescriptions)}");
        Debug.Log($"閻溾晞娉涢幃: {string.Join(", ", readNames)}");
        Debug.Log($"闁告嬬磿閻楀矂寮: {string.Join(", ", readCounts)}");
        Debug.Log($"閻溾晞娉涙禒: {string.Join(", ", readHealth)}");
    }

    /// <summary>
    /// 缂佹潪鎵浼3: 濞存粌鐬煎ǎ闁轰焦澹嗙划 - 濞撳憡鍨欐俊鐎ｃ劋绀侀崣褔宕￠垾铏鍕鹃崶鐐插⒔閻
    /// </summary>
    void Array2DExample()
    {
        Debug.Log("\n========== 濞存粌鐬煎ǎ闁轰焦澹嗙划宥囩矆鏉炴壆浼 ==========");

        DataPacket packet = new DataPacket();

        // 缂佹潪鎵浼 1: 濞撳憡鍨欐俊鐎ｅ墎绀勫┑鈥冲介挅鍕濡存担椋庡畨閻庢稒鍔х槐
        int[,] gameBoard = new int[8, 8]

        {
            { 1, 2, 3, 4, 5, 6, 7, 8 },

            { 0, 0, 0, 0, 0, 0, 0, 0 },

            { 0, 0, 0, 0, 0, 0, 0, 0 },

            { 0, 0, 0, 1, 2, 0, 0, 0 },

            { 0, 0, 0, 2, 1, 0, 0, 0 },

            { 0, 0, 0, 0, 0, 0, 0, 0 },

            { 0, 0, 0, 0, 0, 0, 0, 0 },

            { 8, 7, 6, 5, 4, 3, 2, 1 }

        };

        packet.WriteInt2DArray(gameBoard);

        // 缂佹潪鎵浼 2: 闁革附婢樺ù妯诲倹锚鐎规娊寮鐢缁辨瑩宕烽弶鑳鍩屽Δ鍌涳公缁
        float[,] heightMap = new float[5, 5]

        {
            { 0f, 0.5f, 1f, 0.5f, 0f },

            { 0.5f, 1.5f, 2f, 1.5f, 0.5f },

            { 1f, 2f, 3f, 2f, 1f },

            { 0.5f, 1.5f, 2f, 1.5f, 0.5f },

            { 0f, 0.5f, 1f, 0.5f, 0f }

        };

        packet.WriteFloat2DArray(heightMap);

        // 闂佹彃绉堕悿鍡氱柉鎻掔悼
        packet.ResetReadPosition();

        // 读取数据
        int[,] readBoard = packet.ReadInt2DArray();

        float[,] readHeights = packet.ReadFloat2DArray();

        Debug.Log($"婵℃刊0,0]: {readBoard[0, 0]}");
        Debug.Log($"婵℃刊3,3]: {readBoard[3, 3]}");
        Debug.Log($"闁革附婢橀懜鐗堝倹2,2]: {readHeights[2, 2]}");
    }

    /// <summary>
    /// 缂佹潪鎵浼4: 鐎规挸鑻濡ら悷鐗堢彜 - 婵锝呯箣闁叉粎甯洪懜鍨绠掑㈣埖鑹剧槐鍫曞础閿涘嫮澧
    /// </summary>
    void NestedContainerExample()
    {
        Debug.Log("\n========== 鐎规挸鑻濡ら悷鐗堢彜缂佹潪鎵浼 ==========");

        DataPacket packet = new DataPacket();

        // 缂侀悮瀵哥獥濞撳憡鍨欓幋蹇撴健濡澶哥瑝濠㈣埖鐭闁叉粎甯洪崜浣瑰嶇ｉ悧
        List<List<int>> allPlayersCards = new List<List<int>>

        {
            new List<int> { 1, 2, 3, 4, 5 },      // 閻1閻ㄥ嫬婊呭濈涙湆
            new List<int> { 6, 7, 8 },            // 閻2閻ㄥ嫬婊呭濈涙湆
            new List<int> { 9, 10, 11, 12 },      // 閻3閻ㄥ嫬婊呭濈涙湆
            new List<int> { 13, 14 }              // 閻4閻ㄥ嫬婊呭濈涙湆

        };

        packet.WriteListOfLists(allPlayersCards);

        // 缂侀悮瀵哥獥婵锝呯箣闁叉粎甯洪崜渚宕￠敍鍕澧濋崥灞界Ф琚ㄩ柛鎺撳嫨
        List<List<string>> playerCardNames = new List<List<string>>

        {
            new List<string> { "Red 1", "Blue 2", "Green 3" },
            new List<string> { "Yellow 4", "Red 5" },
            new List<string> { "Skip", "Draw Two", "Reverse" }

        };

        packet.WriteStringListOfLists(playerCardNames);

        // 闂佹彃绉堕悿鍡氱柉鎻掔悼
        packet.ResetReadPosition();

        // 读取数据
        List<List<int>> readAllCards = packet.ReadListOfLists();
        List<List<string>> readCardNames = packet.ReadStringListOfLists();
        Debug.Log($"閻1閻ㄥ嫬瀚瀹曡京澧濈涙湆: {string.Join(", ", readAllCards[0])}");
        Debug.Log($"閻2閻ㄥ嫬瀚瀹曡京澧濈涙湆: {string.Join(", ", readAllCards[1])}");
        Debug.Log($"閻1閻ㄥ嫬瀚瀹曡京澧濈仦鑺ュ: {string.Join(", ", readCardNames[0])}");
    }

    /// <summary>
    /// 缂佹潪鎵浼5: 濞撳憡鍨欓柛妤嬬磿閻楀瞼鍖栭懡銈囧煚 - 濠㈣埖鑹剧槐鍫曞础閿涘嫮澧濋悗鍨宓侀挅
    /// </summary>
    void CardGameExample()
    {
        Debug.Log("\n========== 濞撳憡鍨欓柛妤嬬磿閻楀瞼鍖栭懡銈囧煚缂佹潪鎵浼 ==========");

        DataPacket packet = new DataPacket();

        // 闁告帗绋戠紓鎾村緞濮樼槐鍫曞础閿涘嫮澧
        UnoCard[] cards = new UnoCard[]

        {
            new UnoCard { ID = 1, Name = "Red 1", Value = 1, Color = "Red" },
            new UnoCard { ID = 2, Name = "Blue 2", Value = 2, Color = "Blue" },
            new UnoCard { ID = 3, Name = "Green Skip", Value = 10, Color = "Green" },
            new UnoCard { ID = 4, Name = "Yellow Draw Two", Value = 20, Color = "Yellow" }

        };

        // 閸ㄩ柛鏍ㄧ墪濮樼槐鍫曞础閿涘嫮澧
        packet.WriteSerializableArray(cards);

        // 闂佹彃绉堕悿鍡氱柉鎻掔悼
        packet.ResetReadPosition();

        // 闁告瑥绉寸花闁告帗鐎垫煡宕￠敍鍕澧
        UnoCard[] readCards = packet.ReadSerializableArray<UnoCard>();

        foreach (var card in readCards)

        {
            Debug.Log($"闁: {card.Name} (: {card.Value}, 濡: {card.Color})");
        }
    }

    /// <summary>
    /// 缂佹潪鎵浼6: 閻溾晞鍩栨晶婊呭濈仦鑺ュ辨慨 - 鐏炵偓娈婚惃鍕鍩楅幋蹇旂皻
    /// </summary>
    void SyncPlayerHandsExample()
    {
        Debug.Log("\n========== 閻溾晞鍩栨晶婊呭濈仦鑺ュ辨慨婵勫劤閵囨碍绗 ==========");

        // 闁哄瀚缂傛挸鏈閺嗭絿娈戦悥鑸靛灆婵鎼佸箑娴ｈ勬堕柟褰掑礌
        DataPacket gameStatePacket = new DataPacket();

        // 1. 闁告劖鐟ラ崣鍡樺嶉張澶婂苯璐熺粋婊
        int[] playerIds = new int[] { 1, 2, 3, 4 };

        gameStatePacket.WriteIntArray(playerIds);

        // 2. 闁告劖鐟ラ崣鍡樺嶉張澶婂苯璐熺捄鐑樺崇紒
        string[] playerNames = new string[] { "Alice", "Bob", "Charlie", "Diana" };

        gameStatePacket.WriteStringArray(playerNames);

        // 3. 闁告劖鐟ラ崣鍡欏负缁傛坏 -> 閹电ｉ悧宀勫极娴兼潙娅 闁哄嫬宕遍惃
        Dictionary<int, int> playerCardCounts = new Dictionary<int, int>

        {
            { 1, 5 },

            { 2, 7 },

            { 3, 6 },

            { 4, 4 }

        };

        gameStatePacket.WriteDictionary(playerCardCounts);

        // 4. 闁告劖鐟ラ崣鍡椥掕箛搴ㄥ殝閻溾晛澧庨幍鐎ｉ悧
        List<List<int>> allHandCards = new List<List<int>>

        {
            new List<int> { 1, 2, 3, 4, 5 },
            new List<int> { 6, 7, 8, 9, 10, 11, 12 },
            new List<int> { 13, 14, 15, 16, 17, 18 },
            new List<int> { 19, 20, 21, 22 }

        };

        gameStatePacket.WriteListOfLists(allHandCards);

        // 5. 闁告劖鐟ラ崣鍡氥亹閹惧啿鐘插毉閾忓湱澧濋柛閸℃瑩宕￠敍鍕澧
        string[] discardPile = new string[] { "Red 5", "Blue 3", "Yellow Draw Two" };

        gameStatePacket.WriteStringArray(discardPile);

        Debug.Log($"闁汇垻鍠愰崹姘辨畱閻栬埖鍨欐慨鎼佸箑娴ｈ勬堕柟褰掑礌閵: {gameStatePacket.GetLength()} 閻");
        // -------- 缂傚啯鍨圭划鑸靛奸悩铏圭炕婵￠幁鐎 --------
        byte[] transmissionData = gameStatePacket.GetData();

        // -------- 闁规亽鍎查弫鍦绮╁㈣埖鍊 --------
        DataPacket receivedPacket = new DataPacket(transmissionData);

        int[] receivedPlayerIds = receivedPacket.ReadIntArray();

        string[] receivedNames = receivedPacket.ReadStringArray();

        Dictionary<int, int> receivedCardCounts = receivedPacket.ReadDictionaryIntInt();

        List<List<int>> receivedAllCards = receivedPacket.ReadListOfLists();
        string[] receivedDiscardPile = receivedPacket.ReadStringArray();

        Debug.Log($"闁规亽鍎查弫鍦娈戠敮铏规坏: {string.Join(", ", receivedPlayerIds)}");
        Debug.Log($"闁规亽鍎查弫鍦娈戠敮楦挎硾閹: {string.Join(", ", receivedNames)}");
        Debug.Log($"閻1閻ㄥ嫬瀚瀹曡京澧: {receivedCardCounts[1]}");
        Debug.Log($"鐟滅増鎸告晶鐘插毉閾忓湱澧: {string.Join(", ", receivedDiscardPile)}");
    }
}

/// <summary>
/// UNO闁告嬬磿閻楀瞼鐚 - 閻愰潧绠汭Serializable闁规亽鍎辫ぐ
/// </summary>
public class UnoCard : ISerializable

{
    public int ID { get; set; }

    public string Name { get; set; }

    public int Value { get; set; }

    public string Color { get; set; }

    public void Serialize(DataPacket packet)
    {
        packet.WriteInt(ID);

        packet.WriteString(Name);

        packet.WriteInt(Value);

        packet.WriteString(Color);
    }

    public void Deserialize(DataPacket packet)
    {
        ID = packet.ReadInt();

        Name = packet.ReadString();

        Value = packet.ReadInt();

        Color = packet.ReadString();
    }
}