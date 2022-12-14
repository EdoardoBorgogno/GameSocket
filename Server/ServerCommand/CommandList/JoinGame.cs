using System.Net;

namespace ServerAlpha.Server.ServerCommand.CommandList
{
    internal class JoinGame
    {
        /// <summary>
        /// Join to game.
        /// </summary>
        public static void joinGame(string message, string senderAddress, int senderPort, List<Game> games)
        {
            message = message.Replace("?", "");

            foreach (Game item in games)
            {
                if (item.GameUID == message.Split(";")[0])
                {
                    if(item.GamePassword == message.Split(";")[1])
                    {
                        if(item.addPlayer(senderAddress, senderPort))
                        {
                            Udp.UdpSrv.sendMessage("</JOINEDTOGAME/>", senderAddress, senderPort);

                            foreach (Player player in item.PlayerList)
                            {
                                if(player.EndPoint.Address.ToString() != senderAddress)
                                    Udp.UdpSrv.sendMessage("</PLAYERJOINED/>", player.EndPoint.Address.ToString(), player.EndPoint.Port);
                            }
                        }
                        
                        break;
                    }
                }
            }
        }
    }
}