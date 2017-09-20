using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System;

public class ChatMannger : MonoBehaviour {
    public string ipaddress = "127.0.0.1";
    public int port = 7788;
<<<<<<< HEAD

    public InputField textInput;
    public Text chatLabel;

    private Socket clientSocket;

    private Thread t;

    private byte[] data = new byte[1024];//数据容器

    private string message = "";//消息容器

    // Use this for initialization

    void Start()
    {

        ConnectToServer();

    }

    // Update is called once per frame

    void Update()
    {

        if (message != null && message != "")

        {

            chatLabel.text += "\n" + message;

            message = "";//清空消息

        }

    }

    void ConnectToServer()

    {

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //跟服务器端建立连接

        clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));

        //创建一个新的线程 用来接收消息

        t = new Thread(ReceiveMessage);

        t.Start();

    }

    /// <summary>

    /// 这个线程方法 用来循环接收消息

    /// </summary>

    void ReceiveMessage()

    {

        while (true)

        {

            if (clientSocket.Connected == false)

                break;

            int length = clientSocket.Receive(data);

            message = Encoding.UTF8.GetString(data, 0, length);

            //chatLabel.text += "\n" + message;

        }

    }

    void SendMessageTo(string message)

    {

        byte[] data = Encoding.UTF8.GetBytes(message);

        clientSocket.Send(data);

    }

    public void OnSendButtonClick()

    {

        string value = textInput.textComponent.text;

        SendMessageTo(value);

        textInput.textComponent.text = "";

    }

    void OnDestroy()

    {

        clientSocket.Shutdown(SocketShutdown.Both);

        clientSocket.Close();//关闭连接

=======
    public InputField textInput;
    public Text chatLable;

    private Socket clientSocket;
    private Thread t;
    private byte[] data = new byte[1024];
    private string message = "";
	// Use this for initialization
	void Start () {
        ConnectToServer();

    }
	
	// Update is called once per frame
	void Update () {
        if (message != null && message != "")
        {
            chatLable.text += "\n" + message;
            //清空消息
            message = "";
        }
	}
    void ConnectToServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        //连接服务器
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipaddress),port));
        //创建一个新的线程  用来接收消息
        t = new Thread(ReceiveMessage);
        t.Start();
    }

    /// <summary>
    /// 用来循环接受消息
    /// </summary>
    private void ReceiveMessage()
    {
        while (true)
        {
            if (clientSocket.Connected == false)
            {
                break;
            }
            int length = clientSocket.Receive(data);
            message = Encoding.UTF8.GetString(data,0,length);

        }
    }
    void SendMessage()
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(data);
    }
    public void OnSendButtonClick()
    {
        string value = textInput.textComponent.text;
        SendMessage("asdasdasd");
        textInput.textComponent.text = "";
    }
    private void OnDestroy()
    {
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();//关闭连接
>>>>>>> 4e15debfe65b9f34c424851741a9b295acb4a4af
    }
}
