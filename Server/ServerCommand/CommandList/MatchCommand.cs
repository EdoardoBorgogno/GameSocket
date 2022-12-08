namespace ServerAlpha.Server.ServerCommand.CommandList
{
    internal class MatchCommand
    {
        /// <summary>
        /// Send player action to other players in same match.
        /// </summary>
        /// <param name="message">Message to SEND.</param>
        /// <param name="senderAddress">Address of sender.</param>
        /// <param name="senderPort">Port of sender.</param>
        /// <param name="games">List of all games on server.</param>
        public static void sendPlayerAction(string message, string senderAddress, int senderPort, List<Game> games)
        {
            Game match = games.Where(x => x.PlayerList.Where(y => y.EndPoint.Address.ToString() == senderAddress && y.EndPoint.Port == senderPort).Any()).ToList()[0];

            foreach(var player in match.PlayerList)
                if(player.EndPoint.Address.ToString() != senderAddress)
                    Udp.UdpSrv.sendMessage(message, player.EndPoint.Address.ToString(), player.EndPoint.Port);
        }
    }
}