using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace ChatRoomServer
{
    /// <summary>
    /// 用来和客户端进行通信
    /// </summary>
    class Client
    {
        public string name = null;
        private Socket clientSocket;
        private Thread t;
        private byte[] data = new byte[1024];
        //用户类的构造函数
        public Client(Socket s , string i)
        {
            clientSocket = s;
            name = i;
            //启动一个线程  处理客户端的数据接受
            t = new Thread(ReceiveMessage);
            t.Start();
        }

        private void ReceiveMessage()
        {
            //收到消息的时候就会执行一次
            while (true)
            {
                //在接受数据之前 判断一下socket是不是断开的
            /*
             * Socket.Poll(
             * int microSeconds
             * SelectMode mode
             * )
             * Poll方法会检查Socket的状态  并且返回Bool
                 */
                if (clientSocket.Poll(10,SelectMode.SelectRead))
                {
                    clientSocket.Close();
                    break;
                }
                int length = clientSocket.Receive(data);
                string message = Encoding.UTF8.GetString(data,0,length);
                //在接受到数据的时候   要把这个数据分发到客户端上
                //广播这个消息
                Program.BroadcastMessage(message , name);

                Console.WriteLine("收到 " + name + "的消息： " + message);
            }
        }
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
        }
        public bool Connected()
        {
            return clientSocket.Connected;
        }
    }
}
