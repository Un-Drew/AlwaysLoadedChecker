using System;

namespace AlwaysLoadedChecker.App
{
    internal class ProgramApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Program.ConsoleMode = false;
            return Program.CheckerMain(args);
        }
    }
}
