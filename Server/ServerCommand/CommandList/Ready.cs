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
            bool gameFound = false;

            //in each game check if player is in game
            foreach (Game item in games)
            {
                //check if player is in game and set ready to true
                foreach (Player player in item.PlayerList)
                {
                    if (player.EndPoint.Address.ToString() == senderAddress && player.EndPoint.Port == senderPort)
                    {
                        player.Ready = true;
                        gameFound = true;
                    }
                }

                //if game is found check if all players are ready
                if (gameFound)
                {
                    //check if all players are ready
                    foreach (Player player in item.PlayerList)
                    {
                        if(senderAddress != player.EndPoint.Address.ToString())
                            Udp.UdpSrv.sendMessage("</READY/>", player.EndPoint.Address.ToString(), player.EndPoint.Port);

                        if (!player.Ready)
                        {
                            allPlayersReady = false;
                        }
                    }

                    //if all players are ready start game
                    if (allPlayersReady && item.PlayerList.Count > 1)
                    {
                        foreach (Player player in item.PlayerList)
                        {
                            Udp.UdpSrv.sendMessage("</STARTGAME/>" + item.MapName, player.EndPoint.Address.ToString(), player.EndPoint.Port);
                        }
                    }

                    break;

                }
            }
        }
    }
}