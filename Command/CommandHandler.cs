namespace ServerAlpha.Command
{
    internal class CommandHandler
    {
        // Dictionary with all commands and their functions to execute
        static Dictionary<string, Delegate> dispatcher = new Dictionary<string, Delegate>()
        {
            { "-version", new Func<string[]?, bool>(CommandList.CommandVersion.commandVersion) },
            { "-serve", new Func<string[]?, bool>(CommandList.CommandServe.commandServe) },
            { "-shut-server", new Func<string[]?, bool>(CommandList.CommandShutServer.commandShutServer) },
            { "-exit", new Func<string[]?, bool>(CommandList.CommandExit.commandExit) }
        };

        /// <summary>
        /// Handle command with its parameters.
        /// </summary>
        /// <param name="command">Command name</param>
        /// <param name="args">Array of with all parameters and their values.</param>
        public static void commandHandle(string command, string[]? args)
        {
            //
            // Dispatcher ==> If dictionary contains command, execute releated function, otherwise COD-69
            //

            if (dispatcher.ContainsKey(command))
            {
                dispatcher[command].DynamicInvoke(new object[] { args! });
            }
            else
            {
                Console.WriteLine("\nNot found command! -- COD-69");
            }
        }
    }
}
