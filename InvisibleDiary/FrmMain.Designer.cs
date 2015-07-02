namespace InvisibleDiary
{
    partial class FrmMain
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
            SingleAssemblyComponentResourceManager resources = new SingleAssemblyComponentResourceManager(typeof(FrmMain));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mergeButton = new System.Windows.Forms.Button();
            this.unlockButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.tagsTextBox = new System.Windows.Forms.TextBox();
            this.writeButton = new System.Windows.Forms.Button();
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.entriesTreeView = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Controls.Add(this.mergeButton);
            this.splitContainer.Panel1.Controls.Add(this.unlockButton);
            this.splitContainer.Panel1.Controls.Add(this.label3);
            this.splitContainer.Panel1.Controls.Add(this.dateLabel);
            this.splitContainer.Panel1.Controls.Add(this.tagsTextBox);
            this.splitContainer.Panel1.Controls.Add(this.writeButton);
            this.splitContainer.Panel1.Controls.Add(this.mainTextBox);
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.Controls.Add(this.calendar);
            this.splitContainer.Panel2.Controls.Add(this.entriesTreeView);
            this.splitContainer.Panel2.Controls.Add(this.label2);
            // 
            // mergeButton
            // 
            resources.ApplyResources(this.mergeButton, "mergeButton");
            this.mergeButton.Name = "mergeButton";
            this.mergeButton.UseVisualStyleBackColor = true;
            this.mergeButton.Click += new System.EventHandler(this.mergeButton_Click);
            // 
            // unlockButton
            // 
            resources.ApplyResources(this.unlockButton, "unlockButton");
            this.unlockButton.Name = "unlockButton";
            this.unlockButton.UseVisualStyleBackColor = true;
            this.unlockButton.Click += new System.EventHandler(this.unlockButton_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dateLabel
            // 
            resources.ApplyResources(this.dateLabel, "dateLabel");
            this.dateLabel.Name = "dateLabel";
            // 
            // tagsTextBox
            // 
            resources.ApplyResources(this.tagsTextBox, "tagsTextBox");
            this.tagsTextBox.Name = "tagsTextBox";
            // 
            // writeButton
            // 
            resources.ApplyResources(this.writeButton, "writeButton");
            this.writeButton.Name = "writeButton";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // mainTextBox
            // 
            resources.ApplyResources(this.mainTextBox, "mainTextBox");
            this.mainTextBox.Name = "mainTextBox";
            // 
            // calendar
            // 
            resources.ApplyResources(this.calendar, "calendar");
            this.calendar.Name = "calendar";
            // 
            // entriesTreeView
            // 
            resources.ApplyResources(this.entriesTreeView, "entriesTreeView");
            this.entriesTreeView.Name = "entriesTreeView";
            this.entriesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.entriesTreeView_AfterSelect);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFontToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // changeFontToolStripMenuItem
            // 
            resources.ApplyResources(this.changeFontToolStripMenuItem, "changeFontToolStripMenuItem");
            this.changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.splitContainer);
            this.Name = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button unlockButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.TextBox tagsTextBox;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.TextBox mainTextBox;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.TreeView entriesTreeView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeFontToolStripMenuItem;
        private System.Windows.Forms.Button mergeButton;


    }
}

