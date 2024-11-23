using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CUFramework.Dialogs;
using CUFramework.Windows;
using ModdingTools.Engine;
using UELib;
using static System.Net.Mime.MediaTypeNames;

namespace ModdingTools.Windows.Tools
{
    public partial class AlwaysLoadedChecker : CUWindow
    {
        static public AlwaysLoadedChecker Instance = null;
        public List<string> HatinTimeGameContentClassUpperNames = new List<string>();
        public List<string> AlwaysLoadedClassUpperNames = new List<string>();

        protected Task PreloadTask = null;
        protected CancellationTokenSource PreloadCancelToken = new CancellationTokenSource();

        // Should be called before the form is opened. Returns false to abort opening.
        static public bool PreOpen()
        {
            if (Instance != null)
            {
                Instance.Focus();
                return false;
            }

            if (!DecompressFacade.DoesDecompressorExist())
            {
                DecompressFacade.WarnNoDecompressor();
                return false;
            }

            if (!File.Exists(GetPath_EditorHatinTimeGameContent()))
            {
                CUMessageBox.Show(
                    $"Could not find {@"HatinTimeGame\EditorCookedPC\HatinTimeGameContent.u"} in the game installation.\n" +
                    "Are the steam modding tools installed?",
                    "Missing editor package!", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }

            if (!File.Exists(GetPath_CookedStartup()))
            {
                CUMessageBox.Show(
                    $"Could not find {@"HatinTimeGame\CookedPC\Startup.upk"} in the game installation.\n" +
                    "Try verifying the integrity of the game's files?",
                    "Missing cooked package!", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }

            return true;
        }

        static public string GetPath_EditorHatinTimeGameContent()
        {
            return Path.Combine(Program.ProcFactory.GetGamePath(), @"HatinTimeGame\EditorCookedPC\HatinTimeGameContent.u");
        }

        static public string GetPath_CookedStartup()
        {
            return Path.Combine(Program.ProcFactory.GetGamePath(), @"HatinTimeGame\CookedPC\Startup.upk");
        }

        public AlwaysLoadedChecker()
        {
            InitializeComponent();

            Instance = this;
        }

        public void SetLoadingProgress(int progress)
        {
            loadingProgress.Value = progress;
            loadingProgress.Text = progress + "%";
            loadingProgress.Invalidate();
        }

        public void SetLoadingTextAndProgressAsync(string text, int progress)
        {
            Invoke(new MethodInvoker(() => {
                loadingLabel.Text = text;
                SetLoadingProgress(progress);
            }));
        }

        private void AlwaysLoadedChecker_Shown(object sender, EventArgs e)
        {
            theMenu.Visible = false;
            loadingPanel.Visible = true;

            PreloadTask = Task.Factory.StartNew(() => { PreloadAsync(PreloadCancelToken.Token); }, PreloadCancelToken.Token);
        }

        private void AlwaysLoadedChecker_FormClosing(object sender, FormClosingEventArgs e)
        {
            PreloadCancelToken.Cancel();
            loadingLabel.Text = "Cancelling...";

            if (PreloadTask != null)
                PreloadTask.Wait();
            PreloadTask = null;

            Instance = null;
        }

        public void PreloadAsync(CancellationToken cancellationToken)
        {
            UnrealPackage loadedPackage = null;

            try
            {
                // Build the list of AlwaysLoaded classes (i.e. those that are present in Startup.upk).

                SetLoadingTextAndProgressAsync("Decompressing and loading:\n" + @"CookedPC\Startup.upk", 0);

                var decompressCachePath = Path.Combine(DecompressFacade.GetDecompressCacheDir(), "AlwaysLoadedChecker");
                Directory.CreateDirectory(decompressCachePath);

                cancellationToken.ThrowIfCancellationRequested();

                var uncompressedPath = DecompressFacade.DecompressPackage(GetPath_CookedStartup(), decompressCachePath);

                if (!cancellationToken.IsCancellationRequested)
                {
                    loadedPackage = UnrealLoader.LoadPackage(uncompressedPath);
                    foreach (var exp in loadedPackage.Exports)
                    {
                        // An export is a class if ClassIndex is 0.
                        if (exp.ClassIndex != 0) continue;
                        AlwaysLoadedClassUpperNames.Add(exp.ObjectName.ToString().ToUpperInvariant());
                    }
                    AlwaysLoadedClassUpperNames.TrimExcess();
                    loadedPackage.Dispose();
                    loadedPackage = null;
                }

                File.Delete(uncompressedPath);

                cancellationToken.ThrowIfCancellationRequested();

                // Build the list of valid HatinTimeGameContent classes (so we can warn unrecognised classes, rather than
                // wrongly reporting as Non-AlwaysLoaded). Use the editor package here as it doesn't require decompressing.

                SetLoadingTextAndProgressAsync("Loading:\n" + @"EditorCookedPC\HatinTimeGameContent.u", 50);

                loadedPackage = UnrealLoader.LoadPackage(GetPath_EditorHatinTimeGameContent());
                foreach (var exp in loadedPackage.Exports)
                {
                    // An export is a class if ClassIndex is 0.
                    if (exp.ClassIndex != 0) continue;
                    HatinTimeGameContentClassUpperNames.Add(exp.ObjectName.ToString().ToUpperInvariant());
                }
                HatinTimeGameContentClassUpperNames.TrimExcess();
                loadedPackage.Dispose();
                loadedPackage = null;

                cancellationToken.ThrowIfCancellationRequested();

                SetLoadingTextAndProgressAsync("", 100);
                Invoke(new MethodInvoker(() => { FinishedPreloading(); }));
            }
            catch (OperationCanceledException)
            {
                if (loadedPackage != null)
                {
                    loadedPackage.Dispose();
                    loadedPackage = null;
                }

                Console.WriteLine("AlwaysLoaded checker preloading was cancelled!");
            }
            catch (Exception ex)
            {
                if (loadedPackage != null)
                {
                    loadedPackage.Dispose();
                    loadedPackage = null;
                }

                CUMessageBox.Show("Unexpected exception: " + ex.ToString(), "Uh-oh...", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Invoke(new MethodInvoker(() => { Close(); }));
            }
        }

        public void FinishedPreloading()
        {
            PreloadTask = null;

            // Alright, we're ready to go!
            loadingPanel.Visible = false;
            theMenu.Visible = true;
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            DoTheSearch();
        }

        // Using KeyUp instead of KeyDown, since it doesn't get spammed when you keep it held down...
        private void classTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            DoTheSearch();
        }

        public void DoTheSearch()
        {
            var className = classTextBox.Text.Trim().ToUpperInvariant();
            if (className.Length == 0)
            {
                resultLabel.Text = "Please type the name of a class from HatinTimeGameContent!";
                resultLabel.ForeColor = Color.Yellow;
                return;
            }

            if (className.Length >= 3 && className.Substring(className.Length - 3, 3) == ".UC")
            {
                // Remove the ".uc" extension, if someone believes that's necessary lol.
                className = className.Substring(0, className.Length - 3);
            }
            if (className.Length >= 7 && className.Substring(0, 6) == "CLASS'" && className.Last() == '\'')
            {
                // Remove the "Class'...'" cast, if it's there for some reason??
                className = className.Substring(6, className.Length - 7);
            }
            if (className.Length >= 21 && className.Substring(0, 21) == "HATINTIMEGAMECONTENT.")
            {
                // Remove "HatinTimeGameContent." package prefix, if it exists. (Honestly, who would do this?)
                className = className.Substring(21);
            }

            bool isInMainPackage = HatinTimeGameContentClassUpperNames.Contains(className);
            bool isInStartupPackage = AlwaysLoadedClassUpperNames.Contains(className);

            if (isInMainPackage)
            {
                if (isInStartupPackage)
                {
                    resultLabel.Text = "This class is AlwaysLoaded!";
                    resultLabel.ForeColor = Color.LimeGreen;
                }
                else
                {
                    resultLabel.Text = "This class is NOT AlwaysLoaded...";
                    resultLabel.ForeColor = Color.LightPink;
                }
            }
            else
            {
                if (isInStartupPackage)
                {
                    resultLabel.Text = "Class is AlwaysLoaded in the cooked game, but isn't present in the editor. Are your modding tools up to date?";
                    resultLabel.ForeColor = Color.Yellow;
                }
                else
                {
                    resultLabel.Text = "Could not find this class. Is this a valid class from HatinTimeGameContent?";
                    resultLabel.ForeColor = Color.Yellow;
                }
            }
        }
    }
}
