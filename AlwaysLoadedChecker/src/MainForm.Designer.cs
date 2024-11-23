namespace AlwaysLoadedChecker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.radioSimple = new System.Windows.Forms.RadioButton();
            this.panelSimple = new System.Windows.Forms.Panel();
            this.labelGameDir = new System.Windows.Forms.Label();
            this.textBoxGameDir = new System.Windows.Forms.TextBox();
            this.browseGameDir = new System.Windows.Forms.Button();
            this.radioAdvanced = new System.Windows.Forms.RadioButton();
            this.panelAdvanced = new System.Windows.Forms.Panel();
            this.labelCookedPackage = new System.Windows.Forms.Label();
            this.textBoxCookedPackage = new System.Windows.Forms.TextBox();
            this.browseCookedPackage = new System.Windows.Forms.Button();
            this.labelScriptPackage = new System.Windows.Forms.Label();
            this.textBoxScriptPackage = new System.Windows.Forms.TextBox();
            this.browseScriptPackage = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panelIntro = new System.Windows.Forms.Panel();
            this.labelIntro = new System.Windows.Forms.LinkLabel();
            this.buttonIntro = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelNoteAsterix = new System.Windows.Forms.Label();
            this.labelAsterix = new System.Windows.Forms.Label();
            this.labelAsterixGameDir = new System.Windows.Forms.Label();
            this.labelAsterixCookedPackage = new System.Windows.Forms.Label();
            this.groupBoxSettings.SuspendLayout();
            this.panelSimple.SuspendLayout();
            this.panelAdvanced.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelIntro.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelAsterix);
            this.groupBoxSettings.Controls.Add(this.labelNoteAsterix);
            this.groupBoxSettings.Controls.Add(this.radioSimple);
            this.groupBoxSettings.Controls.Add(this.panelSimple);
            this.groupBoxSettings.Controls.Add(this.radioAdvanced);
            this.groupBoxSettings.Controls.Add(this.panelAdvanced);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSettings.Location = new System.Drawing.Point(8, 8);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(528, 195);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // radioSimple
            // 
            this.radioSimple.AutoSize = true;
            this.radioSimple.Location = new System.Drawing.Point(6, 19);
            this.radioSimple.Name = "radioSimple";
            this.radioSimple.Size = new System.Drawing.Size(56, 17);
            this.radioSimple.TabIndex = 6;
            this.radioSimple.Text = "Simple";
            this.toolTip.SetToolTip(this.radioSimple, "For the given game directory, check the classes from HatinTimeGameContent.");
            this.radioSimple.UseVisualStyleBackColor = true;
            this.radioSimple.CheckedChanged += new System.EventHandler(this.radioSimple_CheckedChanged);
            // 
            // panelSimple
            // 
            this.panelSimple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSimple.Controls.Add(this.labelGameDir);
            this.panelSimple.Controls.Add(this.labelAsterixGameDir);
            this.panelSimple.Controls.Add(this.textBoxGameDir);
            this.panelSimple.Controls.Add(this.browseGameDir);
            this.panelSimple.Enabled = false;
            this.panelSimple.Location = new System.Drawing.Point(27, 42);
            this.panelSimple.Margin = new System.Windows.Forms.Padding(24, 3, 0, 3);
            this.panelSimple.Name = "panelSimple";
            this.panelSimple.Padding = new System.Windows.Forms.Padding(4);
            this.panelSimple.Size = new System.Drawing.Size(498, 39);
            this.panelSimple.TabIndex = 7;
            // 
            // labelGameDir
            // 
            this.labelGameDir.AutoSize = true;
            this.labelGameDir.Location = new System.Drawing.Point(4, 7);
            this.labelGameDir.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelGameDir.Name = "labelGameDir";
            this.labelGameDir.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelGameDir.Size = new System.Drawing.Size(80, 23);
            this.labelGameDir.TabIndex = 0;
            this.labelGameDir.Text = "Game Directory";
            this.labelGameDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxGameDir
            // 
            this.textBoxGameDir.Location = new System.Drawing.Point(138, 9);
            this.textBoxGameDir.Name = "textBoxGameDir";
            this.textBoxGameDir.Size = new System.Drawing.Size(270, 20);
            this.textBoxGameDir.TabIndex = 1;
            this.textBoxGameDir.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxGameDir.MouseEnter += new System.EventHandler(this.manualTooltipControl_MouseEnter);
            this.textBoxGameDir.MouseLeave += new System.EventHandler(this.manualTooltipControl_MouseLeave);
            // 
            // browseGameDir
            // 
            this.browseGameDir.Location = new System.Drawing.Point(414, 7);
            this.browseGameDir.Name = "browseGameDir";
            this.browseGameDir.Size = new System.Drawing.Size(75, 23);
            this.browseGameDir.TabIndex = 2;
            this.browseGameDir.Text = "Browse";
            this.browseGameDir.UseVisualStyleBackColor = true;
            this.browseGameDir.Click += new System.EventHandler(this.browseGameDir_Click);
            // 
            // radioAdvanced
            // 
            this.radioAdvanced.AutoSize = true;
            this.radioAdvanced.Location = new System.Drawing.Point(6, 87);
            this.radioAdvanced.Name = "radioAdvanced";
            this.radioAdvanced.Size = new System.Drawing.Size(74, 17);
            this.radioAdvanced.TabIndex = 8;
            this.radioAdvanced.TabStop = true;
            this.radioAdvanced.Text = "Advanced";
            this.toolTip.SetToolTip(this.radioAdvanced, "Check the classes from a custom script package and its cooked counterpart.");
            this.radioAdvanced.UseVisualStyleBackColor = true;
            this.radioAdvanced.CheckedChanged += new System.EventHandler(this.radioAdvanced_CheckedChanged);
            // 
            // panelAdvanced
            // 
            this.panelAdvanced.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAdvanced.Controls.Add(this.labelCookedPackage);
            this.panelAdvanced.Controls.Add(this.labelAsterixCookedPackage);
            this.panelAdvanced.Controls.Add(this.textBoxCookedPackage);
            this.panelAdvanced.Controls.Add(this.browseCookedPackage);
            this.panelAdvanced.Controls.Add(this.labelScriptPackage);
            this.panelAdvanced.Controls.Add(this.textBoxScriptPackage);
            this.panelAdvanced.Controls.Add(this.browseScriptPackage);
            this.panelAdvanced.Enabled = false;
            this.panelAdvanced.Location = new System.Drawing.Point(27, 110);
            this.panelAdvanced.Margin = new System.Windows.Forms.Padding(24, 3, 0, 3);
            this.panelAdvanced.Name = "panelAdvanced";
            this.panelAdvanced.Padding = new System.Windows.Forms.Padding(4);
            this.panelAdvanced.Size = new System.Drawing.Size(498, 68);
            this.panelAdvanced.TabIndex = 9;
            // 
            // labelCookedPackage
            // 
            this.labelCookedPackage.AutoSize = true;
            this.labelCookedPackage.Location = new System.Drawing.Point(4, 7);
            this.labelCookedPackage.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelCookedPackage.Name = "labelCookedPackage";
            this.labelCookedPackage.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelCookedPackage.Size = new System.Drawing.Size(90, 23);
            this.labelCookedPackage.TabIndex = 3;
            this.labelCookedPackage.Text = "Cooked Package";
            this.labelCookedPackage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxCookedPackage
            // 
            this.textBoxCookedPackage.Location = new System.Drawing.Point(138, 9);
            this.textBoxCookedPackage.Name = "textBoxCookedPackage";
            this.textBoxCookedPackage.Size = new System.Drawing.Size(270, 20);
            this.textBoxCookedPackage.TabIndex = 4;
            this.textBoxCookedPackage.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxCookedPackage.MouseEnter += new System.EventHandler(this.manualTooltipControl_MouseEnter);
            this.textBoxCookedPackage.MouseLeave += new System.EventHandler(this.manualTooltipControl_MouseLeave);
            // 
            // browseCookedPackage
            // 
            this.browseCookedPackage.Location = new System.Drawing.Point(414, 7);
            this.browseCookedPackage.Name = "browseCookedPackage";
            this.browseCookedPackage.Size = new System.Drawing.Size(75, 23);
            this.browseCookedPackage.TabIndex = 5;
            this.browseCookedPackage.Text = "Browse";
            this.browseCookedPackage.UseVisualStyleBackColor = true;
            this.browseCookedPackage.Click += new System.EventHandler(this.browseCookedPackage_Click);
            // 
            // labelScriptPackage
            // 
            this.labelScriptPackage.AutoSize = true;
            this.labelScriptPackage.Location = new System.Drawing.Point(4, 36);
            this.labelScriptPackage.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelScriptPackage.Name = "labelScriptPackage";
            this.labelScriptPackage.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelScriptPackage.Size = new System.Drawing.Size(131, 23);
            this.labelScriptPackage.TabIndex = 0;
            this.labelScriptPackage.Text = "Compiled Scripts Package";
            this.labelScriptPackage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxScriptPackage
            // 
            this.textBoxScriptPackage.Location = new System.Drawing.Point(138, 38);
            this.textBoxScriptPackage.Name = "textBoxScriptPackage";
            this.textBoxScriptPackage.Size = new System.Drawing.Size(270, 20);
            this.textBoxScriptPackage.TabIndex = 1;
            this.textBoxScriptPackage.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxScriptPackage.MouseEnter += new System.EventHandler(this.manualTooltipControl_MouseEnter);
            this.textBoxScriptPackage.MouseLeave += new System.EventHandler(this.manualTooltipControl_MouseLeave);
            // 
            // browseScriptPackage
            // 
            this.browseScriptPackage.Location = new System.Drawing.Point(414, 36);
            this.browseScriptPackage.Name = "browseScriptPackage";
            this.browseScriptPackage.Size = new System.Drawing.Size(75, 23);
            this.browseScriptPackage.TabIndex = 2;
            this.browseScriptPackage.Text = "Browse";
            this.browseScriptPackage.UseVisualStyleBackColor = true;
            this.browseScriptPackage.Click += new System.EventHandler(this.browseScriptPackage_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBoxSettings);
            this.panelMain.Controls.Add(this.buttonStart);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(8);
            this.panelMain.Size = new System.Drawing.Size(544, 261);
            this.panelMain.TabIndex = 2;
            this.panelMain.Visible = false;
            // 
            // buttonStart
            // 
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonStart.Location = new System.Drawing.Point(8, 203);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(528, 50);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "START CHECKER!";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // panelIntro
            // 
            this.panelIntro.Controls.Add(this.labelIntro);
            this.panelIntro.Controls.Add(this.buttonIntro);
            this.panelIntro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIntro.Location = new System.Drawing.Point(0, 0);
            this.panelIntro.Name = "panelIntro";
            this.panelIntro.Padding = new System.Windows.Forms.Padding(8);
            this.panelIntro.Size = new System.Drawing.Size(544, 261);
            this.panelIntro.TabIndex = 3;
            this.panelIntro.Visible = false;
            // 
            // labelIntro
            // 
            this.labelIntro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelIntro.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIntro.LinkArea = new System.Windows.Forms.LinkArea(149, 29);
            this.labelIntro.Location = new System.Drawing.Point(8, 8);
            this.labelIntro.Name = "labelIntro";
            this.labelIntro.Padding = new System.Windows.Forms.Padding(26, 0, 26, 0);
            this.labelIntro.Size = new System.Drawing.Size(528, 195);
            this.labelIntro.TabIndex = 0;
            this.labelIntro.TabStop = true;
            this.labelIntro.Text = resources.GetString("labelIntro.Text");
            this.labelIntro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelIntro.UseCompatibleTextRendering = true;
            this.labelIntro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelIntro_LinkClicked);
            // 
            // buttonIntro
            // 
            this.buttonIntro.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonIntro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIntro.Location = new System.Drawing.Point(8, 203);
            this.buttonIntro.Name = "buttonIntro";
            this.buttonIntro.Size = new System.Drawing.Size(528, 50);
            this.buttonIntro.TabIndex = 1;
            this.buttonIntro.Text = "Gotcha!";
            this.buttonIntro.UseVisualStyleBackColor = true;
            this.buttonIntro.Click += new System.EventHandler(this.buttonIntro_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // labelNoteAsterix
            // 
            this.labelNoteAsterix.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelNoteAsterix.Location = new System.Drawing.Point(354, 16);
            this.labelNoteAsterix.Margin = new System.Windows.Forms.Padding(0);
            this.labelNoteAsterix.Name = "labelNoteAsterix";
            this.labelNoteAsterix.Size = new System.Drawing.Size(155, 16);
            this.labelNoteAsterix.TabIndex = 10;
            this.labelNoteAsterix.Text = "Required fields are marked with";
            this.labelNoteAsterix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAsterix
            // 
            this.labelAsterix.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterix.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelAsterix.Location = new System.Drawing.Point(509, 16);
            this.labelAsterix.Margin = new System.Windows.Forms.Padding(0);
            this.labelAsterix.Name = "labelAsterix";
            this.labelAsterix.Size = new System.Drawing.Size(16, 13);
            this.labelAsterix.TabIndex = 11;
            this.labelAsterix.Text = "*";
            this.labelAsterix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAsterixGameDir
            // 
            this.labelAsterixGameDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterixGameDir.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelAsterixGameDir.Location = new System.Drawing.Point(84, 9);
            this.labelAsterixGameDir.Margin = new System.Windows.Forms.Padding(0);
            this.labelAsterixGameDir.Name = "labelAsterixGameDir";
            this.labelAsterixGameDir.Size = new System.Drawing.Size(16, 13);
            this.labelAsterixGameDir.TabIndex = 12;
            this.labelAsterixGameDir.Text = "*";
            this.labelAsterixGameDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAsterixCookedPackage
            // 
            this.labelAsterixCookedPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterixCookedPackage.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelAsterixCookedPackage.Location = new System.Drawing.Point(94, 9);
            this.labelAsterixCookedPackage.Margin = new System.Windows.Forms.Padding(0);
            this.labelAsterixCookedPackage.Name = "labelAsterixCookedPackage";
            this.labelAsterixCookedPackage.Size = new System.Drawing.Size(16, 13);
            this.labelAsterixCookedPackage.TabIndex = 13;
            this.labelAsterixCookedPackage.Text = "*";
            this.labelAsterixCookedPackage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 261);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelIntro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "The unofficial A Hat in Time AlwaysLoaded Checker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.panelSimple.ResumeLayout(false);
            this.panelSimple.PerformLayout();
            this.panelAdvanced.ResumeLayout(false);
            this.panelAdvanced.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelIntro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIntro;
        private System.Windows.Forms.LinkLabel labelIntro;
        private System.Windows.Forms.Button buttonIntro;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.RadioButton radioSimple;
        private System.Windows.Forms.Panel panelSimple;
        private System.Windows.Forms.Label labelGameDir;
        private System.Windows.Forms.TextBox textBoxGameDir;
        private System.Windows.Forms.Button browseGameDir;
        private System.Windows.Forms.RadioButton radioAdvanced;
        private System.Windows.Forms.Panel panelAdvanced;
        private System.Windows.Forms.Label labelCookedPackage;
        private System.Windows.Forms.TextBox textBoxCookedPackage;
        private System.Windows.Forms.Button browseCookedPackage;
        private System.Windows.Forms.Label labelScriptPackage;
        private System.Windows.Forms.TextBox textBoxScriptPackage;
        private System.Windows.Forms.Button browseScriptPackage;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelNoteAsterix;
        private System.Windows.Forms.Label labelAsterix;
        private System.Windows.Forms.Label labelAsterixGameDir;
        private System.Windows.Forms.Label labelAsterixCookedPackage;
    }
}