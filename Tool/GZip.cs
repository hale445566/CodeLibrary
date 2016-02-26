using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;

/// <summary>
/// 压缩数据
/// </summary>
public class GZip
{
    /// <summary>
    /// 将字节数组进行压缩后返回压缩的字节数组
    /// </summary>
    /// <param name="data">需要压缩的数组</param>
    /// <returns>压缩后的数组</returns>
    public static byte[] Compress(byte[] data)
    {
        MemoryStream stream = new MemoryStream();
        GZipOutputStream gzip = new GZipOutputStream(stream);
        gzip.Write(data, 0, data.Length);
        gzip.Close();
        return stream.ToArray();
    }

    /// <summary>
    /// 解压字符数组
    /// </summary>
    /// <param name="data">压缩的数组</param>
    /// <returns>解压后的数组</returns>
    public static byte[] Decompress(byte[] data)
    {
        MemoryStream stream = new MemoryStream();
        GZipInputStream gzi = new GZipInputStream(new MemoryStream(data));

        byte[] bytes = new byte[40960];
        int n=0;
        while ((n = gzi.Read(bytes, 0, bytes.Length)) != 0)
        {
            stream.Write(bytes, 0, n);
        }
        gzi.Close();
        return stream.ToArray();
    }
}
