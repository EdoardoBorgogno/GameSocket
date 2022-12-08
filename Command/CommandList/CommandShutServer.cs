using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAlpha.Command.CommandList
{
    internal class CommandShutServer
    {
        /// <summary>
        /// If the server is running, close it.
        /// </summary>
        /// <param name="args">No useful param for this function.</param>
        public static bool commandShutServer(string[]? args)
        {
            bool commandCompletedCorrectly = true;
            bool wasAlive = false;

            try
            {
                if (CommandServe.serverThread != null && CommandServe.serverThread.IsAlive)
                {
                    CommandServe.serverThread.Interrupt();

                    Server.Udp.UdpSrv.addServerToServerList("/Esercizi/GameSocket/removeServerPHP.php");

                    wasAlive = true;
                }
                else
                    Console.WriteLine("\nServer is not on running state. \nUse -serve to start server.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error during server shut operation.");
                commandCompletedCorrectly = false;
            }

            if(commandCompletedCorrectly && wasAlive)
                Console.WriteLine("Server shut down correctly.");

            return commandCompletedCorrectly;
        }
    }
}
