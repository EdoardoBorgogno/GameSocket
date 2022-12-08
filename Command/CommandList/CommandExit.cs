using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAlpha.Command.CommandList
{
    internal class CommandExit
    {
        /// <summary>
        /// Application exit, if server is still working, close it (because it is in a back thread).
        /// </summary>
        /// <param name="args">No useful param for this function.</param>
        public static bool commandExit(string[]? args)
        {
            bool commandCompletedCorrectly = true;

            try
            {
                Console.Write("\nIf the server is still active it will be shut down");
                Console.WriteLine("\nPress a key to exit...");
                Console.ReadKey();
            }
            catch (Exception)
            {
                commandCompletedCorrectly = false;
            }

            return commandCompletedCorrectly;
        }
    }
}
