using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        //用来存储从客户端发过来的信息
        private static byte[] result = new byte[1024];

        //端口
        private static int myPort = 5252;

        //socket实例
        static Socket serverSocket;
        static void Main(string[] args)
        {
            //这个函数会把ip地址转换成一个IPAddress类型的个体
            IPAddress ip = IPAddress.Parse("127.0.0.1");


            serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            //IPEndPoint吧网络地址表示为IP地址和端口号
            serverSocket.Bind(new IPEndPoint(ip,myPort));

            //Listen(backlog)函数是将Socket置于监听状态，backlog表示最大连接数
            serverSocket.Listen(10);

            Console.WriteLine("启动监听成功！IP地址是：" + serverSocket.LocalEndPoint.ToString());

            Thread myThread = new Thread(ListenClientConnect);

            myThread.Start();

            Console.ReadKey();
        }

        private static void ListenClientConnect()
        {
            while (true)
            {
            /*Accept 以同步方式从侦听套接字的连接请求队列中提取第一个挂起的连接请求，
             * 然后创建并返回一个新 Socket。 不能使用此返回 Socket 以接受来自连接队列的任何其他连接。
             * 但是，你可以调用 RemoteEndPoint 方法所返回的 Socket 来标识远程主机的网络地址和端口号。
                */
                Socket clientSocket = serverSocket.Accept();
                //这是服务器向客户端发送信息
                clientSocket.Send(Encoding.Unicode.GetBytes("会变成乱码吗？"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }

        private static void ReceiveMessage(object clientSocket)
        {
            //object显示转化成Socket类型
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //接受客户端发过来的消息，并返回一个长度？
                    int receiveNumber = myClientSocket.Receive(result);
                    //{0}:RemoteEndPoint()是返回socket的IP地址       {1}:代表的是客户端中发送给服务端的地址
                    Console.WriteLine("接受客户端{0} , 客户端名称{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.Unicode.GetString(result, 0, receiveNumber));


                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
    }
}
