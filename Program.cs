using ServerAlpha.Command;
using System.Configuration;

namespace ServerAlpha
{
    internal class Program
    {
        //CMD entry point
        static void Main(string[] args)
        {
            string? inputString = string.Empty;
            string[]? inputArray = null;

            //Load application informations
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ConfigurationManager.AppSettings["AppName"] + " ~ " + ConfigurationManager.AppSettings["AppVersion"] + "\n");

            do
            {
                //Print input line
                pcInfo();

                inputString = Console.ReadLine();

                if (inputString != null)
                {
                    inputArray = inputString.Split(' '); //Split input string by space, and get command and arguments/parameters

                    // Handle command, first element of inputArray is command, and the others are parameters
                    CommandHandler.commandHandle(inputArray[0], inputArray.Skip(1).ToArray());
                }

            }
            while (inputString != null && !inputString.StartsWith("-exit"));
        }

        /// <summary>
        /// Write on console Machine name and User name.
        /// </summary>
        static void pcInfo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n" + Environment.MachineName);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ~ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Environment.UserName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("/: ");
        }
    }
}