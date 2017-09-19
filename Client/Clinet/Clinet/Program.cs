using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Clinet
{
    class Program
    {
        private static byte[] result = new byte[1024];
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("112.124.110.164");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip,5252));
                Console.WriteLine("succeed!");
            }
            catch
            {
                Console.WriteLine("Fail");
                return;
            }
            int reveceLength = clientSocket.Receive(result);
            Console.WriteLine("Message:{0}",Encoding.ASCII.GetString(result,0,reveceLength));
            for (int i=0;i<10;i++)
            {
                try
                {
                    Thread.Sleep(1000);
                    string sendMassage = "Client send massage Help" + DateTime.Now;
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMassage));
                    Console.WriteLine("send message to server {0}" + sendMassage);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
            Console.WriteLine("send complete,enter quit");
            Console.ReadLine();
        }
    }
}
