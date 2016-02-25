using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Text;

public class ClientController : MonoBehaviour {
    private ClientApp m_client = new ClientApp();

    //ip
    public InputField ip_Text;
    //昵称
    public InputField name_Text;
    //警告文本
    private Text warringText;
    //发送文本
    private InputField sendmessageText;
    //消息文本
    private Text messageText;
    //在线用户文本
    private Text clientText;

    void Start()
    {
        warringText = GameObject.Find("Warring_Text").GetComponent<Text>();
        sendmessageText = GameObject.Find("Message_InputField").GetComponent<InputField>();
        messageText = GameObject.Find("Message_Text").GetComponent<Text>();
        clientText = GameObject.Find("ClientList_Text").GetComponent<Text>();

    }

    /// <summary>
    /// 连接按钮
    /// </summary>
    public void ConnectSever()
    {
        //ip不符合格式
        if (!Regex.IsMatch(ip_Text.text, @"\b(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\b"))
        {
            warringText.text = "";
            warringText.text = "ip格式不对";
            return;
        }

        string m_name = name_Text.text;

        //没有昵称
        if (m_name == "")
        {
            warringText.text = "";
            warringText.text = "输入昵称";
            return;
        }

        if (m_name.Contains("#"))
        {
            warringText.text = "";
            warringText.text = "昵称中不能有#";
            return;
        }

        //连接服务器
        try
        {
            m_client.Init(ip_Text.text, m_name, ShowMessage);
        }
        catch
        {
            warringText.text = "";
            warringText.text = "连接不到服务器";
            return;
        }
    }

    /// <summary>
    /// 发送按钮
    /// </summary>
    public void SendMessage()
    {
        if (sendmessageText.text != "")
        {
            string s = sendmessageText.text;
            //客户端显示名字+信息
            string severS = this.name_Text.text + ":" + s;

            //发送给服务端
            m_client.SendMessage(severS, MessageStatus.Message);
            //本地显示变色的用户名+信息
            string clientS = s.Insert(0, "<color=blue>" + this.name_Text.text + "</color>:");
            //显示在本地
            messageText.text += clientS + "\n";
        }
    }

   

    /// <summary>
    /// 回调函数
    /// </summary>
    /// <param name="s"></param>
    private void ShowMessage(string s)
    {
        string[] sArray = s.Split('#');
        Debug.Log("ReciveMessage:"+s);
        switch (sArray[0])
        {
            case "n":
                clientText.text += sArray[1] + "\n"; 
                break;

            case "m":
                messageText.text += sArray[1] + "\n";
                break;

            case "w":
                warringText.text = "";
                warringText.text = sArray[1];
                break;

            default: throw new System.Exception("非法包头");

        }
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Close()
    {
        this.m_client.Close(name_Text.text);
    }

    void OnDestroy()
    {
        this.Close();
    }
}
