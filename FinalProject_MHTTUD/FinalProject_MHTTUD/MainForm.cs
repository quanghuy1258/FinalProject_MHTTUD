using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FinalProject_MHTTUD
{
    public partial class MainForm : Form
    {
        private List<Account> _db = new List<Account>();
        private bool _isGuest = true;
        private int _id = -1;
        private ContextMenuStrip rightClickMenu = null;
        private Tuple<string, int[]> clipboard = new Tuple<string, int[]>("", null);

        private void isGuest()
        {
            if (this._isGuest || this._id == -1) return;
            this._isGuest = true;
            this._id = -1;
            this.userToolStripLabel.Text = "Guest";
            this.signInToolStripMenuItem.Enabled = true;
            this.registerToolStripMenuItem.Enabled = true;
            this.editInfoToolStripMenuItem.Enabled = false;
            this.signOutToolStripMenuItem.Enabled = false;
            this.accountExportToolStripMenuItem.Enabled = false;
            this.databaseImportToolStripMenuItem.Enabled = true;
            this.decryptButton.Enabled = false;
            this.signButton.Enabled = false;
        }
        private void isNotGuest(int id, string email)
        {
            if (!this._isGuest || this._id != -1) return;
            this._isGuest = false;
            this._id = id;
            this.userToolStripLabel.Text = email;
            this.signInToolStripMenuItem.Enabled = false;
            this.registerToolStripMenuItem.Enabled = false;
            this.editInfoToolStripMenuItem.Enabled = true;
            this.signOutToolStripMenuItem.Enabled = true;
            this.accountExportToolStripMenuItem.Enabled = true;
            this.databaseImportToolStripMenuItem.Enabled = false;
            this.decryptButton.Enabled = true;
            this.signButton.Enabled = true;
        }
        private void listDirectory(string path)
        {
            clipboard = new Tuple<string, int[]>("", null);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (!this.browseButton.InvokeRequired
                || !this.browseProgressBar.InvokeRequired
                || !this.filesDirectoriesTreeView.InvokeRequired) ;
                this.browseButton.Invoke(new Action(() => this.browseButton.Enabled = false));
                this.browseProgressBar.Invoke(new Action(() =>
                {
                    this.browseProgressBar.Minimum = 0;
                    this.browseProgressBar.Maximum = 100;
                    this.browseProgressBar.Value = 0;
                }));
                this.filesDirectoriesTreeView.Invoke(new Action(() =>
                {
                    this.filesDirectoriesTreeView.Enabled = false;
                    this.filesDirectoriesTreeView.Nodes.Clear();
                }));
                Stack<Tuple<TreeNode, int>> stack = new Stack<Tuple<TreeNode, int>>();
                TreeNode rootNode = new TreeNode(path) { BackColor = Color.Yellow };
                this.filesDirectoriesTreeView.Invoke(new Action(() => this.filesDirectoriesTreeView.Nodes.Add(rootNode)));
                while (rootNode.TreeView == null) ;
                stack.Push(Tuple.Create<TreeNode, int>(rootNode, 100));
                while (stack.Count > 0)
                {
                    TreeNode currentNode = stack.Peek().Item1;
                    int sum = stack.Peek().Item2;
                    stack.Pop();
                    DirectoryInfo directoryInfo = new DirectoryInfo(currentNode.FullPath);
                    try
                    {
                        int length = directoryInfo.GetDirectories().Length;
                        int subsum = 0;
                        if (length != 0)
                        {
                            subsum = sum / length;
                            sum -= subsum * length;
                        }
                        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                        {
                            TreeNode childDirectoryNode = new TreeNode(directory.Name) { BackColor = Color.Yellow };
                            this.filesDirectoriesTreeView.Invoke(new Action(() => currentNode.Nodes.Add(childDirectoryNode)));
                            while (childDirectoryNode.TreeView == null) ;
                            stack.Push(Tuple.Create<TreeNode, int>(childDirectoryNode, subsum));
                        }
                        foreach (FileInfo file in directoryInfo.GetFiles())
                            this.filesDirectoriesTreeView.Invoke(new Action(() => currentNode.Nodes.Add(new TreeNode(file.Name))));
                        this.browseProgressBar.Invoke(new Action(() => this.browseProgressBar.Value += sum));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                this.filesDirectoriesTreeView.Invoke(new Action(() => this.filesDirectoriesTreeView.Enabled = true));
                this.browseProgressBar.Invoke(new Action(() => this.browseProgressBar.Value = 0));
                this.browseButton.Invoke(new Action(() => this.browseButton.Enabled = true));
            }).Start();
        }
        private bool checkParentDirectory(DirectoryInfo testParent, DirectoryInfo testChild)
        {
            while (true)
            {
                if (testParent.FullName == testChild.FullName) return true;
                testChild = testChild.Parent;
                if (testChild == null) break;
            }
            return false;
        }
        private void createRightClickMenu()
        {
            rightClickMenu = new ContextMenuStrip() { Name = "rightClickMenu" };
            ToolStripMenuItem menuCut = new ToolStripMenuItem("Cut") { Name = "menuCut" };
            ToolStripMenuItem menuCopy = new ToolStripMenuItem("Copy") { Name = "menuCopy" };
            ToolStripMenuItem menuPaste = new ToolStripMenuItem("Paste") { Name = "menuPaste" };
            ToolStripMenuItem menuDelete = new ToolStripMenuItem("Delete") { Name = "menuDelete" };
            ToolStripMenuItem menuRename = new ToolStripMenuItem("Rename") { Name = "menuRename" };
            menuCut.Click += new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode currentNode = this.filesDirectoriesTreeView.SelectedNode;
                int[] idSelectedNode = new int[currentNode.Level+1];
                for (int i = currentNode.Level; i >= 0; i--)
                {
                    idSelectedNode[i] = currentNode.Index;
                    currentNode = currentNode.Parent;
                }
                clipboard = new Tuple<string, int[]>("Cut", idSelectedNode);
            });
            menuCopy.Click += new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode currentNode = this.filesDirectoriesTreeView.SelectedNode;
                int[] idSelectedNode = new int[currentNode.Level + 1];
                for (int i = currentNode.Level; i >= 0; i--)
                {
                    idSelectedNode[i] = currentNode.Index;
                    currentNode = currentNode.Parent;
                }
                clipboard = new Tuple<string, int[]>("Copy", idSelectedNode);
            });
            menuPaste.Click += new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode selectedNode = this.filesDirectoriesTreeView.SelectedNode;
                DirectoryInfo directoryInfo = new DirectoryInfo(selectedNode.FullPath);
                try
                {
                    if (clipboard.Item1 == "Cut")
                    {
                        int[] idSourceNode = clipboard.Item2; clipboard = new Tuple<string, int[]>("", null);
                        TreeNode sourceNode = this.filesDirectoriesTreeView.Nodes[idSourceNode[0]];
                        for (int i = 1; i < idSourceNode.Length; i++) sourceNode = sourceNode.Nodes[idSourceNode[i]];
                        if (sourceNode.BackColor == Color.Yellow)
                        {
                            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceNode.FullPath);
                            if (Directory.Exists(directoryInfo.FullName + @"\" + sourceNode.Text))
                                throw new Exception("Directory already exists.");
                            if (checkParentDirectory(sourceDirectory, directoryInfo))
                                throw new Exception("Cannot cut a directory into itself.");
                            sourceDirectory.MoveTo(directoryInfo.FullName + @"\" + sourceNode.Text);
                            this.filesDirectoriesTreeView.Nodes.Remove(sourceNode);
                            selectedNode.Nodes.Insert(0, sourceNode);
                        } else
                        {
                            FileInfo sourceFile = new FileInfo(sourceNode.FullPath);
                            if (File.Exists(directoryInfo.FullName + @"\" + sourceNode.Text))
                                throw new Exception("File already exists.");
                            sourceFile.MoveTo(directoryInfo.FullName + @"\" + sourceNode.Text);
                            this.filesDirectoriesTreeView.Nodes.Remove(sourceNode);
                            selectedNode.Nodes.Add(sourceNode);
                        }
                    }
                    if (clipboard.Item1 == "Copy")
                    {
                        int[] idSourceNode = clipboard.Item2; clipboard = new Tuple<string, int[]>("", null);
                        TreeNode sourceNode = this.filesDirectoriesTreeView.Nodes[idSourceNode[0]];
                        for (int i = 1; i < idSourceNode.Length; i++) sourceNode = sourceNode.Nodes[idSourceNode[i]];
                        if (sourceNode.BackColor == Color.Yellow)
                        {
                            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceNode.FullPath);
                            if (Directory.Exists(directoryInfo.FullName + @"\" + sourceNode.Text))
                                throw new Exception("Directory already exists.");
                            if (checkParentDirectory(sourceDirectory, directoryInfo))
                                throw new Exception("Cannot copy a directory into itself.");
                            DirectoryInfo targetDirectory = new DirectoryInfo(directoryInfo.FullName + @"\" + sourceNode.Text);
                            copyAll(sourceDirectory, targetDirectory);
                            selectedNode.Nodes.Insert(0, (TreeNode)sourceNode.Clone());
                        }
                        else
                        {
                            FileInfo sourceFile = new FileInfo(sourceNode.FullPath);
                            if (File.Exists(directoryInfo.FullName + @"\" + sourceNode.Text))
                                throw new Exception("File already exists.");
                            sourceFile.CopyTo(directoryInfo.FullName + @"\" + sourceNode.Text);
                            selectedNode.Nodes.Add((TreeNode)sourceNode.Clone());
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            menuDelete.Click += new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode selectedNode = this.filesDirectoriesTreeView.SelectedNode;
                if (selectedNode.BackColor == Color.Yellow)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(selectedNode.FullPath);
                    if (MessageBox.Show("Are you sure you want to permanently delete this folder?", "Delete Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            directoryInfo.Delete(true);
                            this.filesDirectoriesTreeView.Nodes.Remove(selectedNode);
                            this.filesDirectoriesTreeView.SelectedNode = null;
                        } catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } else
                {
                    FileInfo fileInfo = new FileInfo(selectedNode.FullPath);
                    if (MessageBox.Show("Are you sure you want to permanently delete this file?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            fileInfo.Delete();
                            this.filesDirectoriesTreeView.Nodes.Remove(selectedNode);
                            this.filesDirectoriesTreeView.SelectedNode = null;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            });
            menuRename.Click += new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode selectedNode = this.filesDirectoriesTreeView.SelectedNode;
                if (selectedNode.BackColor == Color.Yellow)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(selectedNode.FullPath);
                    string newName = Interaction.InputBox("Enter name of this folder", "Rename Folder", selectedNode.Text);
                    if (newName.Length == 0 || newName == selectedNode.Text) return;
                    try
                    {
                        directoryInfo.MoveTo(directoryInfo.Parent.FullName + @"\" + newName);
                        selectedNode.Text = newName;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(selectedNode.FullPath);
                    string newName = Interaction.InputBox("Enter name of this file", "Rename File", selectedNode.Text);
                    if (newName.Length == 0 || newName == selectedNode.Text) return;
                    try
                    {
                        fileInfo.MoveTo(fileInfo.Directory.FullName + @"\" + newName);
                        selectedNode.Text = newName;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });
            rightClickMenu.Items.AddRange(new ToolStripItem[] { menuCut, menuCopy, menuPaste, new ToolStripSeparator(), menuDelete, menuRename });
        }
        private void copyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.EnumerateFiles())
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name));
            foreach (DirectoryInfo diSourceSubDir in source.EnumerateDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                copyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        private void updateDatabaseControl()
        {
            if (this._db.Count == 0)
            {
                this.accountListBox.SelectionMode = SelectionMode.None;
                this.accountListBox.DataSource = new string[] { "(None)" };
                this.databaseExportToolStripMenuItem.Enabled = false;
                this.encryptButton.Enabled = false;
                this.verifyButton.Enabled = false;
            } else
            {
                this.accountListBox.SelectionMode = SelectionMode.One;
                this.accountListBox.DataSource = null;
                this.accountListBox.DataSource = this._db;
                this.accountListBox.DisplayMember = "email";
                this.databaseExportToolStripMenuItem.Enabled = true;
                this.encryptButton.Enabled = true;
                this.verifyButton.Enabled = true;
            }
        }
        private void doCheckedNodes(TreeNodeCollection nodes, EventHandler eh)
        {
            foreach (TreeNode aNode in nodes)
            {
                if (aNode.Checked)
                {
                    aNode.Checked = false;
                    if (aNode.FullPath != this.pathTextBox.Text) eh.Invoke(aNode, null);
                    else
                    {
                        MessageBox.Show("Cannot check the root folder. The root folder will be unchecked.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (aNode.Nodes.Count > 0)
                    doCheckedNodes(aNode.Nodes, eh);
            }
        }
        private void compressDirectory(string fullPath, int lengthRootDirectoryPath, ZipArchive archive)
        {
            archive.CreateEntry(fullPath.Substring(lengthRootDirectoryPath)+"/");
            foreach (string subdir in Directory.EnumerateDirectories(fullPath))
                compressDirectory(subdir, lengthRootDirectoryPath, archive);
            foreach (string file in Directory.EnumerateFiles(fullPath))
            {
                ZipArchiveEntry entry = archive.CreateEntry(file.Substring(lengthRootDirectoryPath));
                using (BinaryWriter writer = new BinaryWriter(entry.Open()))
                    writer.Write(File.ReadAllBytes(file));
            }
        }

        public MainForm()
        {
            this.Icon = Properties.Resources.key;
            InitializeComponent();
            isNotGuest(0, "Guest");
            isGuest();
            this.pathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            listDirectory(this.pathTextBox.Text);
            createRightClickMenu();
            updateDatabaseControl();
            this.compressOneFileCheckBox.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = 
                "Final Project - Mã hoá thông tin và Ứng dụng\n" +
                "\n" +
                "Team member:\n" +
                "1512102 - Phan Trọng Đạt\n" +
                "1512205 - Nguyễn Văn Quang Huy\n";
            MessageBox.Show(info, "About us", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void signInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignInForm signInForm = new SignInForm(this._db);
            signInForm.ShowDialog();
            if (signInForm.id != -1)
                isNotGuest(signInForm.id, this._db[signInForm.id].email);
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(ref this._db);
            registerForm.ShowDialog();
            updateDatabaseControl();
        }

        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditInfoForm editInfoForm = new EditInfoForm(ref this._db, this._db[this._id].email);
            editInfoForm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isGuest();
        }

        private void accountImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ACC Files (*.acc)|*.acc";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string xml = File.ReadAllText(openFileDialog.FileName, Encoding.GetEncoding("UTF-8"));
                    Account acc = new Account();
                    if (acc.import(xml))
                    {
                        for (int i = 0; i < this._db.Count; i++)
                        {
                            if (this._db[i].getEmailAddress() == acc.getEmailAddress())
                            {
                                MessageBox.Show("Duplicate Account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        this._db.Add(acc);
                        updateDatabaseControl();
                    }
                    else MessageBox.Show("Failed to Import", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void accountExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream; 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ACC Files (*.acc)|*.acc";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(this._db[this._id].export());
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Close();
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void databaseImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DBACC Files (*.dbacc)|*.dbacc";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string xml = File.ReadAllText(openFileDialog.FileName, Encoding.GetEncoding("UTF-8"));
                    if (!Account.importDatabase(xml, ref this._db))
                        MessageBox.Show("Failed to Import", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else updateDatabaseControl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void databaseExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DBACC Files (*.dbacc)|*.dbacc";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(Account.exportDatabase(this._db));
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Close();
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.pathTextBox.Text = folderBrowserDialog.SelectedPath;
                listDirectory(this.pathTextBox.Text);
            }
        }

        private void filesDirectoriesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Node.FullPath != this.pathTextBox.Text)
            {
                ToolStripMenuItem menuPaste = (ToolStripMenuItem)rightClickMenu.Items["menuPaste"];
                menuPaste.Enabled = true;
                if (e.Node.BackColor != Color.Yellow) menuPaste.Enabled = false;
                if (clipboard.Item1 != "Cut" && clipboard.Item1 != "Copy") menuPaste.Enabled = false;
                this.filesDirectoriesTreeView.SelectedNode = e.Node;
                this.filesDirectoriesTreeView.ContextMenuStrip = rightClickMenu;
            }
        }

        private void filesDirectoriesTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            this.filesDirectoriesTreeView.SelectedNode = null;
            this.filesDirectoriesTreeView.ContextMenuStrip = null;
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remember:\n" +
                "With Folder(s), Encryption Feature MUST compress it before encrypting.\n\n" +
                "Do you want to continue?", "Encrypt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Account receiver = (Account)this.accountListBox.SelectedItem;
            List<string> successList = new List<string>();
            List<string> failureList = new List<string>();
            int mode = 0;
            mode += (this.compressCheckBox.Checked) ? 1 : 0;
            mode += (this.compressOneFileCheckBox.Checked) ? 1 : 0;
            EventHandler encryptEvent = null;
            switch (mode)
            {
                case 0:
                    encryptEvent = new EventHandler(delegate (object o, EventArgs a)
                    {
                        TreeNode node = (TreeNode)o;
                        string path = node.FullPath;
                        try
                        {
                            if (File.Exists(path + ".enc")) throw new Exception("Cannot encrypt because ENC file already exists.");
                            byte[] zipData = null;
                            if (node.BackColor == Color.Yellow)
                            {
                                using (MemoryStream memStream = new MemoryStream())
                                {
                                    using (ZipArchive archive = new ZipArchive(memStream, ZipArchiveMode.Create))
                                        compressDirectory(path, Path.GetDirectoryName(path).Length + 1, archive);
                                    zipData = memStream.ToArray();
                                }
                            } else zipData = File.ReadAllBytes(path);
                            byte[] data = new byte[zipData.Length + 33];
                            data[0] = (byte)((node.BackColor == Color.Yellow) ? 1 : 0);
                            Buffer.BlockCopy(SHA256.Create().ComputeHash(zipData), 0, data, 1, 32);
                            Buffer.BlockCopy(zipData, 0, data, 33, zipData.Length);
                            File.WriteAllBytes(path + ".enc", DataEncryption.encrypt(receiver.getPublicKey(), data));
                            successList.Add(path);
                            node.Parent.Nodes.Add(node.Text + ".enc");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            failureList.Add(path);
                        }
                    });
                    doCheckedNodes(this.filesDirectoriesTreeView.Nodes, encryptEvent);
                    break;
                case 1:
                    encryptEvent = new EventHandler(delegate (object o, EventArgs a)
                    {
                        TreeNode node = (TreeNode)o;
                        string path = node.FullPath;
                        try
                        {
                            if (File.Exists(path + ".enc")) throw new Exception("Cannot encrypt because ENC file already exists.");
                            byte[] zipData = null;
                            if (node.BackColor == Color.Yellow)
                            {
                                using (MemoryStream memStream = new MemoryStream())
                                {
                                    using (ZipArchive archive = new ZipArchive(memStream, ZipArchiveMode.Create))
                                        compressDirectory(path, Path.GetDirectoryName(path).Length + 1, archive);
                                    zipData = memStream.ToArray();
                                }
                            }
                            else
                            {
                                using (MemoryStream memStream = new MemoryStream())
                                {
                                    using (ZipArchive archive = new ZipArchive(memStream, ZipArchiveMode.Create))
                                    {
                                        ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(path));
                                        using (BinaryWriter writer = new BinaryWriter(entry.Open()))
                                            writer.Write(File.ReadAllBytes(path));
                                    }
                                    zipData = memStream.ToArray();
                                }
                            }
                            byte[] data = new byte[zipData.Length + 33];
                            data[0] = 1;
                            Buffer.BlockCopy(SHA256.Create().ComputeHash(zipData), 0, data, 1, 32);
                            Buffer.BlockCopy(zipData, 0, data, 33, zipData.Length);
                            File.WriteAllBytes(path + ".enc", DataEncryption.encrypt(receiver.getPublicKey(), data));
                            successList.Add(path);
                            node.Parent.Nodes.Add(node.Text + ".enc");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            failureList.Add(path);
                        }
                    });
                    doCheckedNodes(this.filesDirectoriesTreeView.Nodes, encryptEvent);
                    break;
                case 2:
                    try
                    {
                        DirectoryInfo root = new DirectoryInfo(this.pathTextBox.Text);
                        if (File.Exists(root.FullName + "\\" + root.Name + ".enc")) throw new Exception("Cannot encrypt because ENC file already exists.");
                        byte[] zipData = null;
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            using (ZipArchive archive = new ZipArchive(memStream, ZipArchiveMode.Create))
                            {
                                encryptEvent = new EventHandler(delegate (object o, EventArgs a)
                                {
                                    TreeNode node = (TreeNode)o;
                                    string path = node.FullPath;
                                    if (node.BackColor == Color.Yellow)
                                        compressDirectory(path, Path.GetDirectoryName(path).Length + 1, archive);
                                    else
                                    {
                                        ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(path));
                                        using (BinaryWriter writer = new BinaryWriter(entry.Open()))
                                            writer.Write(File.ReadAllBytes(path));
                                    }
                                });
                                doCheckedNodes(this.filesDirectoriesTreeView.Nodes, encryptEvent);
                            }
                            zipData = memStream.ToArray();
                        }
                        byte[] data = new byte[zipData.Length + 33];
                        data[0] = 1;
                        Buffer.BlockCopy(SHA256.Create().ComputeHash(zipData), 0, data, 1, 32);
                        Buffer.BlockCopy(zipData, 0, data, 33, zipData.Length);
                        File.WriteAllBytes(root.FullName + "\\" + root.Name + ".enc", DataEncryption.encrypt(receiver.getPublicKey(), data));
                        this.filesDirectoriesTreeView.Nodes[0].Nodes.Add(root.Name + ".enc");
                        MessageBox.Show("Encrypt Success", "Encrypt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                default:
                    MessageBox.Show("Encrypt Failure", "Encrypt Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            string success = "Success: " + successList.Count.ToString() + " file(s) or folder(s)\n";
            foreach (string s in successList)
                success += s + "\n";
            string failure = "Failure: " + failureList.Count.ToString() + " file(s) or folder(s)\n";
            foreach (string s in failureList)
                failure += s + "\n";
            MessageBox.Show("Result:\n\n" + success + "\n" + failure, "Encrypt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox("Enter password to decrypt file(s)", "Decrypt File(s)");
            if (password == "") return;
            if (!this._db[this._id].checkPassphrase(password))
            {
                MessageBox.Show("Wrong Password. Please re-enter your password.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> successList = new List<string>();
            List<string> failureList = new List<string>();
            EventHandler decryptEvent = new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode node = (TreeNode)o;
                string path = node.FullPath;
                FileInfo fileInfo = new FileInfo(path);
                try
                {
                    if (fileInfo.Extension != ".enc")
                        throw new Exception("Cannot decrypt because of wrong type of file.");
                    byte[] data = DataEncryption.decrypt(this._db[this._id], password, File.ReadAllBytes(path));
                    byte[] sig = data.Skip(1).Take(32).ToArray();
                    byte[] zipData = data.Skip(33).ToArray();
                    if (!SHA256.Create().ComputeHash(zipData).SequenceEqual(sig))
                        throw new Exception("Cannot decrypt because of wrong key.");
                    if (data[0] == 0)
                    {
                        FileInfo newFile = new FileInfo(path.Substring(0, path.Length - fileInfo.Extension.Length));
                        if (newFile.Exists)
                            throw new Exception("Cannot decrypt the file because decryption file already exists.");
                        File.WriteAllBytes(newFile.FullName, zipData);
                        node.Parent.Nodes.Add(newFile.Name);
                    } else
                    {
                        DirectoryInfo newDir = new DirectoryInfo(path.Substring(0, path.Length - fileInfo.Extension.Length));
                        if (newDir.Exists)
                            throw new Exception("Cannot decrypt the file because decryption folder already exists.");
                        using (MemoryStream memStream = new MemoryStream(zipData))
                        using (ZipArchive archive = new ZipArchive(memStream))
                            archive.ExtractToDirectory(newDir.FullName);
                        node.Parent.Nodes.Insert(0, new TreeNode(newDir.Name) { BackColor = Color.Yellow });
                    }
                    successList.Add(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    failureList.Add(path);
                }
            });
            doCheckedNodes(this.filesDirectoriesTreeView.Nodes, decryptEvent);
            string success = "Success: " + successList.Count.ToString() + " file(s)\n";
            foreach (string s in successList)
                success += s + "\n";
            string failure = "Failure: " + failureList.Count.ToString() + " file(s)\n";
            foreach (string s in failureList)
                failure += s + "\n";
            MessageBox.Show("Result:\n\n" + success + "\n" + failure, "Decrypt File(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void signButton_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox("Remember:\n" +
                "Signing Feature supports only File(s), not Folder(s)\n\n" +
                "Enter password to sign file(s)", "Sign File(s)");
            if (password == "") return;
            if (!this._db[this._id].checkPassphrase(password))
            {
                MessageBox.Show("Wrong Password. Please re-enter your password.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> successList = new List<string>();
            List<string> failureList = new List<string>();
            EventHandler signEvent = new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode node = (TreeNode)o;
                string path = node.FullPath;
                try
                {
                    if (File.Exists(path + ".sig")) throw new Exception("Cannot sign the file because SIG file already exists.");
                    if (node.BackColor == Color.Yellow) throw new Exception("Cannot sign a folder");
                    byte[] data = File.ReadAllBytes(path);
                    File.WriteAllBytes(path + ".sig", Account.signData(this._db[this._id], password, data));
                    successList.Add(path);
                    node.Parent.Nodes.Add(node.Text + ".sig");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    failureList.Add(path);
                }
            });
            doCheckedNodes(this.filesDirectoriesTreeView.Nodes, signEvent);
            string success = "Success: " + successList.Count.ToString() + " file(s)\n";
            foreach (string s in successList)
                success += s + "\n";
            string failure = "Failure: " + failureList.Count.ToString() + " file(s)\n";
            foreach (string s in failureList)
                failure += s + "\n";
            MessageBox.Show("Result:\n\n" + success + "\n" + failure, "Sign File(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remember:\nVerifying Feature supports only File(s), not Folder(s)\n\nDo you want to continue?", "Verify File(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            List<string> successList = new List<string>();
            List<string> failureList = new List<string>();
            EventHandler verifyEvent = new EventHandler(delegate (object o, EventArgs a)
            {
                TreeNode node = (TreeNode)o;
                string path = node.FullPath;
                try
                {
                    if (node.BackColor == Color.Yellow) throw new Exception("Cannot verify a folder");
                    FileInfo fileInfo = new FileInfo(path);
                    byte[] data = File.ReadAllBytes(fileInfo.FullName);
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "SIG Files (*.sig)|*.sig";
                    openFileDialog.Title = "Open " + fileInfo.Name + ".sig File";
                    openFileDialog.InitialDirectory = fileInfo.DirectoryName;
                    openFileDialog.Multiselect = false;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        byte[] sig = File.ReadAllBytes(openFileDialog.FileName);
                        foreach (Account owner in this._db)
                        {
                            if (Account.verifyData(owner.getPublicKey(), data, sig))
                            {
                                successList.Add(fileInfo.FullName + " | " + owner.email);
                                return;
                            }
                        }
                    }
                    successList.Add(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    failureList.Add(path);
                }
            });
            doCheckedNodes(this.filesDirectoriesTreeView.Nodes, verifyEvent);
            string success = "Success: " + successList.Count.ToString() + " file(s)\n";
            foreach (string s in successList)
                success += s + "\n";
            string failure = "Failure: " + failureList.Count.ToString() + " file(s)\n";
            foreach (string s in failureList)
                failure += s + "\n";
            MessageBox.Show("Result:\n\n" + success + "\n" + failure, "Verify File(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void compressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.compressOneFileCheckBox.Checked = false;
            this.compressOneFileCheckBox.Enabled = this.compressCheckBox.Checked;
        }
    }
}