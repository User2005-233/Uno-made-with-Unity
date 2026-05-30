using System;

using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// DataPacket扩展类 - 提供高级数据类型的序列化支持
/// </summary>
public static class DataPacketExtensions

{
    #region 数组写入方法

    /// <summary>
    /// 写入整型数组
    /// </summary>
    public static void WriteIntArray(this DataPacket packet, int[] values)
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (int value in values)
        {
            packet.WriteInt(value);
        }
    }

    /// <summary>
    /// 写入浮点型数组
    /// </summary>
    public static void WriteFloatArray(this DataPacket packet, float[] values)
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (float value in values)
        {
            packet.WriteFloat(value);
        }
    }

    /// <summary>
    /// 写入字符串数组
    /// </summary>
    public static void WriteStringArray(this DataPacket packet, string[] values)
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (string value in values)
        {
            packet.WriteString(value);
        }
    }

    /// <summary>
    /// 写入布尔型数组
    /// </summary>
    public static void WriteBooleanArray(this DataPacket packet, bool[] values)
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (bool value in values)
        {
            packet.WriteBoolean(value);
        }
    }

    /// <summary>
    /// 写入Vector3数组
    /// </summary>
    public static void WriteVector3Array(this DataPacket packet, Vector3[] values)
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (Vector3 value in values)

        {
            packet.WriteVector3(value);
        }
    }

    /// <summary>
    /// 写入Vector2数组
    /// </summary>
    public static void WriteVector2Array(this DataPacket packet, Vector2[] values)
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (Vector2 value in values)

        {
            packet.WriteVector2(value);
        }
    }

    /// <summary>
    /// 写入可序列化对象数组
    /// </summary>
    public static void WriteSerializableArray<T>(this DataPacket packet, T[] values) where T : ISerializable
    {
        if (values == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(values.Length);

        foreach (T value in values)

        {
            packet.WriteSerializable(value);
        }
    }

    #endregion

    #region 数组读取方法

    /// <summary>
    /// 读取整型数组
    /// </summary>
    public static int[] ReadIntArray(this DataPacket packet)
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        int[] array = new int[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadInt();
        }

        return array;
    }

    /// <summary>
    /// 读取浮点型数组
    /// </summary>
    public static float[] ReadFloatArray(this DataPacket packet)
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        float[] array = new float[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadFloat();
        }

        return array;
    }

    /// <summary>
    /// 读取字符串数组
    /// </summary>
    public static string[] ReadStringArray(this DataPacket packet)
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        string[] array = new string[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadString();
        }

        return array;
    }

    /// <summary>
    /// 读取布尔型数组
    /// </summary>
    public static bool[] ReadBooleanArray(this DataPacket packet)
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        bool[] array = new bool[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadBoolean();
        }

        return array;
    }

    /// <summary>
    /// 读取Vector3数组
    /// </summary>
    public static Vector3[] ReadVector3Array(this DataPacket packet)
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        Vector3[] array = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadVector3();
        }

        return array;
    }

    /// <summary>
    /// 读取Vector2数组
    /// </summary>
    public static Vector2[] ReadVector2Array(this DataPacket packet)
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        Vector2[] array = new Vector2[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadVector2();
        }

        return array;
    }

    /// <summary>
    /// 读取可序列化对象数组
    /// </summary>
    public static T[] ReadSerializableArray<T>(this DataPacket packet) where T : ISerializable, new()
    {
        int length = packet.ReadInt();
        if (length == 0)
            return null;

        T[] array = new T[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = packet.ReadSerializable<T>();
        }

        return array;
    }

    #endregion

    #region 字典写入方法

    /// <summary>
    /// 写入字典 (int, int)
    /// </summary>
    public static void WriteDictionary(this DataPacket packet, Dictionary<int, int> dict)
    {
        if (dict == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(dict.Count);

        foreach (var kvp in dict)

        {
            packet.WriteInt(kvp.Key);

            packet.WriteInt(kvp.Value);
        }
    }

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵壯勭⊕閸 (string string
    /// </summary>
    public static void WriteDictionary(this DataPacket packet, Dictionary<string, string> dict)
    {
        if (dict == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(dict.Count);

        foreach (var kvp in dict)

        {
            packet.WriteString(kvp.Key);

            packet.WriteString(kvp.Value);
        }
    }

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵壯勭⊕閸 (int string
    /// </summary>
    public static void WriteDictionary(this DataPacket packet, Dictionary<int, string> dict)
    {
        if (dict == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(dict.Count);

        foreach (var kvp in dict)

        {
            packet.WriteInt(kvp.Key);

            packet.WriteString(kvp.Value);
        }
    }

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵壯勭⊕閸 (string int
    /// </summary>
    public static void WriteDictionary(this DataPacket packet, Dictionary<string, int> dict)
    {
        if (dict == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(dict.Count);

        foreach (var kvp in dict)

        {
            packet.WriteString(kvp.Key);

            packet.WriteInt(kvp.Value);
        }
    }

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵壯勭⊕閸 (int float
    /// </summary>
    public static void WriteDictionary(this DataPacket packet, Dictionary<int, float> dict)
    {
        if (dict == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(dict.Count);

        foreach (var kvp in dict)

        {
            packet.WriteInt(kvp.Key);

            packet.WriteFloat(kvp.Value);
        }
    }

    #endregion

    #region 闁诲孩绋掗崥宀冮庢焿閹绘帞鎮奸梺鍝勫介妴

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸柣搴㈢⊕閸 (int int
    /// </summary>
    public static Dictionary<int, int> ReadDictionaryIntInt(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        Dictionary<int, int> dict = new Dictionary<int, int>();

        for (int i = 0; i < count; i++)
        {
            int key = packet.ReadInt();
            int value = packet.ReadInt();
            dict[key] = value;
        }

        return dict;
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸柣搴㈢⊕閸 (string string
    /// </summary>
    public static Dictionary<string, string> ReadDictionaryStringString(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        Dictionary<string, string> dict = new Dictionary<string, string>();

        for (int i = 0; i < count; i++)
        {
            string key = packet.ReadString();
            string value = packet.ReadString();
            dict[key] = value;
        }

        return dict;
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸柣搴㈢⊕閸 (int string
    /// </summary>
    public static Dictionary<int, string> ReadDictionaryIntString(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        Dictionary<int, string> dict = new Dictionary<int, string>();

        for (int i = 0; i < count; i++)
        {
            int key = packet.ReadInt();
            string value = packet.ReadString();
            dict[key] = value;
        }

        return dict;
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸柣搴㈢⊕閸 (string int
    /// </summary>
    public static Dictionary<string, int> ReadDictionaryStringInt(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        Dictionary<string, int> dict = new Dictionary<string, int>();

        for (int i = 0; i < count; i++)
        {
            string key = packet.ReadString();
            int value = packet.ReadInt();
            dict[key] = value;
        }

        return dict;
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸柣搴㈢⊕閸 (int float
    /// </summary>
    public static Dictionary<int, float> ReadDictionaryIntFloat(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        Dictionary<int, float> dict = new Dictionary<int, float>();

        for (int i = 0; i < count; i++)
        {
            int key = packet.ReadInt();
            float value = packet.ReadFloat();
            dict[key] = value;
        }

        return dict;
    }

    #endregion

    #region 濞存粌鎼閻鐓幥庨梺杞扮劍婢瑰棛鍒掑ュ婃橀悷娆

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵夈倓姹夌规繛锝夋煛娴ｅ摜鎽犻弳鍌滃枛瀵閹电紒
    /// </summary>
    public static void WriteInt2DArray(this DataPacket packet, int[,] array)
    {
        if (array == null)

        {
            packet.WriteInt(0);

            packet.WriteInt(0);

            return;
        }

        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        packet.WriteInt(rows);

        packet.WriteInt(cols);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                packet.WriteInt(array[i, j]);
            }
        }
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮煎ù婊冩惈閻鐓幥庨梺杞扮侀悺銊︽畯閻旂厧鏋侀幍缂
    /// </summary>
    public static int[,] ReadInt2DArray(this DataPacket packet)
    {
        int rows = packet.ReadInt();
        int cols = packet.ReadInt();
        if (rows == 0 || cols == 0)
            return null;

        int[,] array = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = packet.ReadInt();
            }
        }

        return array;
    }

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵夈倓姹夌规繛锝勬叏閹褌鍖栭梺杞拌兌婢ф佹畯    /// </summary>
    public static void WriteFloat2DArray(this DataPacket packet, float[,] array)
    {
        if (array == null)

        {
            packet.WriteInt(0);

            packet.WriteInt(0);

            return;
        }

        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        packet.WriteInt(rows);

        packet.WriteInt(cols);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                packet.WriteFloat(array[i, j]);
            }
        }
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮煎ù婊冩惈閻鐓幥庢穱閹褌鍖栭梺杞拌兌婢ф佹畯    /// </summary>
    public static float[,] ReadFloat2DArray(this DataPacket packet)
    {
        int rows = packet.ReadInt();
        int cols = packet.ReadInt();
        if (rows == 0 || cols == 0)
            return null;

        float[,] array = new float[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = packet.ReadFloat();
            }
        }

        return array;
    }

    #endregion

    #region 閹垮啯浼存偡閻楀牏褰滈梺鍝勫介妴

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵夆晛绀嗛柕鍡楀暟闁(List<List<int>>)
    /// </summary>
    public static void WriteListOfLists(this DataPacket packet, List<List<int>> lists)
    {
        if (lists == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(lists.Count);

        foreach (var list in lists)

        {
            packet.WriteIntList(list);
        }
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸梺鍛婂竾閸愰柛(List<List<int>>)
    /// </summary>
    public static List<List<int>> ReadListOfLists(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        List<List<int>> lists = new List<List<int>>();
        for (int i = 0; i < count; i++)
        {
            lists.Add(packet.ReadIntList());
        }

        return lists;
    }

    /// <summary>
    /// 闂佸憡鍔栭悷銉ュ弿閵夆晛绀嗛柕鍡楀暟闁(List<List<string>>)
    /// </summary>
    public static void WriteStringListOfLists(this DataPacket packet, List<List<string>> lists)
    {
        if (lists == null)

        {
            packet.WriteInt(0);

            return;
        }

        packet.WriteInt(lists.Count);

        foreach (var list in lists)

        {
            packet.WriteStringList(list);
        }
    }

    /// <summary>
    /// 鐠囬庢焿閹绘帞鎮奸梺鍛婂竾閸愰柛(List<List<string>>)
    /// </summary>
    public static List<List<string>> ReadStringListOfLists(this DataPacket packet)
    {
        int count = packet.ReadInt();
        if (count == 0)
            return null;

        List<List<string>> lists = new List<List<string>>();
        for (int i = 0; i < count; i++)
        {
            lists.Add(packet.ReadStringList());
        }

        return lists;
    }

    #endregion
}

