namespace ServerAlpha.Server.ServerCommand
{
    internal class ServerCommandHandler
    {
        private static List<Game> games = new List<Game>();

        public static void serverCommandHandle(string command, string message, string senderAddress, int senderPort)
        {
            bool taskComplete = true;

            switch (command)
            {
                case "STARTGAME":

                    Game? game = null;

                    try
                    {
                        game = CommandList.StartGame.startGame(message, senderAddress, senderPort);
                        games.Add(game);
                    }
                    catch (Exception)
                    {
                        taskComplete = false;
                    }

                    if (taskComplete)
                    {
                        Udp.UdpSrv.sendMessage("</GAMEINIT/>" + game!.GameUID + ";" + game.GamePassword, senderAddress, senderPort);
                    }

                    break;
                case "ENDGAME":
                    break;
                case "JOINGAME":

                    // Check if the game exists
                    foreach (Game item in games)
                    {
                        if (item.GameUID == message.Split(";")[0])
                        {
                            if(item.GamePassword == message.Split(";")[1])
                            {
                                if(item.addPlayer(senderAddress, senderPort))
                                {
                                    Udp.UdpSrv.sendMessage("</JOINEDTOGAME/>", senderAddress, senderPort);
                                }
                            }
                        }
                    }

                    break;
                case "READY":

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

                    break;
                case "MOVE":
                    break;
                case "JUMP":
                    break;
                case "SHOOT":
                    break;
                case "HP":
                    break;
            }
        }
    }
}