using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AlwaysLoadedChecker
{
    public static class Program
    {
        public enum ErrorCodes
        {
            SUCCESS = 0x00,
            ERROR_FILE_NOT_FOUND = 0x02,
            ERROR_BAD_ARGUMENTS = 0xA0
        };

        static public bool ConsoleMode = false;

        static public string GamePath = null, PassedCookedPath = null, PassedCompiledPath = null;
        static public string LogClassesToTextFile = null;
        static public AlwaysLoadedChecker.ELogFilterType ClassLogFilter = AlwaysLoadedChecker.ELogFilterType.All;
        static public bool ShowWarnings = true, WindowsOpenWith = false;

        public enum ELogType
        {
            Normal,
            Verbose,
            Question,
            Warning,
            Error
        };

        static public void LogMessage(string contents, string title = null, ELogType type = ELogType.Normal)
        {
            if (ConsoleMode)
            {
                ConsoleColor usedColor = GetColorFromLogType(type);
                if (usedColor != ConsoleColor.White) Console.ForegroundColor = usedColor;

                contents = contents.Replace('\n', ' ');
                if (title != null)
                    contents = (title == null ? "" : (title + GetLogTypePunctuationMark(type) + " ")) + contents;
                
                Console.WriteLine($"[ALWAYSLOADEDCHECKER {GetFriendlyLogTypeName(type)}] {contents}");

                if (usedColor != ConsoleColor.White) Console.ResetColor();

                return;
            }

            MessageBox.Show(
                contents.Replace("\n", "\n\n"),
                title ?? ("AlwaysLoadedChecker " + GetFriendlyLogTypeName(type)),
                MessageBoxButtons.OK,
                GetMessageBoxIconFromLogType(type)
            );
        }

        static public MessageBoxIcon GetMessageBoxIconFromLogType(ELogType type)
        {
            switch (type)
            {
                case ELogType.Verbose:
                    return MessageBoxIcon.None;
                case ELogType.Question:
                    return MessageBoxIcon.Question;
                case ELogType.Warning:
                    return MessageBoxIcon.Warning;
                case ELogType.Error:
                    return MessageBoxIcon.Error;
                case ELogType.Normal:
                default:
                    return MessageBoxIcon.Information;
            }
        }

        static public ConsoleColor GetColorFromLogType(ELogType type)
        {
            switch (type)
            {
                case ELogType.Verbose:
                    return ConsoleColor.DarkGray;
                case ELogType.Question:
                    return ConsoleColor.Cyan;
                case ELogType.Warning:
                    return ConsoleColor.Red;
                case ELogType.Error:
                    return ConsoleColor.Red;
                case ELogType.Normal:
                default:
                    return ConsoleColor.White;
            }
        }

        static public string GetFriendlyLogTypeName(ELogType type)
        {
            switch (type)
            {
                case ELogType.Verbose:
                    return "Verbose";
                case ELogType.Question:
                    return "Question";
                case ELogType.Warning:
                    return "Warning";
                case ELogType.Error:
                    return "Error";
                case ELogType.Normal:
                default:
                    return "Info";
            }
        }

        static public string GetLogTypePunctuationMark(ELogType type)
        {
            switch (type)
            {
                case ELogType.Question:
                    return "";
                case ELogType.Warning:
                case ELogType.Error:
                    return "!";
                case ELogType.Normal:
                case ELogType.Verbose:
                default:
                    return ".";
            }
        }

        public static int CheckerMain(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToUpperInvariant())
                {
                    case "-GAME":
                    case "-GAMEPATH":
                    case "-GAMEDIR":
                    case "-GAMEDIRECTORY":
                        GamePath = args[++i];
                        break;
                    case "-COOKED":
                    case "-COOKEDPACKAGE":
                    case "-STARTUP":
                    case "-STARTUPPACKAGE":
                        PassedCookedPath = args[++i];
                        break;
                    case "-COMPILED":
                    case "-COMPILEDPACKAGE":
                    case "-EDITOR":
                    case "-EDITORPACKAGE":
                    case "-SCRIPT":
                    case "-SCRIPTPACKAGE":
                        PassedCompiledPath = args[++i];
                        break;
                    case "-SHOWWARNS":
                    case "-SHOWWARNINGS":
                        ShowWarnings = true;
                        break;
                    case "-HIDEWARNS":
                    case "-HIDEWARNINGS":
                        ShowWarnings = false;
                        break;
                    case "-SETWARNVISIBILITY":
                    case "-SETWARNINGVISIBILITY":
                        i++;
                        switch (args[i].ToUpperInvariant())
                        {
                            case "0":
                            case "FALSE":
                            case "OFF":
                            case "NO":
                            case "N":
                                ShowWarnings = false;
                                break;
                            default:
                                ShowWarnings = true;
                                break;
                        }
                        break;
                    case "-LOGCLASSES":
                    case "-LOGCLASSESTO":
                    case "-LOGSCRIPTS":
                    case "-LOGSCRIPTSTO":
                        LogClassesToTextFile = args[++i];
                        break;
                    case "-CLASSLOGFILTER":
                    case "-CLASSLOGTYPE":
                        i++;
                        switch (args[i].ToUpperInvariant())
                        {
                            case "ALL":
                            case "0":
                                ClassLogFilter = AlwaysLoadedChecker.ELogFilterType.All;
                                break;
                            case "ALWAYSLOADED":
                            case "1":
                                ClassLogFilter = AlwaysLoadedChecker.ELogFilterType.AlwaysLoaded;
                                break;
                            case "NONALWAYSLOADED":
                            case "NON-ALWAYSLOADED":
                            case "2":
                                ClassLogFilter = AlwaysLoadedChecker.ELogFilterType.NonAlwaysLoaded;
                                break;
                            default:
                                LogMessage($"Unrecognised class log filter type:\n{args[i]}", type: ELogType.Error);
                                return (int)ErrorCodes.ERROR_BAD_ARGUMENTS;
                        }
                        break;
                    default:
                        // Could be the path argument that windows passes on Open With?
                        // Restrict to non-Cli mode only, as that's the only context where Open With makes sense, really.
                        if (!ConsoleMode && i == 0 && File.Exists(args[i]))
                        {
                            PassedCookedPath = args[i];
                            WindowsOpenWith = true;
                            break;
                        }
                        LogMessage($"Unexpected launch argument:\n{args[i]}", type: ELogType.Error);
                        return (int)ErrorCodes.ERROR_BAD_ARGUMENTS;
                }
            }

            bool wasGivenAction = (LogClassesToTextFile != null);

            AlwaysLoadedChecker checker = null;
            if (PassedCookedPath != null)
            {
                // Opening with Open With doesn't even have a way to provide both the cooked and compiled packages, so don't warn about it.
                checker = LaunchAdvanced(PassedCookedPath, PassedCompiledPath, ShowWarnings, showMissingCompiledWarning: !WindowsOpenWith, owner: null);
            }
            else if (GamePath != null)
            {
                checker = LaunchSimple(GamePath, ShowWarnings, owner: null);
            }

            if (wasGivenAction && checker == null)
            {
                return (int)ErrorCodes.ERROR_FILE_NOT_FOUND;
            }

            if (checker != null)
            {
                if (LogClassesToTextFile != null)
                {
                    if (ConsoleMode)
                    {
                        // Then don't open the form, and just preload on the main thread.

                        LogMessage("Starting preload...", type: ELogType.Normal);

                        Action<string, double> progressAction = (t, p) =>
                        {
                            if (t == null) return;
                            LogMessage($"{String.Format("{0,3:D}", (int)(Math.Round(p * 100.0)))}% : {t}", type: ELogType.Verbose);
                        };

                        if (!checker.Preload(new CancellationTokenSource().Token, progressAction))
                        {
                            LogMessage("Loading failed! Aborting class logging...", type: ELogType.Error);
                            return (int)ErrorCodes.ERROR_BAD_ARGUMENTS;
                        }

                        LogMessage("Finished preloading! Logging classes to file...", type: ELogType.Normal);

                        if (checker.CreateAndLogToTextFile(LogClassesToTextFile, ClassLogFilter))
                            LogMessage("Finished logging!", type: ELogType.Normal);

                        checker.Dispose();

                        return (int)ErrorCodes.SUCCESS;
                    }
                    else
                    {
                        // Then, I guess open the form and let it do its whole UI thing, then save on a callback.
                        // Curious to see if anyone will ever run the form executable like this. :P

                        checker.PostLoadCallback = (inChecker, success) =>
                        {
                            if (success && inChecker.CreateAndLogToTextFile(LogClassesToTextFile, ClassLogFilter))
                                LogMessage($"Finished logging to file:\n{LogClassesToTextFile}", type: ELogType.Normal);
                            inChecker.Close();
                            return true;
                        };
                    }
                    
                }

                checker.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(checker);

                return (int)ErrorCodes.SUCCESS;
            }

            if (ConsoleMode)
            {
                // If we got to this point in console mode, then we weren't given any action to do (only context arguments). Error and exit.

                LogMessage("No action arguments given! For launching the app's UI, use AlwaysLoadedCheckerApp.exe! Exiting...", type: ELogType.Error);

                return (int)ErrorCodes.ERROR_BAD_ARGUMENTS;
            }

            var form = new MainForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(form);

            return (int)ErrorCodes.SUCCESS;
        }

        static bool PreLaunchChecks()
        {
            if (!DecompressFacade.DoesDecompressorExist())
            {
                if (ConsoleMode)
                {
                    LogMessage($"Couldn't find decompress.exe! " +
                        $"Please download Glidor's Package Decompressor, place it in the app's root, and try again: {DecompressFacade.DECOMPRESS_LINK}",
                        type: ELogType.Error
                    );
                    return false;
                }

                var result = MessageBox.Show(
                    "Glidor's Package Decompressor is required in this tool's root folder for it to work.\n\n"
                    + "Open its download page?",
                    "Could not launch - Missing decompress.exe!", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
                if (result == DialogResult.Yes)
                {
                    DecompressFacade.OpenDecompressorDownloadPageInBrowser();
                }
                return false;
            }

            if (!AlwaysLoadedChecker.PreOpen())
                return false;

            return true;
        }

        static public AlwaysLoadedChecker LaunchSimple(string gamePath, bool showWarnings, IWin32Window owner = null)
        {
            string cookedPath, compiledPath;

            if (!PreLaunchChecks())
                return null;

            if (gamePath == "" || !Directory.Exists(gamePath))
            {
                LogMessage("Please set a valid Game Directory before continuing!", type: ELogType.Error);
                return null;
            }

            cookedPath = Path.Combine(gamePath, @"HatinTimeGame\CookedPC\Startup.upk");
            compiledPath = Path.Combine(gamePath, @"HatinTimeGame\EditorCookedPC\HatinTimeGameContent.u");

            if (!File.Exists(cookedPath))
            {
                LogMessage("Check that the game directory is correct!", "Missing HatinTimeGame\\CookedPC\\Startup.upk", type: ELogType.Error);
                return null;
            }

            if (!File.Exists(compiledPath))
            {
                if (showWarnings)
                {
                    string warnText = "The modding tools might not be installed. While this tool can still function without them, " +
                        "it won't be able to tell whether a class exists or not.";
                    string warnTitle = "Missing HatinTimeGame\\EditorCookedPC\\HatinTimeGameContent.u";
                    if (ConsoleMode)
                    {
                        LogMessage(warnText, warnTitle, type: ELogType.Error);
                    }
                    else
                    {
                        var result = MessageBox.Show(warnText + "\n\nContinue anyway?", warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            return null;
                        }
                    }
                }
                
                compiledPath = null;
            }

            string packageName = compiledPath == null ? Path.GetFileNameWithoutExtension(cookedPath) : Path.GetFileNameWithoutExtension(compiledPath);
            return new AlwaysLoadedChecker(cookedPath, compiledPath, packageName);
        }

        static public AlwaysLoadedChecker LaunchAdvanced(string cookedPath, string compiledPath, bool showWarnings, bool showMissingCompiledWarning = true, IWin32Window owner = null)
        {
            if (!PreLaunchChecks())
                return null;

            if (cookedPath == "")
            {
                LogMessage($"The Cooked Package must be specified for Advanced mode to work!", type: ELogType.Error);
                return null;
            }
            else if (!File.Exists(cookedPath))
            {
                LogMessage($"Could not find cooked package at the given path:\n{cookedPath}", type: ELogType.Error);
                return null;
            }
            else if (showWarnings
                && String.Equals(Path.GetFileName(cookedPath).ToUpperInvariant(), "HATINTIMEGAMECONTENT.U")
                && String.Equals(Path.GetFileName(Path.GetDirectoryName(cookedPath)).ToUpperInvariant(), "COOKEDPC")
            )
            {
                // The user chose CookedPC\HatinTimeGameContent.u, which contains all classes, regardless of whether they're
                // AlwaysLoaded or not. By normal means, the game never uses this package, so of course relying on it doesn't
                // yield correct results in the tool.
                // The user LIKELY close this package by mistake, so warn them before continuing.
                string warnText = "Using CookedPC\\HatinTimeGameContent.u will cause the tool to incorrectly detect all classes as AlwaysLoaded.\n" +
                    "For correct results, please select CookedPC\\Startup.upk, or use Simple mode instead.";
                string warnTitle = "CookedPC\\HatinTimeGameContent.u detected";
                if (ConsoleMode)
                {
                    LogMessage(warnText, warnTitle, type: ELogType.Warning);
                }
                else
                {
                    var result = MessageBox.Show(
                        warnText.Replace("\n", "\n\n") + "\n\nContinue with the current settings anyway?",
                        warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning
                    );
                    if (result == DialogResult.No)
                    {
                        return null;
                    }
                }
            }

            if (compiledPath == null || compiledPath == "")
            {
                if (showWarnings && showMissingCompiledWarning)
                {
                    string warnText = "While this tool can function without specifying an uncooked compiled scripts package, " +
                        "it won't be able to tell whether a class exists or not.";
                    string warnTitle = "Unspecified script package";

                    if (ConsoleMode)
                    {
                        LogMessage(warnText, warnTitle, type: ELogType.Warning);
                    }
                    else
                    {
                        var result = MessageBox.Show(warnText + "\n\nContinue anyway?", warnTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            return null;
                        }
                    }
                }

                compiledPath = null;
            }
            else if (!File.Exists(compiledPath))
            {
                LogMessage($"Could not find compiled script package at the given path:\n{compiledPath}", type: ELogType.Error);
                return null;
            }

            string packageName = compiledPath == null ? Path.GetFileNameWithoutExtension(cookedPath) : Path.GetFileNameWithoutExtension(compiledPath);
            return new AlwaysLoadedChecker(cookedPath, compiledPath, packageName);
        }

        public static string GetAppRoot()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        static public void StartInDefaultBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception)
            {
            }
        }
    }
}
