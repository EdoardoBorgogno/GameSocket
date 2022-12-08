namespace ServerAlpha.Server.ServerCommand.CommandList
{
    internal class Ready
    {
        /// <summary>
        /// Handle player ready command.
        /// </summary>
        /// <param name="message">Message to handle.</param>
        /// <param name="senderAddress">Address of sender.</param>
        /// <param name="senderPort">Port of sender.</param>
        /// <param name="games">List of all games on server.</param>
        public static void ready(string message, string senderAddress, int senderPort, List<Game> games)
        {
            bool allPlayersReady = true;

            foreach (Game item in games)
            {
                foreach (Player player in item.PlayerList)
                {
                    if(senderAddress == player.EndPoint.Address.ToString())
                    {
                        player.Ready = true;
                    }

                    if(!player.Ready)
                    {
                        allPlayersReady = false;
                    }
                }
            }

            if(allPlayersReady)
            {
                foreach (Game item in games)
                {
                    foreach (Player player in item.PlayerList)
                    {
                        Udp.UdpSrv.sendMessage("</OPENGAME/>", player.EndPoint.Address.ToString(), player.EndPoint.Port);
                    }
                }
            }
        }
    }
}