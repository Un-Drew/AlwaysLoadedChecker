namespace AlwaysLoadedChecker
{
    partial class AlwaysLoadedChecker
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
            this.title1 = new System.Windows.Forms.Label();
            this.title2 = new System.Windows.Forms.Label();
            this.classTextBox = new System.Windows.Forms.TextBox();
            this.checkButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.loadingLabel = new System.Windows.Forms.Label();
            this.theMenu = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToTextFileAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            this.labelLoadingProgress = new System.Windows.Forms.Label();
            this.panelLoadingOuter = new System.Windows.Forms.Panel();
            this.panelLoadingInner = new System.Windows.Forms.Panel();
            this.theMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            this.panelLoadingOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // title1
            // 
            this.title1.Dock = System.Windows.Forms.DockStyle.Top;
            this.title1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title1.Location = new System.Drawing.Point(0, 24);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(400, 34);
            this.title1.TabIndex = 1;
            this.title1.Text = "Is THIS class";
            this.title1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // title2
            // 
            this.title2.Dock = System.Windows.Forms.DockStyle.Top;
            this.title2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title2.ForeColor = System.Drawing.Color.Green;
            this.title2.Location = new System.Drawing.Point(0, 58);
            this.title2.Name = "title2";
            this.title2.Size = new System.Drawing.Size(400, 50);
            this.title2.TabIndex = 2;
            this.title2.Text = "AlwaysLoaded?";
            this.title2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // classTextBox
            // 
            this.classTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classTextBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classTextBox.Location = new System.Drawing.Point(50, 114);
            this.classTextBox.Name = "classTextBox";
            this.classTextBox.Size = new System.Drawing.Size(304, 26);
            this.classTextBox.TabIndex = 3;
            this.classTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.classTextBox_KeyDown);
            this.classTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.classTextBox_KeyUp);
            this.classTextBox.Leave += new System.EventHandler(this.classTextBox_Leave);
            this.classTextBox.MouseEnter += new System.EventHandler(this.manualTooltipControl_MouseEnter);
            this.classTextBox.MouseLeave += new System.EventHandler(this.manualTooltipControl_MouseLeave);
            // 
            // checkButton
            // 
            this.checkButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkButton.Location = new System.Drawing.Point(100, 150);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(200, 36);
            this.checkButton.TabIndex = 4;
            this.checkButton.Text = "GO";
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(0, 192);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(400, 78);
            this.resultLabel.TabIndex = 5;
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadingLabel
            // 
            this.loadingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLabel.Location = new System.Drawing.Point(5, 5);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(390, 210);
            this.loadingLabel.TabIndex = 6;
            this.loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // theMenu
            // 
            this.theMenu.Controls.Add(this.resultLabel);
            this.theMenu.Controls.Add(this.checkButton);
            this.theMenu.Controls.Add(this.classTextBox);
            this.theMenu.Controls.Add(this.title2);
            this.theMenu.Controls.Add(this.title1);
            this.theMenu.Controls.Add(this.menuStrip);
            this.theMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theMenu.Location = new System.Drawing.Point(0, 0);
            this.theMenu.Name = "theMenu";
            this.theMenu.Size = new System.Drawing.Size(400, 270);
            this.theMenu.TabIndex = 7;
            this.theMenu.Visible = false;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extraToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(400, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToTextFileToolStripMenuItem});
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.extraToolStripMenuItem.Text = "Extra Tools";
            // 
            // logToTextFileToolStripMenuItem
            // 
            this.logToTextFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToTextFileAllToolStripMenuItem,
            this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem,
            this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem});
            this.logToTextFileToolStripMenuItem.Name = "logToTextFileToolStripMenuItem";
            this.logToTextFileToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.logToTextFileToolStripMenuItem.Text = "Log classes to text file";
            // 
            // logToTextFileAllToolStripMenuItem
            // 
            this.logToTextFileAllToolStripMenuItem.Name = "logToTextFileAllToolStripMenuItem";
            this.logToTextFileAllToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.logToTextFileAllToolStripMenuItem.Text = "All classes";
            this.logToTextFileAllToolStripMenuItem.Click += new System.EventHandler(this.logToTextFileAllToolStripMenuItem_Click);
            // 
            // logToTextFileAlwaysLoadedOnlyToolStripMenuItem
            // 
            this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem.Name = "logToTextFileAlwaysLoadedOnlyToolStripMenuItem";
            this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem.Text = "AlwaysLoaded classes only";
            this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem.Click += new System.EventHandler(this.logToTextFileAlwaysLoadedOnlyToolStripMenuItem_Click);
            // 
            // logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem
            // 
            this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem.Name = "logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem";
            this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem.Text = "Non-AlwaysLoaded classes only";
            this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem.Click += new System.EventHandler(this.logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem_Click);
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.loadingLabel);
            this.loadingPanel.Controls.Add(this.labelLoadingProgress);
            this.loadingPanel.Controls.Add(this.panelLoadingOuter);
            this.loadingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingPanel.Location = new System.Drawing.Point(0, 0);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Padding = new System.Windows.Forms.Padding(5);
            this.loadingPanel.Size = new System.Drawing.Size(400, 270);
            this.loadingPanel.TabIndex = 6;
            this.loadingPanel.Visible = false;
            // 
            // timerFlash
            // 
            this.timerFlash.Interval = 150;
            this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
            // 
            // labelLoadingProgress
            // 
            this.labelLoadingProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLoadingProgress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoadingProgress.Location = new System.Drawing.Point(5, 215);
            this.labelLoadingProgress.Name = "labelLoadingProgress";
            this.labelLoadingProgress.Size = new System.Drawing.Size(390, 20);
            this.labelLoadingProgress.TabIndex = 8;
            this.labelLoadingProgress.Text = "0%";
            this.labelLoadingProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLoadingOuter
            // 
            this.panelLoadingOuter.BackColor = System.Drawing.Color.White;
            this.panelLoadingOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLoadingOuter.Controls.Add(this.panelLoadingInner);
            this.panelLoadingOuter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLoadingOuter.Location = new System.Drawing.Point(5, 235);
            this.panelLoadingOuter.Name = "panelLoadingOuter";
            this.panelLoadingOuter.Size = new System.Drawing.Size(390, 30);
            this.panelLoadingOuter.TabIndex = 9;
            // 
            // panelLoadingInner
            // 
            this.panelLoadingInner.BackColor = System.Drawing.Color.LightGreen;
            this.panelLoadingInner.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLoadingInner.Location = new System.Drawing.Point(0, 0);
            this.panelLoadingInner.Margin = new System.Windows.Forms.Padding(0);
            this.panelLoadingInner.Name = "panelLoadingInner";
            this.panelLoadingInner.Size = new System.Drawing.Size(100, 28);
            this.panelLoadingInner.TabIndex = 10;
            // 
            // AlwaysLoadedChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 270);
            this.Controls.Add(this.theMenu);
            this.Controls.Add(this.loadingPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "AlwaysLoadedChecker";
            this.Text = "Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlwaysLoadedChecker_FormClosing);
            this.Shown += new System.EventHandler(this.AlwaysLoadedChecker_Shown);
            this.theMenu.ResumeLayout(false);
            this.theMenu.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.loadingPanel.ResumeLayout(false);
            this.panelLoadingOuter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title1;
        private System.Windows.Forms.Label title2;
        private System.Windows.Forms.TextBox classTextBox;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.Panel theMenu;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToTextFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToTextFileAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToTextFileAlwaysLoadedOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToTextFileNonAlwaysLoadedOnlyToolStripMenuItem;
        private System.Windows.Forms.Timer timerFlash;
        private System.Windows.Forms.Label labelLoadingProgress;
        private System.Windows.Forms.Panel panelLoadingOuter;
        private System.Windows.Forms.Panel panelLoadingInner;
    }
}