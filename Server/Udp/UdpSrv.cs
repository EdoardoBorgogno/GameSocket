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
        private static UdpClient serverSender = new UdpClient();
        
        /// <summary>
        /// Send message to client.
        /// </summary>
        /// <param name="message">Message to send.</param>
        /// <param name="address">Address of client.</param>
        /// <param name="port">Port of client.</param>
        public static void sendMessage(string message, string address, int port)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(message);

            try
            {
                serverSender.Send(sendBytes, sendBytes.Length, address, port);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Create new socket to listen.
        /// </summary>
        /// <param name="port">Port to listen.</param>
        /// <returns>Socket to listen.</returns>
        public static UdpClient initSocket(int port = 11000)
        {
            return new UdpClient(new IPEndPoint(IPAddress.Any, port));
        }

        /// <summary>
        /// Get all IP addresses of this computer.
        /// </summary>
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
    
        /// <summary>
        /// Start listening.
        /// </summary>
        /// <param name="socket">Socket to listen.</param>
        public static void serverStart(UdpClient socket)
        {
            addServerToServerList("/Esercizi/GameSocket/addServerPHP.php");

            byte[] data = new byte[1024];
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                data = socket.Receive(ref sender);
                string receivedData = Encoding.ASCII.GetString(data, 0, data.Length);

                string command = getCommand(receivedData);
                string message = getMessage(receivedData);

                ServerCommand.ServerCommandHandler.serverCommandHandle(command, message, sender.Address.ToString(), sender.Port);
            }
        }
        /// <summary>
        /// Get message from string.
        /// </summary>
        private static string getMessage(string data)
        {
            return data.Substring(data.LastIndexOf(">") + 1);
        }
        /// <summary>
        /// Get command from string.
        /// </summary>
        private static string getCommand(string str) 
        {
             return str.Substring(str.IndexOf("</") + 2, str.IndexOf("/>") - 2).ToUpper(); 
        }
        
        /// <summary>
        /// Send server IPs to server list API.
        /// </summary>
        public static async void addServerToServerList(string apiPath)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://paolobruno1280.altervista.org");

                IEnumerable<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();

                foreach (var ip in AddressList())
                {
                    postParams = postParams.Concat(new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("serverIP[]", ip)
                    });
                }

                var content = new FormUrlEncodedContent(postParams);

                var result = await client.PostAsync(apiPath, content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }
    }
}
