using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;

public delegate void clientCallBack(string s);

public class ClientApp {

    private Socket m_Socket;
    private clientCallBack callBack;
    private byte[] sendDataPackage=new byte[1024];
    private byte[] reciveDataPackage = new byte[1024];

    private StringBuilder sb=new StringBuilder();
   
    /// <summary>
    /// 建立连接
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="name"></param>
    /// <param name="cb"></param>
    public void Init(string ip, string name, clientCallBack cb)
    {
        this.callBack = cb;
        //创建
        m_Socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        IPEndPoint m_IPEndPoint = new IPEndPoint(IPAddress.Parse(ip),23555);
        //连接
        m_Socket.Connect(m_IPEndPoint);

        m_Socket.BeginReceive(reciveDataPackage, 0, reciveDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Recive), m_Socket);

        callBack(PackData.Instance().PackStringDataHead( "连接成功",MessageStatus.Warring));
        callBack(PackData.Instance().PackStringDataHead( name,MessageStatus.Name));
        SendMessage(name,MessageStatus.Name);
    }

    /// <summary>
    /// 接收数据
    /// </summary>
    /// <param name="ar"></param>
    private void M_Recive(System.IAsyncResult ar)
    {
        Debug.Log("Recive");

        Socket SeverSocket = ar.AsyncState as Socket;

        if (!SeverSocket.Connected)
        {
            Debug.Log("掉线了");
            return;
        }
        int count=0;
        count = SeverSocket.EndReceive(ar);

        string content="";
        //数据包转字符串
        if (count > 0)
        {
            content = PackData.Instance().ByteToString(reciveDataPackage, count);
            Debug.Log("ClientContent:"+content);
        }

        //处理粘包
        for (int i = 0; i < content.Length; )
        {
            //包长度内
            if (i <= content.Length -PackData.Instance().packageEndLength)
            {
                //如果字符串不是包尾 添加到暂存字符串中
                if (content.Substring(i, PackData.Instance().packageEndLength) != "/*/")
                {
                    sb.Append(content[i]);
                    i++;
                }
                //字符串是包尾 调用回调函数 清空暂存字符串 同时数组指针指向包尾后字符串
                else
                {
                    callBack(sb.ToString());
                    sb = new StringBuilder();
                    i += PackData.Instance().packageEndLength;
                }
            }
            //包长度外 即出现分包情况 储存当前包的后半部 和下个包结合
            else
            {
                sb.Append(content[i]);
                i++;
            }
        }

        SeverSocket.BeginReceive(reciveDataPackage, 0, reciveDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Recive), SeverSocket);
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="s"></param>
    public void SendMessage(string s, MessageStatus m)
    {
        if (!m_Socket.Connected)
        {
            Debug.Log("掉线了");
            return;
        }

        if (PackData.Instance().StringToByte(s, m, out sendDataPackage))
            m_Socket.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send), m_Socket);
     
    }

    private void M_Send(System.IAsyncResult ar)
    {
        m_Socket = ar.AsyncState as Socket;
        m_Socket.EndSend(ar);
        //for (int i = 0; i < sendDataPackage.Length; i++)
        //{
        //Debug.Log("Pac::"+sendDataPackage[i]);
            
        //} 
    }

    public void Close(string name)
    {
        SendMessage(name,MessageStatus.Close);

        m_Socket.Disconnect(false);

        if(this.m_Socket!=null)
            this.m_Socket.Close();
    }
}
