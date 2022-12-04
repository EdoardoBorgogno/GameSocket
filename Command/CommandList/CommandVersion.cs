using System.Configuration;

namespace ServerAlpha.Command.CommandList
{
    internal class CommandVersion
    {
        /// <summary>
        /// Print the version of the app.
        /// </summary>
        /// <param name="args">No useful param for this function.</param>
        public static bool commandVersion(string[]? args)
        {
            bool commandCompletedCorrectly = true;

            try
            {
                Console.WriteLine("\nApplication Version: " + ConfigurationManager.AppSettings["AppVersion"]);
            }
            catch (Exception)
            {
                commandCompletedCorrectly = false;
            }

            return commandCompletedCorrectly;
        }
    }
}
