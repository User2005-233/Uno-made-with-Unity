using System;

using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// 数据包类，用于序列化和反序列化各种数据类型
/// 支持基本数据类型、数组、复杂对象的序列化操作
/// </summary>
public class DataPacket

{
    /// <summary>
    /// 内部字节缓冲区，用于存储序列化的数据
    /// </summary>
    private List<byte> buffer = new List<byte>();

    int[] IntArr;
    
    
    
    /// <summary>
    /// 当前读取位置，用于反序列化时跟踪读取进度
    /// </summary>
    private int readPosition = 0;

    /// <summary>
    /// 数据包ID，用于标识不同的数据包类型
    /// </summary>
    public int PacketID { get; set; }

    /// <summary>
    /// 构造函数 - 创建空的数据包
    /// </summary>
    public DataPacket()
    {
    }

    /// <summary>
    /// 构造函数 - 从字节数组创建数据包
    /// </summary>
    public DataPacket(byte[] data)
    {
        buffer.AddRange(data);
    }

    #region 写入方法
    /// <summary>
    /// 写入布尔值
    /// </summary>
    public void WriteBoolean(bool value)
    {
        buffer.Add(value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// 写入字节
    /// </summary>
    public void WriteByte(byte value)
    {
        buffer.Add(value);
    }

    /// <summary>
    /// 写入字节数组
    /// </summary>
    public void WriteBytes(byte[] data)
    {
        if (data == null)

        {
            WriteInt(0); // 写入0表示null
            return;
        }

        WriteInt(data.Length); // 写入数组长度
        buffer.AddRange(data);
    }

    /// <summary>
    /// 写入短整型(short)
    /// </summary>
    public void WriteShort(short value)
    {
        buffer.AddRange(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// 写入整型(int)
    /// </summary>
    public void WriteInt(int value)
    {
        buffer.AddRange(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// 写入长整型(long)
    /// </summary>
    public void WriteLong(long value)
    {
        buffer.AddRange(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// 写入浮点型(float)
    /// </summary>
    public void WriteFloat(float value)
    {
        buffer.AddRange(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// 写入双精度浮点型(double)
    /// </summary>
    public void WriteDouble(double value)
    {
        buffer.AddRange(BitConverter.GetBytes(value));
    }

    /// <summary>
    /// 写入字符串
    /// </summary>
    public void WriteString(string value)
    {
        if (value == null)

        {
            WriteInt(0); // 写入0表示null
            return;
        }

        byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(value);

        WriteInt(strBytes.Length); // 写入字符串长度

        buffer.AddRange(strBytes);
    }

    /// <summary>
    /// 写入Vector3
    /// </summary>
    public void WriteVector3(Vector3 value)
    {
        WriteFloat(value.x);

        WriteFloat(value.y);

        WriteFloat(value.z);
    }

    /// <summary>
    /// 写入Vector2
    /// </summary>
    public void WriteVector2(Vector2 value)
    {
        WriteFloat(value.x);

        WriteFloat(value.y);
    }

    /// <summary>
    /// 写入整型列表
    /// </summary>
    public void WriteIntList(List<int> list)
    {
        if (list == null)

        {
            WriteInt(0);

            return;
        }

        WriteInt(list.Count);

        foreach (int item in list)
        {
            WriteInt(item);
        }
    }

    /// <summary>
    /// 写入字符串列表
    /// </summary>
    public void WriteStringList(List<string> list)
    {
        if (list == null)

        {
            WriteInt(0);

            return;
        }

        WriteInt(list.Count);

        foreach (string item in list)
        {
            WriteString(item);
        }
    }

    /// <summary>
    /// 写入可序列化对象
    /// </summary>
    public void WriteSerializable(ISerializable obj)
    {
        if (obj == null)

        {
            WriteBoolean(false);

            return;
        }

        WriteBoolean(true);

        obj.Serialize(this);
    }

    /// <summary>
    /// 写入枚举类型
    /// </summary>
    public void WriteEnum<T>(T value) where T : Enum
    {
        WriteInt(Convert.ToInt32(value));
    }

    /// <summary>
    /// 写入枚举列表
    /// </summary>
    public void WriteEnumList<T>(List<T> list) where T : Enum
    {
        if (list == null)
        {
            WriteInt(0);
            return;
        }

        WriteInt(list.Count);
        foreach (T item in list)
        {
            WriteEnum(item);
        }
    }

    #endregion

    #region 读取方法

    /// <summary>
    /// 读取布尔值
    /// </summary>
    public bool ReadBoolean()
    {
        if (readPosition >= buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        return buffer[readPosition++] != 0;
    }

    /// <summary>
    /// 读取字节
    /// </summary>
    public byte ReadByte()
    {
        if (readPosition >= buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        return buffer[readPosition++];
    }

    /// <summary>
    /// 读取字节数组
    /// </summary>
    public byte[] ReadBytes()
    {
        int length = ReadInt();
        if (length == 0)
            return null;

        byte[] data = new byte[length];

        for (int i = 0; i < length; i++)
        {
            data[i] = ReadByte();
        }

        return data;
    }

    /// <summary>
    /// 读取短整型(short)
    /// </summary>
    public short ReadShort()
    {
        if (readPosition + 2 > buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        short value = BitConverter.ToInt16(buffer.ToArray(), readPosition);

        readPosition += 2;

        return value;
    }

    /// <summary>
    /// 读取整型(int)
    /// </summary>
    public int ReadInt()
    {
        if (readPosition + 4 > buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        int value = BitConverter.ToInt32(buffer.ToArray(), readPosition);
        readPosition += 4;

        return value;
    }

    /// <summary>
    /// 读取长整型(long)
    /// </summary>
    public long ReadLong()
    {
        if (readPosition + 8 > buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        long value = BitConverter.ToInt64(buffer.ToArray(), readPosition);

        readPosition += 8;

        return value;
    }

    /// <summary>
    /// 读取浮点型(float)
    /// </summary>
    public float ReadFloat()
    {
        if (readPosition + 4 > buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        float value = BitConverter.ToSingle(buffer.ToArray(), readPosition);
        readPosition += 4;

        return value;
    }

    /// <summary>
    /// 读取双精度浮点型(double)
    /// </summary>
    public double ReadDouble()
    {
        if (readPosition + 8 > buffer.Count)
            throw new IndexOutOfRangeException("读取超出缓冲区范围");

        double value = BitConverter.ToDouble(buffer.ToArray(), readPosition);
        readPosition += 8;

        return value;
    }

    /// <summary>
    /// 读取字符串
    /// </summary>
    public string ReadString()
    {
        int length = ReadInt();
        if (length == 0)
            return null;

        string value = System.Text.Encoding.UTF8.GetString(buffer.ToArray(), readPosition, length);
        readPosition += length;

        return value;
    }

    /// <summary>
    /// 读取Vector3
    /// </summary>
    public Vector3 ReadVector3()
    {
        return new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
    }

    /// <summary>
    /// 读取Vector2
    /// </summary>
    public Vector2 ReadVector2()
    {
        return new Vector2(ReadFloat(), ReadFloat());
    }

    /// <summary>
    /// 读取整型列表
    /// </summary>
    public List<int> ReadIntList()
    {
        int count = ReadInt();
        if (count == 0)
            return null;

        List<int> list = new List<int>();
        for (int i = 0; i < count; i++)
        {
            list.Add(ReadInt());
        }

        return list;
    }

    /// <summary>
    /// 读取字符串列表
    /// </summary>
    public List<string> ReadStringList()
    {
        int count = ReadInt();
        if (count == 0)
            return null;

        List<string> list = new List<string>();
        for (int i = 0; i < count; i++)
        {
            list.Add(ReadString());
        }

        return list;
    }

    /// <summary>
    /// 读取可序列化对象
    /// </summary>
    public T ReadSerializable<T>() where T : ISerializable, new()
    {
        bool isNotNull = ReadBoolean();
        if (!isNotNull)
            return default(T);

        T obj = new T();

        obj.Deserialize(this);

        return obj;
    }

    /// <summary>
    /// 读取枚举类型
    /// </summary>
    public T ReadEnum<T>() where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), ReadInt());
    }

    /// <summary>
    /// 读取枚举列表
    /// </summary>
    public List<T> ReadEnumList<T>() where T : Enum
    {
        int count = ReadInt();
        if (count == 0)
            return null;

        List<T> list = new List<T>();
        for (int i = 0; i < count; i++)
        {
            list.Add(ReadEnum<T>());
        }

        return list;
    }

    #endregion

    #region 工具方法

    /// <summary>
    /// 获取数据字节数组
    /// </summary>
    public byte[] GetData()
    {
        return buffer.ToArray();
    }

    /// <summary>
    /// 获取数据长度
    /// </summary>
    public int GetLength()
    {
        return buffer.Count;
    }

    /// <summary>
    /// 重置读取位置
    /// </summary>
    public void ResetReadPosition()
    {
        readPosition = 0;
    }

    /// <summary>
    /// 清空数据包
    /// </summary>
    public void Clear()
    {
        buffer.Clear();

        readPosition = 0;
    }

    /// <summary>
    /// 获取剩余未读取的字节数
    /// </summary>
    public int GetRemainingBytes()
    {
        return buffer.Count - readPosition;
    }

    #endregion
}

/// <summary>
/// 可序列化接口 - 定义对象序列化和反序列化的标准方法
/// </summary>
public interface ISerializable

{
    /// <summary>
    /// 序列化对象到数据包
    /// </summary>
    void Serialize(DataPacket packet);
    /// <summary>
    /// 从数据包反序列化对象
    /// </summary>
    void Deserialize(DataPacket packet);
}

