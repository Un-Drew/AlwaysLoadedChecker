namespace ModdingTools.Windows.Tools
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
            this.classTextBox = new CUFramework.Controls.CUTextBox();
            this.checkButton = new CUFramework.Controls.CUButton();
            this.resultLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.loadingLabel = new System.Windows.Forms.Label();
            this.theMenu = new System.Windows.Forms.Panel();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.loadingProgress = new CUFramework.Controls.CUProgressBar();
            this.theMenu.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // title1
            // 
            this.title1.Dock = System.Windows.Forms.DockStyle.Top;
            this.title1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title1.Location = new System.Drawing.Point(0, 0);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(396, 34);
            this.title1.TabIndex = 1;
            this.title1.Text = "Is THIS class";
            this.title1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // title2
            // 
            this.title2.Dock = System.Windows.Forms.DockStyle.Top;
            this.title2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.title2.Location = new System.Drawing.Point(0, 34);
            this.title2.Name = "title2";
            this.title2.Size = new System.Drawing.Size(396, 50);
            this.title2.TabIndex = 2;
            this.title2.Text = "AlwaysLoaded?";
            this.title2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // classTextBox
            // 
            this.classTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.classTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.classTextBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classTextBox.ForeColor = System.Drawing.Color.White;
            this.classTextBox.Location = new System.Drawing.Point(50, 90);
            this.classTextBox.Name = "classTextBox";
            this.classTextBox.Size = new System.Drawing.Size(300, 26);
            this.classTextBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.classTextBox, "Type the name of any class from HatinTimeGameContent.");
            this.classTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.classTextBox_KeyUp);
            // 
            // checkButton
            // 
            this.checkButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.checkButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.checkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkButton.ForeColor = System.Drawing.Color.White;
            this.checkButton.Location = new System.Drawing.Point(100, 126);
            this.checkButton.Name = "checkButton";
            this.checkButton.NoFocus = false;
            this.checkButton.Size = new System.Drawing.Size(200, 36);
            this.checkButton.TabIndex = 4;
            this.checkButton.Text = "GO";
            this.checkButton.UseVisualStyleBackColor = false;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(0, 166);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(396, 68);
            this.resultLabel.TabIndex = 5;
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadingLabel
            // 
            this.loadingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLabel.Location = new System.Drawing.Point(5, 5);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(386, 194);
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
            this.theMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theMenu.Location = new System.Drawing.Point(2, 34);
            this.theMenu.Name = "theMenu";
            this.theMenu.Size = new System.Drawing.Size(396, 234);
            this.theMenu.TabIndex = 7;
            this.theMenu.Visible = false;
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.loadingLabel);
            this.loadingPanel.Controls.Add(this.loadingProgress);
            this.loadingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingPanel.Location = new System.Drawing.Point(2, 34);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Padding = new System.Windows.Forms.Padding(5);
            this.loadingPanel.Size = new System.Drawing.Size(396, 234);
            this.loadingPanel.TabIndex = 6;
            // 
            // loadingProgress
            // 
            this.loadingProgress.BorderSize = 2;
            this.loadingProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadingProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingProgress.ForeColor = System.Drawing.Color.White;
            this.loadingProgress.Location = new System.Drawing.Point(5, 199);
            this.loadingProgress.MaxValue = 100F;
            this.loadingProgress.Name = "loadingProgress";
            this.loadingProgress.ProgressBarColor = System.Drawing.Color.Purple;
            this.loadingProgress.Size = new System.Drawing.Size(386, 30);
            this.loadingProgress.TabIndex = 7;
            this.loadingProgress.Text = "0%";
            this.loadingProgress.Value = 0F;
            // 
            // AlwaysLoadedChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 270);
            this.Controls.Add(this.loadingPanel);
            this.Controls.Add(this.theMenu);
            this.IsMaximizeButtonEnabled = false;
            this.IsResizable = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AlwaysLoadedChecker";
            this.Text = "ALWAYSLOADED CHECKER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlwaysLoadedChecker_FormClosing);
            this.Shown += new System.EventHandler(this.AlwaysLoadedChecker_Shown);
            this.Controls.SetChildIndex(this.theMenu, 0);
            this.Controls.SetChildIndex(this.loadingPanel, 0);
            this.theMenu.ResumeLayout(false);
            this.theMenu.PerformLayout();
            this.loadingPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title1;
        private System.Windows.Forms.Label title2;
        private CUFramework.Controls.CUTextBox classTextBox;
        private CUFramework.Controls.CUButton checkButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.Panel theMenu;
        private System.Windows.Forms.Panel loadingPanel;
        private CUFramework.Controls.CUProgressBar loadingProgress;
    }
}