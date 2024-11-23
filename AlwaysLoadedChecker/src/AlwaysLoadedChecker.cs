using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UELib;

namespace AlwaysLoadedChecker
{
    public partial class AlwaysLoadedChecker : Form
    {
        static public AlwaysLoadedChecker LoadingInstance = null;

        public Dictionary<Control, string> ManualTooltips = null;

        public string CookedPackagePath, EditorPackagePath;
        public string PackageName;

        public struct ClassAlwaysLoadedStatus
        {
            public string ClassName;
            public bool IsAlwaysLoaded;

            public ClassAlwaysLoadedStatus(string className, bool isAlwaysLoaded)
            {
                ClassName = className;
                IsAlwaysLoaded = isAlwaysLoaded;
            }
        };

        public List<ClassAlwaysLoadedStatus> StatusList = new List<ClassAlwaysLoadedStatus>();
        public bool WasSorted = false;

        public List<string> CookedClassUpperNames = new List<string>();
        public List<string> EditorClassUpperNames = new List<string>();

        protected CancellationTokenSource PreloadUICancelToken;
        protected Task PreloadTask = null;

        public Func<AlwaysLoadedChecker, bool, bool> PostLoadCallback = null;

        public enum ELogFilterType
        {
            All,
            AlwaysLoaded,
            NonAlwaysLoaded
        };

        // Should be called before the form is opened. Returns false to abort opening.
        static public bool PreOpen()
        {
            if (LoadingInstance != null)
            {
                Program.LogMessage
                (
                    "Please wait for the current Checker window to finish loading!",
                    $"Cannot load two Checker windows at once", type: Program.ELogType.Error
                );
                return false;
            }

            return true;
        }

        public void InitManualTooltips()
        {
            ManualTooltips = new Dictionary<Control, string>()
            {
                {classTextBox, "Write the name of a class from " + (PackageName ?? "the package you selected")}
            };
        }

        // Tool-tips do not work all that well in TextBoxes for some reason (at least in Windows 10). Handle them manually. :))))
        private void manualTooltipControl_MouseEnter(object sender, EventArgs e)
        {
            if (!ManualTooltips.TryGetValue((Control)sender, out string tip))
                return;
            Point relativeMousePos = ((Control)sender).PointToClient(Cursor.Position);
            // Since the TextBoxes are fairly small in my use-case, stick the tool-tip to the bottom of it.
            relativeMousePos.Y = ((Control)sender).Height + 4;
            toolTip.Show(tip, (Control)sender, relativeMousePos);
        }

        private void manualTooltipControl_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide((Control)sender);
        }

        public AlwaysLoadedChecker(string cookedPackagePath, string scriptPackagePath, string packageName)
        {
            InitializeComponent();

            LoadingInstance = this;

            CookedPackagePath = cookedPackagePath;
            EditorPackagePath = scriptPackagePath;
            PackageName = packageName;

            Text += " - " + PackageName;
        }

        public void SetLoadingProgress(double progress)
        {
            labelLoadingProgress.Text = (int)(Math.Round(progress * 100.0)) + "%";
            panelLoadingInner.Width = (int)(Math.Round(panelLoadingOuter.Width * progress));
        }

        private void AlwaysLoadedChecker_Shown(object sender, EventArgs e)
        {
            theMenu.Visible = false;
            loadingPanel.Visible = true;

            Action<string, double> progressAction = (t, p) =>
            {
                BeginInvoke(new MethodInvoker(() => {
                    loadingLabel.Text = t ?? "";  // Could be null, in which case use fallback.
                    SetLoadingProgress(p);
                }));
            };

            PreloadUICancelToken = new CancellationTokenSource();
            PreloadTask = Task.Factory.StartNew(() => { Preload(PreloadUICancelToken.Token, progressAction, UI: true); }, PreloadUICancelToken.Token);
        }

        private void AlwaysLoadedChecker_FormClosing(object sender, FormClosingEventArgs e)
        {
            // NOTE: No clue if this is necessary.
            timerFlash.Stop();

            if (PreloadUICancelToken != null) PreloadUICancelToken.Cancel();
            loadingLabel.Text = "Cancelling...";

            // This halts the UI thread, until the preload thread is done cancelling.
            if (PreloadTask != null)
                PreloadTask.Wait();
            PreloadTask = null;

            if (LoadingInstance == this)
                LoadingInstance = null;
        }

        // NOTE: progressAction's string parameter (the user-firendly status message) is passed as null at 100%.
        // If UI = true, invokes PostPreload on the form's thread.
        public bool Preload(CancellationToken cancellationToken, Action<string, double> progressAction = null, bool UI = false)
        {
            UnrealPackage loadedPackage = null;

            TextWriter originalConsoleOut = Console.Out;

            string cookedPackageFileName = Path.GetFileName(CookedPackagePath);
            string editorPackageFileName = EditorPackagePath == null ? null : Path.GetFileName(EditorPackagePath);
            bool filenamesAreTheSame = (editorPackageFileName != null && String.Compare(editorPackageFileName, cookedPackageFileName, true) == 0);

            try
            {
                // Build the list of AlwaysLoaded classes (i.e. those that are present in the cooked package).

                if (progressAction != null)
                    progressAction("Decompressing" + (filenamesAreTheSame ? " cooked package" : "") + ":\n" + cookedPackageFileName, 0.0);

                var decompressCachePath = Path.Combine(DecompressFacade.GetDecompressCacheDir(), "AlwaysLoadedChecker");
                Directory.CreateDirectory(decompressCachePath);

                cancellationToken.ThrowIfCancellationRequested();

                var uncompressedPath = DecompressFacade.DecompressPackage(CookedPackagePath, decompressCachePath);

                string className, upperClassName;

                if (!cancellationToken.IsCancellationRequested)  // If cancelled, skip code, but don't throw just yet so we can delete the decompressed file.
                {
                    if (progressAction != null)
                        progressAction("Loading decompressed" + (filenamesAreTheSame ? " cooked package" : "") + ":\n" + cookedPackageFileName, EditorPackagePath == null ? 0.5 : 0.333333);
                    if (!cancellationToken.IsCancellationRequested)  // I don't trust threading, so check again.
                    {
                        // HACK: Temporarily remove the console TextWriter so the package file summary doesn't get show up.
                        Console.SetOut(TextWriter.Null);

                        loadedPackage = UnrealLoader.LoadPackage(uncompressedPath);

                        Console.SetOut(originalConsoleOut);

                        foreach (var exp in loadedPackage.Exports)
                        {
                            // An export is a class if ClassIndex is 0.
                            if (exp.ClassIndex != 0) continue;
                            className = exp.ObjectName.ToString();
                            CookedClassUpperNames.Add(className.ToUpperInvariant());
                            StatusList.Add(new ClassAlwaysLoadedStatus(className, true));
                        }
                        CookedClassUpperNames.TrimExcess();
                        loadedPackage.Dispose();
                        loadedPackage = null;
                    }
                }

                File.Delete(uncompressedPath);

                cancellationToken.ThrowIfCancellationRequested();

                if (EditorPackagePath != null)
                {
                    // Build the list of valid editor classes. Use the editor package here as it doesn't require decompressing.

                    if (progressAction != null)
                        progressAction("Loading" + (filenamesAreTheSame ? " editor package" : "") + ":\n" + editorPackageFileName, 0.666666);
                    cancellationToken.ThrowIfCancellationRequested();  // I don't trust threading, so check again.

                    // HACK: Temporarily remove the console TextWriter so the package file summary doesn't get show up.
                    Console.SetOut(TextWriter.Null);

                    loadedPackage = UnrealLoader.LoadPackage(EditorPackagePath);

                    Console.SetOut(originalConsoleOut);

                    if (DecompressFacade.IsPackageCompressed(loadedPackage))
                    {
                        loadedPackage.Dispose();
                        loadedPackage = null;
                        throw new ApplicationException("The given Script Package Path is a compressed package, but a non-cooked package was expected!");
                    }
                    foreach (var exp in loadedPackage.Exports)
                    {
                        // An export is a class if ClassIndex is 0.
                        if (exp.ClassIndex != 0) continue;
                        className = exp.ObjectName.ToString();
                        upperClassName = className.ToUpperInvariant();
                        EditorClassUpperNames.Add(upperClassName);
                        if (!CookedClassUpperNames.Contains(upperClassName))
                            StatusList.Add(new ClassAlwaysLoadedStatus(className, false));
                    }
                    EditorClassUpperNames.TrimExcess();
                    loadedPackage.Dispose();
                    loadedPackage = null;

                    cancellationToken.ThrowIfCancellationRequested();
                }

                StatusList.TrimExcess();

                if (progressAction != null)
                    progressAction(null, 1.0);
                if (UI) BeginInvoke(new MethodInvoker(() => { PostPreload(true); }));
                return true;
            }
            catch (OperationCanceledException)
            {
                Console.SetOut(originalConsoleOut);

                if (loadedPackage != null)
                {
                    loadedPackage.Dispose();
                    loadedPackage = null;
                }

                Console.WriteLine("AlwaysLoaded checker preloading was cancelled!");
                return false;
            }
            catch (ApplicationException ex)
            {
                Console.SetOut(originalConsoleOut);

                Program.LogMessage(ex.Message, "Invalid setting", type: Program.ELogType.Error);

                if (UI) BeginInvoke(new MethodInvoker(() => { PostPreload(false); }));
                return false;
            }
            catch (Exception ex)
            {
                Console.SetOut(originalConsoleOut);

                if (loadedPackage != null)
                {
                    loadedPackage.Dispose();
                    loadedPackage = null;
                }

                Program.LogMessage("Unexpected exception: " + ex.ToString(), type: Program.ELogType.Error);

                if (UI) BeginInvoke(new MethodInvoker(() => { PostPreload(false); }));
                return false;
            }
        }

        public void PostPreload(bool success)
        {
            PreloadTask = null;
            if (LoadingInstance == this)
                LoadingInstance = null;

            if (PostLoadCallback != null && PostLoadCallback(this, success))
                return;

            if (!success)
            {
                Close();
                return;
            }

            InitManualTooltips();

            if (EditorPackagePath == null)
            {
                // Gray these out since we weren't given a script package to detect Non-AL classes from.
                logToTextFileAllToolStripMenuItem.Enabled = false;
                logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem.Enabled = false;
            }

            // Alright, we're ready to go!
            loadingPanel.Visible = false;
            theMenu.Visible = true;
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            DoTheSearch();
        }

        bool pressedEnter = false;

        private void classTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;  // Prevents the TextBox from beeping.
            if (pressedEnter) return;
            e.Handled = true;
            pressedEnter = true;
        }

        private void classTextBox_Leave(object sender, EventArgs e)
        {
            pressedEnter = false;
        }

        // Using KeyUp instead of KeyDown, since it doesn't get spammed when you keep it held down...
        // But only after KeyDown was detected, so we don't detect unpresses from, say, dismissing a message box.
        private void classTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;  // Prevents the TextBox from beeping.
            if (!pressedEnter) return;
            e.Handled = true;
            pressedEnter = false;
            DoTheSearch();
        }

        public void DoTheSearch()
        {
            var className = classTextBox.Text.Trim().ToUpperInvariant();

            if (className.Length >= 7 && className.Substring(0, 6) == "CLASS'" && className.Last() == '\'')
            {
                // Remove the "Class'...'" cast, if it's there for some reason??
                className = className.Substring(6, className.Length - 7).Trim();
            }
            if (className.Length >= 3 && className.Substring(className.Length - 3, 3) == ".UC")
            {
                // Remove the ".uc" extension, if someone believes that's necessary lol.
                className = className.Substring(0, className.Length - 3).TrimEnd();
            }
            if (PackageName != null)
            {
                int prefixLength = PackageName.Length + 1;
                if (className.Length >= prefixLength && className.Substring(0, prefixLength) == PackageName.ToUpperInvariant() + ".")
                {
                    // Remove the package prefix, if it exists. (Honestly, who would do this?)
                    className = className.Substring(prefixLength).TrimStart();
                }
            }

            timerFlash.Stop();
            Color originalBackColor = resultLabel.Parent.BackColor;

            if (className.Length == 0)
            {
                resultLabel.Text = $"Please type the name of a class from {PackageName ?? "the package you selected"}!";
                resultLabel.ForeColor = Color.Purple;
                resultLabel.BackColor = LerpColor(Color.Purple, originalBackColor, 0.9);
            }
            else
            {
                bool isInMainPackage = EditorPackagePath == null || EditorClassUpperNames.Contains(className);
                bool isInStartupPackage = CookedClassUpperNames.Contains(className);

                if (isInMainPackage)
                {
                    if (isInStartupPackage)
                    {
                        resultLabel.Text = "This class is AlwaysLoaded!";
                        resultLabel.ForeColor = Color.Green;
                        resultLabel.BackColor = LerpColor(Color.Green, originalBackColor, 0.85);
                    }
                    else
                    {
                        resultLabel.Text = "This class is NOT AlwaysLoaded..." + (EditorPackagePath == null ? " (or doesn't exist)" : "");
                        resultLabel.ForeColor = Color.Red;
                        resultLabel.BackColor = LerpColor(Color.Red, originalBackColor, 0.9);
                    }
                }
                else
                {
                    if (isInStartupPackage)
                    {
                        resultLabel.Text = "This class is AlwaysLoaded!\n(But isn't present in the editor package.)";
                    }
                    else
                    {
                        resultLabel.Text = $"Could not find this class. Is this a valid class from {PackageName ?? "the package you selected"}?";
                    }
                    resultLabel.ForeColor = Color.Purple;
                    resultLabel.BackColor = LerpColor(Color.Purple, originalBackColor, 0.9);
                }
            }

            timerFlash.Start();
        }

        public Color LerpColor(Color a, Color b, double interp)
        {
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * interp),
                (int)(a.R + (b.R - a.R) * interp),
                (int)(a.G + (b.G - a.G) * interp),
                (int)(a.B + (b.B - a.B) * interp)
            );
        }

        private void timerFlash_Tick(object sender, EventArgs e)
        {
            resultLabel.BackColor = resultLabel.Parent.BackColor;
            timerFlash.Stop();
        }

        private void logToTextFileAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskForTextFile(out string path)) return;
            if (CreateAndLogToTextFile(path, ELogFilterType.All))
                OnPostUILog();
        }

        private void logToTextFileAlwaysLoadedOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskForTextFile(out string path)) return;
            if (CreateAndLogToTextFile(path, ELogFilterType.AlwaysLoaded))
                OnPostUILog();
        }
        
        private void logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AskForTextFile(out string path)) return;
            if (CreateAndLogToTextFile(path, ELogFilterType.NonAlwaysLoaded))
                OnPostUILog();
        }

        public bool AskForTextFile(out string path)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
                return true;
            }

            path = "";
            return false;
        }

        public bool CreateAndLogToTextFile(string path, ELogFilterType filterType)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(path))
                {
                    LogToTextFile(outputFile, filterType);
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.LogMessage("Unexpected exception while writing log: " + ex.ToString(), type: Program.ELogType.Error);
                return false;
            }
        }

        public void LogToTextFile(StreamWriter output, ELogFilterType filterType)
        {
            if (!WasSorted)
                SortStatusList();

            Func<ClassAlwaysLoadedStatus, bool> func = null;
            switch (filterType)
            {
                case ELogFilterType.All:
                    // When using no filter (All)
                    foreach (var s in StatusList)
                    {
                        output.WriteLine(s.ClassName + (s.IsAlwaysLoaded ? " A" : " N"));
                    }
                    return;
                case ELogFilterType.AlwaysLoaded:    func = ((s) => s.IsAlwaysLoaded);  break;
                case ELogFilterType.NonAlwaysLoaded: func = ((s) => !s.IsAlwaysLoaded); break;
            }
            // When using a particular filter
            foreach (var s in StatusList)
            {
                if (func(s))
                    output.WriteLine(s.ClassName);
            }
        }

        private void OnPostUILog()
        {
            resultLabel.Text = "Wrote text file successfully!";
            resultLabel.ForeColor = Color.Blue;

            timerFlash.Stop();
            resultLabel.BackColor = LerpColor(Color.Blue, resultLabel.Parent.BackColor, 0.9);
            timerFlash.Start();
        }

        public void SortStatusList()
        {
            StatusList.Sort((x, y) => string.Compare(x.ClassName, y.ClassName, true));
            WasSorted = true;
        }
    }
}
