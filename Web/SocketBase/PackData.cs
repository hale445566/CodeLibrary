using UnityEngine;
using System.Collections;
using System.Text;
public enum MessageStatus
{
    Name,
    Message,
    Warring,
    Transform,
    TransformColor,
    Close,
}
/// <summary>
/// 打包数据
/// </summary>
public class PackData  {
    //包尾字符
    public string packageEndStr = "/*/";
    //包尾长度
    public int packageEndLength ;

    private static PackData instance;

    public static PackData Instance()
    {
        if (instance == null)
            instance = new PackData();

        return instance;
    }


    private PackData() {
        packageEndLength = packageEndStr.Length;
    }



    /// <summary>
    /// 根据数据类型打包数据
    /// 包括包头和包尾
    /// </summary>
    /// <param name="s">传入数据</param>
    /// <param name="ms">类型</param>
    /// <returns></returns>
    public string PackStringDataAll(string s, MessageStatus ms)
    {
        string temp;
        switch(ms){
            case MessageStatus.Name:
            temp= "n#" + s;
            break;
            case MessageStatus.Message:
             temp="m#" + s;
             break;
            case MessageStatus.Warring:
            temp= "w#" + s;
            break;
            case MessageStatus.Transform:
            temp = "t#" + s;
            break;
            case MessageStatus.TransformColor:
            temp = "tc#" + s;
            break;
            default: return "";
        }

        return temp + packageEndStr;
    }

    //只有包头
    public string PackStringDataHead(string s, MessageStatus ms)
    {
        switch (ms)
        {
            case MessageStatus.Name:
                return  "n#" + s;
            case MessageStatus.Message:
                return  "m#" + s;
            case MessageStatus.Warring:
                return  "w#" + s;
            case MessageStatus.Transform:
                return "t#" + s;
            case MessageStatus.TransformColor:
                return  "tc#" + s;
            default: return "";
        }
    }

    //只有包尾
    public string PackStringDataEnd(string s)
    {
        return s + packageEndStr;
    }

    /// <summary>
    /// 数据包转字符串
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public string ByteToString(byte[] b,int length)
    {
        return UTF8Encoding.UTF8.GetString(b,0,length);
    }

    /// <summary>
    /// 字符串转数据包
    /// </summary>
    /// <param name="s"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public bool StringToByte(string s, MessageStatus m,out byte[] b)
    {
        b = new byte[1024];
        //如果消息为空 返回false
        if (s == "")
            return false;

        //字符串转数据包
        b=UTF8Encoding.UTF8.GetBytes(this.PackStringDataAll(s, m));
     
        return true;
    }


    public bool StringToByte(string s, out byte[] b)
    {
        b = new byte[1024];
        //如果消息为空 返回false
        if (s == "")
            return false;

        //字符串转数据包
        b = UTF8Encoding.UTF8.GetBytes(s);

        return true;
    }
}
