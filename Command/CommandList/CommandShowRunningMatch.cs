using System.Net.Sockets;

namespace ServerAlpha.Command.CommandList
{
    internal class CommandShowRunningMatch
    {
        /// <summary>
        /// Show all running match.
        /// </summary>
        /// <param name="args">no useful parameters</param>
        /// <returns></returns>
        public static bool commandShowRunningMatch(string[]? args)
        {
            bool commandCompletedCorrectly = true;
            
            var matchList = Server.ServerCommand.ServerCommandHandler.games;

            if (matchList.Count == 0)
            {
                Console.WriteLine("\nNo matches running!");
            }
            else
            {
                Console.WriteLine("\nRunning matches:\n");
                foreach(var match in matchList)
                    Console.WriteLine($"Match ID: {match.GameUID} | Player Count: {match.PlayerList.Count} | Match Map: {match.MapName}");
            }


            return commandCompletedCorrectly;
        }
    }
}
