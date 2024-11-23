using System;

namespace AlwaysLoadedChecker.Cli
{
    internal class ProgramCli
    {
        static int Main(string[] args)
        {
            Program.ConsoleMode = true;

            if (args.Length == 0)
            {
                Program.LogMessage("No arguments provided!", type: Program.ELogType.Error);
                Program.LogMessage("To run the app's UI, use AlwaysLoadedCheckerApp.exe!", type: Program.ELogType.Normal);
                Program.LogMessage("Or for a list of arguments, check the GitHub page's wiki:", type: Program.ELogType.Normal);
                Program.LogMessage("https://github.com/Un-Drew/AlwaysLoadedChecker/wiki", type: Program.ELogType.Normal);
                Console.ReadKey(true);
                return (int)Program.ErrorCodes.ERROR_BAD_ARGUMENTS;
            }

            return Program.CheckerMain(args);
        }
    }
}
