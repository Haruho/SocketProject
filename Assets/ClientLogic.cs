using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;
using UnityEngine.UI;
public class ClientLogic : MonoBehaviour {
    private static byte[] result = new byte[1024];

    private string clientName;   //客户端名称

    public InputField chatContent;

    Socket clientSocket;
    // Use this for initialization
    void Start () {
        clientName = "中国";
        //链接服务器
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, 5252));
            print("已连接服务器!");
        }
        catch
        {
            print("连接服务器失败");
            return;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendMessageToServer()
    {
        int reveceLength = clientSocket.Receive(result);
        //这个信息是从服务器上发送过来的  
        //这里解码和编码的方式具体查看云笔记里收藏的文章 C#学习 中   中文不支持ASCII编码  这种编码只能支持127个字符 不包括中文，  如果相对中文进行编码，使用Unicode或者GKB，解码要用相同的方式进行解码
        print("消息:" + Encoding.Unicode.GetString(result, 0, reveceLength));
        try
        {
            string sendMessage = chatContent.textComponent.text + "     " +DateTime.Now;
            //把上面要发送的消息转换成bytes，并发送
            clientSocket.Send(Encoding.Unicode.GetBytes(sendMessage));
           // print("发送的消息是" + sendMessage);
        }
        catch
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        print("消息发送完毕");
    }
}
