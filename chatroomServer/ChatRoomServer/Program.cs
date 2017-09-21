using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
namespace ChatRoomServer
{
    class Program
    {
        static List<Client> clientList = new List<Client>();

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public static void BroadcastMessage(string message, string name)
        {
            var notConnectedList = new List<Client>();
            foreach (var client in clientList)
            {
                if (client.Connected())
                {
                    client.SendMessage(name + " : " + message);
                }
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
            Socket tcpServer = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"),7788));
            //开启服务器监听
            tcpServer.Listen(100);
            Console.WriteLine("Server Running...");

            //每当有一个用户连接的时候会执行一次
            while (true)
            {
                i = i + 1;
                if(i == 1)
                {
                    name = "Tom";
                }else if(i == 2)
                {
                    name = "Alice";
                }
                else
                {
                    name = "Other";
                }
                Socket clientSocket = tcpServer.Accept();
                Console.WriteLine("用户 " + name + " 已连接！");
                Client client = new Client(clientSocket,name); //把每个客户端通信的逻辑（收发消息）放到client类里面进行处理
                clientList.Add(client);
            }
        }
    }
}
