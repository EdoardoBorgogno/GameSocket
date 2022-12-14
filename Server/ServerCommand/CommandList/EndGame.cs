namespace ServerAlpha.Server.ServerCommand.CommandList
{
    internal class EndGame
    {
        /// <summary>
        /// End game, Remove player from list and remove game from game list.
        /// </summary>
        public static void endGame(string message, string senderAddress, int senderPort, List<Game> games)
        {
            foreach(var game in games)
            {
                foreach(var player in game.PlayerList)
                {
                    if (player.EndPoint.Address.ToString() == senderAddress && player.EndPoint.Port == senderPort)
                    {
                        Udp.UdpSrv.sendMessage("</CLOSEGAME/>", player.EndPoint.Address.ToString(), player.EndPoint.Port);
                        game.PlayerList.Remove(player);
                        break;
                    }
                }

                if (game.PlayerList.Count == 0)
                {
                    games.Remove(game);
                    break;
                }
            }
        }
    }
}