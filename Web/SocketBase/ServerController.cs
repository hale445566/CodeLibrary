using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;

public class ServerController : MonoBehaviour {
    SeverApp severApp=new SeverApp();
    public Text m_MessageText;
    public Text clientInfo;

    private string m_Messge;
    private string m_ClientInfo;

    /// <summary>
    /// 打开服务端
    /// </summary>
    public void StartSever()
    {
        severApp.Init(ShowMessage);
    }

    /// <summary>
    /// 回调函数
    /// </summary>
    /// <param name="s"></param>
    private void ShowMessage(string s,Socket socket)
    {
        string[] sArray = s.Split('#');

        switch (sArray[0])
        {
            case "n":
                clientInfo.text += sArray[1] + "\n";
                severApp.AddSocketToList(sArray[1], socket);
                severApp.SendMessageToOther(sArray[1], MessageStatus.Name, socket);
                severApp.OtherSendToThis( MessageStatus.Name, socket);
                break;

            case "m":
                m_MessageText.text += sArray[1] + "\n";
                severApp.SendMessageToOther(sArray[1], MessageStatus.Message, socket);
                break;

            case "w":
                break;

            case "c":
                clientInfo.text.Remove(clientInfo.text.IndexOf(sArray[1]), sArray.Length);
                severApp.ClientClose(socket);
                severApp.SendMessageToOther(sArray[1] + "已下线", MessageStatus.Message, socket);
                break;


            case "t":
                m_MessageText.text += sArray[1] + "\n";
                severApp.SendMessageToAll(sArray[1], MessageStatus.Transform);
                break;

            case "tc":
                m_MessageText.text += sArray[1] + "\n";
                severApp.AddColor(sArray[1],socket.RemoteEndPoint.ToString());
                severApp.SendMessageToAll(sArray[1], MessageStatus.TransformColor);
                severApp.OtherSendToThis( MessageStatus.TransformColor, socket);
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            severApp.SendMessageToAll("wwwwwwwwww", MessageStatus.Message);
    }


    /// <summary>
    /// 关闭服务器
    /// </summary>
    public void CloseSever()
    {
        m_MessageText.text += "服务器已关闭\n";
        severApp.Close();
    }

    void OnDestroy()
    {
        CloseSever();
    }
}
