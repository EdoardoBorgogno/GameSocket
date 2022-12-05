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
                string receivedData = Encoding.ASCII.GetString(socket.Receive(ref sender), 0, data.Length);

                string command = getCommand(receivedData);
                string message = getMessage(receivedData);

                ServerCommand.ServerCommandHandler.serverCommandHandle(command, message, sender.Address.ToString());
            }
        }

        private static string getMessage(string data)
        {
            return data.Substring(data.LastIndexOf(">") + 1);
        }

        private static string getCommand(string str) 
        {
             return str.Substring(str.IndexOf("</") + 2, str.IndexOf("/>") - 2).ToUpper(); 
        }
    }
}
