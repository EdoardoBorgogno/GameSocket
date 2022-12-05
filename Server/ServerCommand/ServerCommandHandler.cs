namespace ServerAlpha.Server.ServerCommand
{
    internal class ServerCommandHandler
    {
        List<Game> games = new List<Game>();

        public static void serverCommandHandle(string command, string message, string senderAddress)
        {
            switch (command)
            {
                case "STARTGAME":
                    break;
                case "ENDGAME":
                    break;
                case "MOVE":
                    break;
                case "JUMP":
                    break;
                case "SHOOT":
                    break;
                case "READY":
                    break;
                case "HP":
                    break;
            }
        }
    }
}