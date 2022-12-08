using System.Net.Sockets;

namespace ServerAlpha.Command.CommandList
{
    internal class CommandServe
    {
        public static Thread? serverThread;

        /// <summary>
        /// Start server, open udp socket.
        /// </summary>
        /// <param name="args">Array with parameters. {--udp-port / --tcp-port}</param>
        /// <returns></returns>
        public static bool commandServe(string[]? args)
        {
            bool commandCompletedCorrectly = true;
            UdpClient? udpClient = null;

            try
            {
                string port;
                if(ParamHandler.getParamValue(args!, "--port", out port))
                    udpClient = Server.Udp.UdpSrv.initSocket(Convert.ToInt32(port));
                else
                    udpClient = Server.Udp.UdpSrv.initSocket();
            }
            catch
            {
                Console.WriteLine("Error during socket init. -- COD-19");

                commandCompletedCorrectly = false;
            }

            if(commandCompletedCorrectly)
            {
                Console.WriteLine("Server IP (Use the correct one): ");
                foreach (var address in Server.Udp.UdpSrv.AddressList())
                {
                    Console.Write(address + "   ");
                }
                Console.WriteLine("\n\n------> Server is ready to listen");

                serverThread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    Server.Udp.UdpSrv.serverStart(udpClient!);
                });

                serverThread.Start();
            }

            return commandCompletedCorrectly;
        }
    }
}
