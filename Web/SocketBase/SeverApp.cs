using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
public delegate void SeverCallBack(string s,Socket socket);

public class SeverApp {
    //服务端socket
    private Socket m_socket;
    //数据包
    private byte[] sendDataPackage=new byte[1024];
    private byte[] reciveDataPackage = new byte[1024];

    //回调函数
    private SeverCallBack severCallBack;
    //客户端集合
    //private List<UserClient> clientList = new List<UserClient>(10);
    private List<UserClient> clientList = new List<UserClient>(10);

    //数据暂存
    private StringBuilder sb = new StringBuilder();

    /// <summary>
    /// 打开服务端
    /// </summary>
    /// <param name="sc"></param>
    public void Init(SeverCallBack sc)
    {
        this.severCallBack = sc;
        //创建
        m_socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        
        IPEndPoint m_EndPoint=new IPEndPoint(IPAddress.Any,23555);
        //绑定
        m_socket.Bind(m_EndPoint);

        //监听
        m_socket.Listen(10);

        //接受
        m_socket.BeginAccept(new System.AsyncCallback(M_Accept), m_socket);

        severCallBack(PackData.Instance().PackStringDataHead("服务器已打开", MessageStatus.Message),m_socket);
    }

    //接受
    private void M_Accept(System.IAsyncResult ar)
    {

        //服务器接收的客户端
        m_socket = ar.AsyncState as Socket;

        //客户端赋值
        Socket clientSocket = m_socket.EndAccept(ar);

        //开始接收数据
        clientSocket.BeginReceive(reciveDataPackage, 0, reciveDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Recive), clientSocket);

        //华蓥
        if (clientSocket.Connected)
        {
            //欢迎客户端
            SendMessage("欢迎欢迎，热烈欢迎", MessageStatus.Message,clientSocket);
        }
        
    

        //继续接受下一个客户端
        m_socket.BeginAccept(new System.AsyncCallback(M_Accept), m_socket);
    }

    //接收数据
    private void M_Recive(System.IAsyncResult ar)
    {
        //获得接收到的结果
        Socket workingSocket = ar.AsyncState as Socket;

        //客户端不再连接 返回
        if (!workingSocket.Connected)
        {
            Debug.Log("客户端下线");
            return;
        }

        //数据包长度
        int count = 0;
        count=workingSocket.EndReceive(ar);

        //长度为0 代表客户端关闭
        if (count == 0)
        {
            workingSocket.Shutdown(SocketShutdown.Both);
            workingSocket.Close();
            ClientClose(workingSocket);
            return;
        }
        //数据文本
        string content="";


        //获得数据文本
        if (count > 0)
        {
            content = PackData.Instance().ByteToString(reciveDataPackage, count);
        }
        Debug.Log(workingSocket.RemoteEndPoint+"M_Recive:" + content);

        //处理粘包
        for (int i = 0; i < content.Length; )
        {
            //包长度内
            if (i <= content.Length - PackData.Instance().packageEndLength)
            {
                //如果字符串不是包尾 添加到暂存字符串中
                if (content.Substring(i, PackData.Instance().packageEndLength) != PackData.Instance().packageEndStr)
                {
                    Debug.Log("singelStr::"+content[i]);
                    sb.Append(content[i]);
                    i++;
                }
                //字符串是包尾 调用回调函数 清空暂存字符串 同时数组指针指向包尾后字符串
                else
                {
                    Debug.Log("package::"+sb.ToString());
                    //显示到服务器端
                    severCallBack(sb.ToString(), workingSocket);

                

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

        //继续接收
        workingSocket.BeginReceive(reciveDataPackage, 0, reciveDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Recive), workingSocket);
    }

    /// <summary>
    /// 给所有客户端发送消息
    /// </summary>
    /// <param name="s"></param>
    public void SendMessageToAll(string s,MessageStatus m)
    {
        for (int i = 0; i < clientList.Count;i++ )
        {
            if (PackData.Instance().StringToByte(s, m, out sendDataPackage))
                clientList[i].m_Socket.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send),  clientList[i]);
        }
    }

    /// <summary>
    /// 给除了发送者以外的所有客户端发送消息
    /// </summary>
    public void SendMessageToOther(string s,MessageStatus m,Socket soc)
    {
        //发送给其他客户端
        for (int i = 0; i < this.clientList.Count; i++)
        {
            if (soc.RemoteEndPoint.ToString() != clientList[i].m_EndPoint.ToString() && clientList[i].m_Socket.Connected)
            {

                if (PackData.Instance().StringToByte(s, m, out sendDataPackage))
                    clientList[i].m_Socket.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send), clientList[i]);
                ////发送数据 因为已是打包好的 所以直接发
                //SendMessage(sb.ToString(), clientList[j].m_Socket);
                //Debug.Log("TOOTHER:" + sb.ToString());
            }

        }
    }

    /// <summary>
    /// 发送给soc其他客户端的信息
    /// </summary>
    /// <param name="s"></param>
    /// <param name="m"></param>
    /// <param name="soc"></param>
    public void OtherSendToThis( MessageStatus m, Socket soc)
    {
        for (int i = 0; i < this.clientList.Count;i++ )
        {
            if(clientList[i].m_Socket.RemoteEndPoint.ToString()!=soc.RemoteEndPoint.ToString())
            {
                switch(m){
                    case MessageStatus.Name:
                        if (PackData.Instance().StringToByte(clientList[i].m_name, m, out sendDataPackage))
                            soc.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send), soc);
                        break;

                    case MessageStatus.TransformColor:
                        if (PackData.Instance().StringToByte(clientList[i].m_Color, m, out sendDataPackage))
                            soc.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send), soc);
                        break;
                }
            }
        }
     
    }

    /// <summary>
    /// 发送已经有包头没有包尾的数据
    /// </summary>
    /// <param name="s"></param>
    /// <param name="clientSocket"></param>
    public void SendMessage(string s, Socket clientSocket)
    {
        Debug.Log(clientSocket.RemoteEndPoint+"==="+s);
        sendDataPackage = UTF8Encoding.UTF8.GetBytes(PackData.Instance().PackStringDataEnd( s));
        clientSocket.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send), clientSocket);
    }


    /// <summary>
    /// 发送没有包头和包尾的数据
    /// </summary>
    /// <param name="s"></param>
    /// <param name="m"></param>
    /// <param name="clientSocket"></param>
    public void SendMessage(string s,MessageStatus m,Socket clientSocket)
    {
        
        if (PackData.Instance().StringToByte(s, m, out sendDataPackage))
            clientSocket.BeginSend(sendDataPackage, 0, sendDataPackage.Length, SocketFlags.None, new System.AsyncCallback(M_Send), clientSocket);
    }
   

    private void M_Send(System.IAsyncResult ar)
    {
        Socket workingSocket = ar.AsyncState as Socket;
        workingSocket.EndSend(ar);
        Debug.Log("dataPackage"+UTF8Encoding.UTF8.GetString(sendDataPackage));
    }

    /// <summary>
    /// 添加socket到集合
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clientSocket"></param>
    public void AddSocketToList(string name,Socket clientSocket)
    {
        //将客户端存入集合
        clientList.Add(new UserClient(name, "",clientSocket.RemoteEndPoint, clientSocket));
    }

    /// <summary>
    /// 添加颜色到集合
    /// </summary>
    /// <param name="color"></param>
    /// <param name="remoteEndPoint"></param>
    public void AddColor(string color,string remoteEndPoint)
    {
        for (int i = 0; i < clientList.Count;i++ )
        {
            if(clientList[i].m_EndPoint.ToString()==remoteEndPoint)
            {
                clientList[i].m_Color = color;
                return;
            }
        }
    }

    /// <summary>
    /// 客户端关闭
    /// </summary>
    /// <param name="socket"></param>
    public void ClientClose(Socket socket)
    {
        for (int i = 0; i < clientList.Count;i++ )
        {
            if (clientList[i].m_EndPoint.ToString() == socket.RemoteEndPoint.ToString())
            {
                clientList[i].m_Socket.Shutdown(SocketShutdown.Both);
                clientList[i].m_Socket.Close();
                clientList.Remove(clientList[i]);
                return;
            }
        }
    }

    public void Close()
    {
        for (int i = 0; i < clientList.Count; i++)
        {
            clientList[i].m_Socket.Shutdown(SocketShutdown.Both);
            clientList[i].m_Socket.Close();
            return;
        }

        if (m_socket != null)
            m_socket.Close();

        m_socket = null;
    }
}
