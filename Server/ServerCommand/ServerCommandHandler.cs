namespace ServerAlpha.Server.ServerCommand
{
    internal class ServerCommandHandler
    {
        // List of all matches on server.
        private static List<Game> games = new List<Game>();

        /// <summary>
        /// Handle server command sent by player.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        /// <param name="message">Message to handle.</param>
        /// <param name="senderAddress">Address of sender.</param>
        /// <param name="senderPort">Port of sender.</param>
        public static void serverCommandHandle(string command, string message, string senderAddress, int senderPort)
        {
            bool taskComplete = true;
            
            switch (command)
            {
                case "STARTGAME":
                    {
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
                    }
                    break;
                case "ENDGAME":
                    break;
                case "JOINGAME":
                    {
                        CommandList.JoinGame.joinGame(message, senderAddress, senderPort, games);
                    }
                    break;
                case "READY":
                    {
                        CommandList.Ready.ready(message, senderAddress, senderPort, games);
                    }
                    break;
                case "MOVE":
                    {
                        CommandList.MatchCommand.sendPlayerAction("</MOVE/>" + message, senderAddress, senderPort, games);
                    }
                    break;
                case "JUMP":
                    {
                        CommandList.MatchCommand.sendPlayerAction("</JUMP/>", senderAddress, senderPort, games);
                    }
                    break;
                case "SHOOT":
                    {
                        CommandList.MatchCommand.sendPlayerAction("</SHOOT/>", senderAddress, senderPort, games);
                    }
                    break;
                case "HP":
                    {
                        CommandList.MatchCommand.sendPlayerAction("</HP/>" + message, senderAddress, senderPort, games);
                    }
                    break;
            }
        }
    }
}