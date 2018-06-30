namespace FinalProject_MHTTUD
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.accountListBox = new System.Windows.Forms.ListBox();
            this.accountLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.filesDirectoriesTreeView = new System.Windows.Forms.TreeView();
            this.filesDirectoriesLabel = new System.Windows.Forms.Label();
            this.browseProgressBar = new System.Windows.Forms.ProgressBar();
            this.compressCheckBox = new System.Windows.Forms.CheckBox();
            this.encryptButton = new System.Windows.Forms.Button();
            this.decryptButton = new System.Windows.Forms.Button();
            this.signButton = new System.Windows.Forms.Button();
            this.verifyButton = new System.Windows.Forms.Button();
            this.compressOneFileCheckBox = new System.Windows.Forms.CheckBox();
            this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.accountToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.userToolStripLabel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(509, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.ShowShortcutKeys = false;
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountImportToolStripMenuItem,
            this.databaseImportToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShowShortcutKeys = false;
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "I&mport...";
            // 
            // accountImportToolStripMenuItem
            // 
            this.accountImportToolStripMenuItem.Name = "accountImportToolStripMenuItem";
            this.accountImportToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.accountImportToolStripMenuItem.Text = "&Account";
            this.accountImportToolStripMenuItem.Click += new System.EventHandler(this.accountImportToolStripMenuItem_Click);
            // 
            // databaseImportToolStripMenuItem
            // 
            this.databaseImportToolStripMenuItem.Name = "databaseImportToolStripMenuItem";
            this.databaseImportToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.databaseImportToolStripMenuItem.Text = "&Database";
            this.databaseImportToolStripMenuItem.Click += new System.EventHandler(this.databaseImportToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountExportToolStripMenuItem,
            this.databaseExportToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShowShortcutKeys = false;
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "E&xport...";
            // 
            // accountExportToolStripMenuItem
            // 
            this.accountExportToolStripMenuItem.Name = "accountExportToolStripMenuItem";
            this.accountExportToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.accountExportToolStripMenuItem.Text = "&Account";
            this.accountExportToolStripMenuItem.Click += new System.EventHandler(this.accountExportToolStripMenuItem_Click);
            // 
            // databaseExportToolStripMenuItem
            // 
            this.databaseExportToolStripMenuItem.Name = "databaseExportToolStripMenuItem";
            this.databaseExportToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.databaseExportToolStripMenuItem.Text = "&Database";
            this.databaseExportToolStripMenuItem.Click += new System.EventHandler(this.databaseExportToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signInToolStripMenuItem,
            this.registerToolStripMenuItem,
            this.toolStripSeparator1,
            this.editInfoToolStripMenuItem,
            this.toolStripSeparator2,
            this.signOutToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.accountToolStripMenuItem.ShowShortcutKeys = false;
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.accountToolStripMenuItem.Text = "A&ccount";
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            this.signInToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.signInToolStripMenuItem.ShowShortcutKeys = false;
            this.signInToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.signInToolStripMenuItem.Text = "Sign &In";
            this.signInToolStripMenuItem.Click += new System.EventHandler(this.signInToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.registerToolStripMenuItem.ShowShortcutKeys = false;
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.registerToolStripMenuItem.Text = "&Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // editInfoToolStripMenuItem
            // 
            this.editInfoToolStripMenuItem.Name = "editInfoToolStripMenuItem";
            this.editInfoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.editInfoToolStripMenuItem.ShowShortcutKeys = false;
            this.editInfoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editInfoToolStripMenuItem.Text = "&Edit Information";
            this.editInfoToolStripMenuItem.Click += new System.EventHandler(this.editInfoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.signOutToolStripMenuItem.ShowShortcutKeys = false;
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.signOutToolStripMenuItem.Text = "Sign &Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contactToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem.ShowShortcutKeys = false;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // userToolStripLabel
            // 
            this.userToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.userToolStripLabel.Name = "userToolStripLabel";
            this.userToolStripLabel.Size = new System.Drawing.Size(35, 17);
            this.userToolStripLabel.Text = "Guest";
            // 
            // accountListBox
            // 
            this.accountListBox.FormattingEnabled = true;
            this.accountListBox.Location = new System.Drawing.Point(338, 96);
            this.accountListBox.Name = "accountListBox";
            this.accountListBox.Size = new System.Drawing.Size(157, 108);
            this.accountListBox.Sorted = true;
            this.accountListBox.TabIndex = 3;
            // 
            // accountLabel
            // 
            this.accountLabel.AutoSize = true;
            this.accountLabel.Location = new System.Drawing.Point(448, 80);
            this.accountLabel.Name = "accountLabel";
            this.accountLabel.Size = new System.Drawing.Size(47, 13);
            this.accountLabel.TabIndex = 14;
            this.accountLabel.Text = "Account";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 30);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(29, 13);
            this.pathLabel.TabIndex = 10;
            this.pathLabel.Text = "Path";
            // 
            // pathTextBox
            // 
            this.pathTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.pathTextBox.Location = new System.Drawing.Point(47, 27);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(367, 20);
            this.pathTextBox.TabIndex = 11;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(420, 27);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // filesDirectoriesTreeView
            // 
            this.filesDirectoriesTreeView.CheckBoxes = true;
            this.filesDirectoriesTreeView.Location = new System.Drawing.Point(12, 96);
            this.filesDirectoriesTreeView.Name = "filesDirectoriesTreeView";
            this.filesDirectoriesTreeView.Size = new System.Drawing.Size(320, 212);
            this.filesDirectoriesTreeView.TabIndex = 2;
            this.filesDirectoriesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.filesDirectoriesTreeView_NodeMouseClick);
            this.filesDirectoriesTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.filesDirectoriesTreeView_MouseDown);
            // 
            // filesDirectoriesLabel
            // 
            this.filesDirectoriesLabel.AutoSize = true;
            this.filesDirectoriesLabel.Location = new System.Drawing.Point(12, 80);
            this.filesDirectoriesLabel.Name = "filesDirectoriesLabel";
            this.filesDirectoriesLabel.Size = new System.Drawing.Size(83, 13);
            this.filesDirectoriesLabel.TabIndex = 13;
            this.filesDirectoriesLabel.Text = "Files/Directories";
            // 
            // browseProgressBar
            // 
            this.browseProgressBar.Location = new System.Drawing.Point(12, 54);
            this.browseProgressBar.Name = "browseProgressBar";
            this.browseProgressBar.Size = new System.Drawing.Size(483, 23);
            this.browseProgressBar.TabIndex = 12;
            // 
            // compressCheckBox
            // 
            this.compressCheckBox.AutoSize = true;
            this.compressCheckBox.Location = new System.Drawing.Point(338, 210);
            this.compressCheckBox.Name = "compressCheckBox";
            this.compressCheckBox.Size = new System.Drawing.Size(126, 17);
            this.compressCheckBox.TabIndex = 4;
            this.compressCheckBox.Text = "Compress Mode (Zip)";
            this.compressCheckBox.UseVisualStyleBackColor = true;
            this.compressCheckBox.CheckedChanged += new System.EventHandler(this.compressCheckBox_CheckedChanged);
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(338, 257);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 6;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(420, 258);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 7;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // signButton
            // 
            this.signButton.Location = new System.Drawing.Point(338, 288);
            this.signButton.Name = "signButton";
            this.signButton.Size = new System.Drawing.Size(75, 23);
            this.signButton.TabIndex = 8;
            this.signButton.Text = "Sign";
            this.signButton.UseVisualStyleBackColor = true;
            this.signButton.Click += new System.EventHandler(this.signButton_Click);
            // 
            // verifyButton
            // 
            this.verifyButton.Location = new System.Drawing.Point(420, 288);
            this.verifyButton.Name = "verifyButton";
            this.verifyButton.Size = new System.Drawing.Size(75, 23);
            this.verifyButton.TabIndex = 9;
            this.verifyButton.Text = "Verify";
            this.verifyButton.UseVisualStyleBackColor = true;
            this.verifyButton.Click += new System.EventHandler(this.verifyButton_Click);
            // 
            // compressOneFileCheckBox
            // 
            this.compressOneFileCheckBox.AutoSize = true;
            this.compressOneFileCheckBox.Location = new System.Drawing.Point(338, 234);
            this.compressOneFileCheckBox.Name = "compressOneFileCheckBox";
            this.compressOneFileCheckBox.Size = new System.Drawing.Size(130, 17);
            this.compressOneFileCheckBox.TabIndex = 5;
            this.compressOneFileCheckBox.Text = "Compress To One File";
            this.compressOneFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // contactToolStripMenuItem
            // 
            this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
            this.contactToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.contactToolStripMenuItem.ShowShortcutKeys = false;
            this.contactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.contactToolStripMenuItem.Text = "&Contact";
            this.contactToolStripMenuItem.Click += new System.EventHandler(this.contactToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 321);
            this.Controls.Add(this.compressOneFileCheckBox);
            this.Controls.Add(this.verifyButton);
            this.Controls.Add(this.signButton);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.compressCheckBox);
            this.Controls.Add(this.browseProgressBar);
            this.Controls.Add(this.filesDirectoriesLabel);
            this.Controls.Add(this.filesDirectoriesTreeView);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.accountLabel);
            this.Controls.Add(this.accountListBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "FinalProject_MHTTUD";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel userToolStripLabel;
        private System.Windows.Forms.ToolStripMenuItem editInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseExportToolStripMenuItem;
        private System.Windows.Forms.ListBox accountListBox;
        private System.Windows.Forms.Label accountLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TreeView filesDirectoriesTreeView;
        private System.Windows.Forms.Label filesDirectoriesLabel;
        private System.Windows.Forms.ProgressBar browseProgressBar;
        private System.Windows.Forms.CheckBox compressCheckBox;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Button signButton;
        private System.Windows.Forms.Button verifyButton;
        private System.Windows.Forms.CheckBox compressOneFileCheckBox;
        private System.Windows.Forms.ToolStripMenuItem contactToolStripMenuItem;
    }
}

