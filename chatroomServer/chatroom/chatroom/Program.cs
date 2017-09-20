using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace chatroom
{
    class Program
    {
        static List<Client> clientList = new List<Client>();

        /// <summary>

        /// 广播消息

        /// </summary>

        /// <param name="message"></param>

        public static void BroadcastMessage(string message, string name)

        {

            var notConnectedList = new List<Client>();

            foreach (var client in clientList)

            {

                if (client.Connected)

                    client.SendMessage(name + " : " + message);

                else

                {

                    notConnectedList.Add(client);

                }

            }

            foreach (var temp in notConnectedList)

            {

                clientList.Remove(temp);

            }

        }

        static void Main(string[] args)
        {

            int i = 0;

            string name = null;

            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//create tcpserver

            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));//set ip and port

            tcpServer.Listen(100);//listen port

            Console.WriteLine("server running...");

            while (true)

            {

                i = i + 1;

                if (i == 1) { name = "Tom"; }

                else if (i == 2) { name = "Alice"; }

                else { name = "Other"; }

                Socket clientSocket = tcpServer.Accept();//accept client request

                Console.WriteLine("用户 " + name + " 已连接 !");

                Client client = new Client(clientSocket, name);//把与每个客户端通信的逻辑(收发消息)放到client类里面进行处理

                clientList.Add(client);

            }

        }

    }
}
