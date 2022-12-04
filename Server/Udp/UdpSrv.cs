using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerAlpha.Server.Udp
{
    internal class UdpSrv
    {
        public static UdpClient initSocket(int port = 11000)
        {
            return new UdpClient(new IPEndPoint(IPAddress.Any, port));
        }
    
        public static List<string> AddressList()
        {
            List<string> list = new List<string>();

            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    list.Add(ip.ToString());
                }
            }

            return list;
        }
    
        public static void serverStart(UdpClient socket)
        {
            byte[] data = new byte[1024];
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                data = socket.Receive(ref sender);

                string receivedData = Encoding.ASCII.GetString(data, 0, data.Length);

                string command = receivedData.Substring(receivedData.IndexOf("</") + 2, receivedData.IndexOf("/>") - 2).ToUpper();
                Console.WriteLine(command);
            }
        }
    }
}
