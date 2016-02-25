using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public enum RecordCommond_Type
{
    Position = 0,
    Rotation = 1,
}

/// <summary>
/// 记录玩家操作 回放  
/// by:何天宇
/// 2016.2.25
/// </summary>
public class Record  {

    #region 单例
    private static Record instance;

    public static Record Instance()
    {
        if (instance==null)
            instance = new Record();

        return instance;
    }

    private Record() { }
    #endregion

    private Queue<RecordTransform> record = new Queue<RecordTransform>();
    private int structLength=18;

    /// <summary>
    /// 记录当前帧
    /// </summary>
    /// <param name="type">指令类型</param>
    /// <param name="value">值</param>
    public void RecordCurrentFrame(RecordCommond_Type type, Vector3 value)
    {
        record.Enqueue(new RecordTransform(Time.time, type, value));
    }

    /// <summary>
    /// 保存记录的数据 一共18个字节 前4个字节时间 中间2个字节是命令的代号 最后12个字节是xyz的值
    /// </summary>
    public void SaveRecord(string fileName)
    {
        byte[] bytes = new byte[record.Count * structLength];
        int len = record.Count;
        for (int i = 0; i < len; i++)
        {
            record.Dequeue().GetBytes().CopyTo(bytes, i * structLength);
        }

        File.WriteAllBytes(Application.streamingAssetsPath + fileName, bytes);
    }
}

/// <summary>
/// 记录的结构体 
/// </summary>
public struct RecordTransform
{
    public float time;
    public short type;
    public float x;
    public float y;
    public float z;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="type">指令类型 旋转 移动</param>
    /// <param name="value">指令对应值</param>
    public RecordTransform(float time, RecordCommond_Type type, Vector3 value)
    {
        this.time = time;
        this.type = (short)type;
        this.x = value.x;
        this.y = value.y;
        this.z = value.z;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="type">指令类型 旋转 移动</param>
    /// <param name="x">值x</param>
    /// <param name="y">值y</param>
    /// <param name="z">值z</param>
    public RecordTransform(float time, short type, float x, float y, float z)
    {
        this.time = time;
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// 获得指令
    /// </summary>
    /// <returns>指令</returns>
    public RecordCommond_Type GetRecordCommondType()
    {
        return (RecordCommond_Type)type;
    }

    /// <summary>
    /// 获得值
    /// </summary>
    /// <returns></returns>
    public Vector3 GetValue()
    {
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// 获得转成byte数组的属性
    /// </summary>
    /// <returns></returns>
    public byte[] GetBytes()
    {
        byte[] bytes = new byte[18];
        BitConverter.GetBytes(time).CopyTo(bytes, 0);
        BitConverter.GetBytes(type).CopyTo(bytes, 4);
        BitConverter.GetBytes(x).CopyTo(bytes, 6);
        BitConverter.GetBytes(y).CopyTo(bytes, 10);
        BitConverter.GetBytes(z).CopyTo(bytes, 14);
        return bytes;
    }
}
