using System.Net.Sockets;
using System.Net;

namespace ServerAlpha.Server.ServerCommand
{
    internal class Game
    {
        // Fields
        private readonly string gameUID;
        private readonly string gamePassword;
        private List<Player> playerList = new List<Player>();
        private readonly string mapName;

        // Properties
        public string GameUID
        {
            get { return gameUID; }
        }
        public string GamePassword
        {
            get { return gamePassword; }
        }
        public List<Player> PlayerList
        {
            get { return playerList; }
        }
        public string MapName
        {
            get { return mapName; }
        }
        
        // Constructor
        public Game(string map)
        {
            // Generate a random UID
            gameUID = Guid.NewGuid().ToString().Substring(0, 7);

            // Generate a random password
            gamePassword = Guid.NewGuid().ToString().Substring(0, 5);

            mapName = map;
        }
    
        //Methods

        /// <summary>
        /// Add a player to the game, only if it isn't already in the game.
        /// </summary>
        /// <param name="address">Address of the player.</param>
        /// <param name="port">Port of the player.</param>
        public bool addPlayer(string address, int port)
        {
            if (playerList.Count >= 2)
            {
                return false;
            }

            try
            {
                // Check if the player is already in the game
                foreach (Player player in playerList)
                {
                    if (player.EndPoint.Address.ToString() == address && player.EndPoint.Port == port)
                    {
                        return false;
                    }
                }

                // Add the player to the game
                playerList.Add(new Player(new IPEndPoint(IPAddress.Parse(address), port)));
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
    }
}