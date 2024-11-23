using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlwaysLoadedChecker
{
    public partial class MainForm : Form
    {
        public Dictionary<Control, string> ManualTooltips = null;
        public bool Opened = false;

        public void InitManualTooltips()
        {
            ManualTooltips = new Dictionary<Control, string>()
            {
                {textBoxGameDir, "(Required) A Hat in Time's install directory\n\n" + 
                "e.g.: C:\\Program Files (x86)\\Steam\\steamapps\\common\\HatinTime"},
                {textBoxCookedPackage, "(Required) The cooked .u package.\n\n" +
                "For example, in Simple mode, this is: .....\\CookedPC\\Startup.upk"},
                {textBoxScriptPackage, "(Optional, but recommended) The non-cooked, compiled .u package.\n\n" +
                "For example, in Simple mode, this is: .....\\EditorCookedPC\\HatinTimeGameContent.u"}
            };
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load settings at startup.
            textBoxGameDir.Text = Properties.Settings.Default.SavedGameDir;
            textBoxCookedPackage.Text = Properties.Settings.Default.SavedCookedPath;
            textBoxScriptPackage.Text = Properties.Settings.Default.SavedCompiledPath;

#if !DEBUG
            if (DecompressFacade.DoesDecompressorExist())
            {
                panelMain.Visible = true;
            }
            else
#endif
            {
                panelIntro.Visible = true;
            }

            Opened = true;

            // Load the radio button AFTER Opened is enabled.
            switch (Properties.Settings.Default.SavedRadioButtonIndex)
            {
                case 0:
                    radioSimple.Checked = true;
                    break;
                case 1:
                    radioAdvanced.Checked = true;
                    break;
            }

            // Not needed anymore, because the radio changed events already update this automatically.
            //UpdateStartButtonState();

            InitManualTooltips();
        }

        private void labelIntro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DecompressFacade.OpenDecompressorDownloadPageInBrowser();
        }

        private void buttonIntro_Click(object sender, EventArgs e)
        {
            panelIntro.Visible = false;
            panelMain.Visible = true;
        }

        private void radioSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (!Opened) return;
            panelSimple.Enabled = radioSimple.Checked;
            if (radioSimple.Checked) UpdateStartButtonState();
        }

        private void radioAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if (!Opened) return;
            panelAdvanced.Enabled = radioAdvanced.Checked;
            if (radioAdvanced.Checked) UpdateStartButtonState();
        }

        private void browseGameDir_Click(object sender, EventArgs e)
        {
            // The folder browser that comes with .NET Framework 4.8 is... rough, to say the least.
            // It shows folders in a tree stucture and doesn't even let you paste a path anywhere.
            // Therefore I'm using one from a library, that actually looks and functions like the file picker - but for folders.
            using (var dialog = new WK.Libraries.BetterFolderBrowserNS.BetterFolderBrowser())
            {
                // This folder browser has a failsafe in case the given path is blank, but Windows already has a (IMO better) failsafe for this.
                // So use a " " string to bypass the check.
                dialog.RootFolder = textBoxGameDir.Text == "" ? " " : textBoxGameDir.Text;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxGameDir.Text = dialog.SelectedPath;
                }
            }
        }

        private void browseCookedPackage_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = textBoxCookedPackage.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxCookedPackage.Text = dialog.FileName;
                }
            }
        }

        private void browseScriptPackage_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = textBoxScriptPackage.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxScriptPackage.Text = dialog.FileName;
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!Opened) return;
            UpdateStartButtonState();
        }

        public void UpdateStartButtonState()
        {
            bool enabled;
            if (radioSimple.Checked)
            {
                enabled = textBoxGameDir.Text != "";
            }
            else if (radioAdvanced.Checked)
            {
                enabled = textBoxCookedPackage.Text != "";
            }
            else
            {
                enabled = false;
            }
            if (enabled != buttonStart.Enabled)
                buttonStart.Enabled = enabled;
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

        private void buttonStart_Click(object sender, EventArgs e)
        {
            AlwaysLoadedChecker checkerForm = null;

            if (radioSimple.Checked)
            {
                checkerForm = Program.LaunchSimple(textBoxGameDir.Text.Trim(), showWarnings: true, owner: this);
                if (checkerForm == null)
                    return;

                // Save the settings that you used.
                Properties.Settings.Default.SavedRadioButtonIndex = 0;
                Properties.Settings.Default.SavedGameDir = textBoxGameDir.Text;
                Properties.Settings.Default.Save();
            }
            else if (radioAdvanced.Checked)
            {
                checkerForm = Program.LaunchAdvanced(textBoxCookedPackage.Text.Trim(), textBoxScriptPackage.Text.Trim(), showWarnings: true, owner: this);
                if (checkerForm == null)
                    return;

                // Save the settings that you used.
                Properties.Settings.Default.SavedRadioButtonIndex = 1;
                Properties.Settings.Default.SavedCookedPath = textBoxCookedPackage.Text;
                Properties.Settings.Default.SavedCompiledPath = textBoxScriptPackage.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show(
                    "How is there no radio button selected...?",
                    "Uhh...", MessageBoxButtons.OK, MessageBoxIcon.Error
                );

                return;
            }

            checkerForm.StartPosition = FormStartPosition.CenterScreen;
            // Pass this as the owner, so closing the main form auto-closes the checker(s) as well (and runs cleanup code in the process).
            checkerForm.Show(this);
        }
    }
}
