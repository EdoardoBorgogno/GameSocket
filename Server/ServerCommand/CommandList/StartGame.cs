using System.Net;

namespace ServerAlpha.Server.ServerCommand.CommandList
{
    internal class StartGame
    {
        public static Game startGame(string message, string senderAddress, int senderPort)
        {
            Game game = new Game(message.Split(';')[0]);

            // Add the player to the game
            game.addPlayer(senderAddress, senderPort);
            
            return game;
        }
    }
}