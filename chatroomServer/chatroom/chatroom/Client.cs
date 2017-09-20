using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chatroom
{
    class Client
    {
        public string name = null;

        private Socket clientSocket;

        private Thread t;

        private byte[] data = new byte[1024];//这个是一个数据容器

        public Client(Socket s, string i)

        {

            clientSocket = s;

            name = i;

            //启动一个线程 处理客户端的数据接收

            t = new Thread(ReceiveMessage);

            t.Start();

        }

        private void ReceiveMessage()

        {

            //一直接收客户端的数据

            while (true)

            {

                //在接收数据之前  判断一下socket连接是否断开

                if (clientSocket.Poll(10, SelectMode.SelectRead))

                {

                    clientSocket.Close();

                    break;//跳出循环 终止线程的执行

                }

                int length = clientSocket.Receive(data);

                string message = Encoding.UTF8.GetString(data, 0, length);

                //接收到数据的时候 要把这个数据 分发到客户端

                //广播这个消息

                Program.BroadcastMessage(message, name);

                Console.WriteLine("收到 " + name + " 的消息:" + message);

            }

        }

        public void SendMessage(string message)

        {

            byte[] data = Encoding.UTF8.GetBytes(message);

            clientSocket.Send(data);

        }

        public bool Connected

        {

            get { return clientSocket.Connected; }

        }

    }
}

