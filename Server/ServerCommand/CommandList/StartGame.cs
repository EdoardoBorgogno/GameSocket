using System.Net;

namespace ServerAlpha.Server.ServerCommand.CommandList
{
    internal class StartGame
    {
        /// <summary>
        /// Start a new game.
        /// </summary>
        /// <param name="message">Message to handle.</param>
        /// <param name="senderAddress">Address of sender.</param>
        /// <param name="senderPort">Port of sender.</param>
        /// <returns>Game object.</returns>
        public static Game startGame(string message, string senderAddress, int senderPort)
        {
            Game game = new Game(message.Split(';')[0]);

            // Add the player to the game
            game.addPlayer(senderAddress, senderPort);
            
            return game;
        }
    }
}