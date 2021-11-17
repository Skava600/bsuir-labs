using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace lab1form
{
    public partial class Form1 : Form
    {
        StringBuilder curDir = new StringBuilder(@"C:\");
        readonly List<FileObj> FileList = new List<FileObj>();
        readonly List<string> history = new List<string>();
        readonly List<string> filesToCopy = new List<string>();
        readonly List<string> directoriesToCopy = new List<string>();
        readonly List<string> historyNext = new List<string>();
        public Form1()
        {
            InitializeComponent();
            GoToDirectory(curDir.ToString());
        }
        private void GoToDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                FileList.Clear();
                string[] directories = Directory.GetDirectories(dir);
                string[] files = Directory.GetFiles(dir);

                if (directories.Length != 0)
                {
                    foreach (string directory in directories)
                    {
                        FileList.Add(new FileObj(Path.GetFileName(directory), true));
                    }
                }
                if (files.Length != 0)
                {
                    foreach (string file in files)
                    {
                        FileList.Add(new FileObj(Path.GetFileName(file), false));
                    }
                }
                curDir = new StringBuilder(dir);
                UpdateListView();
            }
            else
            {
                try
                {
                    GoToDirectory(curDir.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error" + e.Message);
                }
            }
        }
        private void UpdateListView()
        {
            int i = 0;

            ExplorerWnds.Items.Clear();
            foreach (var file in FileList)
            {
                if (file.IsFolder)
                {
                    ExplorerWnds.Items.Add(file.Name);
                    ExplorerWnds.Items[i].BackColor = Color.Yellow;
                }
                else
                {
                    ExplorerWnds.Items.Add(file.Name);
                    if (Path.GetExtension(Path.Combine(curDir.ToString() + file.Name)) == ".zip")
                    {
                        ExplorerWnds.Items[i].BackColor = Color.Red;
                    }
                    else
                    {
                        ExplorerWnds.Items[i].BackColor = Color.White;
                    }
                }
                i++;
            }
            explorerInput.Text = curDir.ToString();
            filePreview.Text = "";
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm newFile = new CreateForm(curDir.ToString(), false);
            newFile.Show();
            GoToDirectory(curDir.ToString());
        }

        private void Explorer_DoubleClick(object sender, MouseEventArgs e)
        {            
                if (ExplorerWnds.SelectedIndices.Count == 1)
                {
                    var file = FileList[ExplorerWnds.SelectedIndices[0]];
                    var path = Path.Combine(curDir.ToString(), file.Name);
                    if (file.IsFolder)
                    {
                        history.Add(curDir.ToString());
                        historyNext.Clear();
                        next_Button.Enabled = false;
                        GoToDirectory(path);
                        prev_Button.Enabled = true;
                    }   
                    else
                    {
                    filePreview.Text = "";
                    var editor = new EditFileForm(path);
                    editor.Show();
                    }
                }            
        }

        private void Explorer_Click(object sender, MouseEventArgs e)
        {
            if (ExplorerWnds.SelectedIndices.Count == 1)
            {
                var curFile = FileList[ExplorerWnds.SelectedIndices[0]];
                if (!curFile.IsFolder && Path.GetExtension(curFile.Name) == ".txt")
                {
                    var path = Path.Combine(curDir.ToString(), curFile.Name);
                    filePreview.Text = File.ReadAllText(path);
                }
                else
                {
                   filePreview.Text = "";
                }
            }
        }
        private void Next_Button_Click(object sender, EventArgs e)
        {
            if (historyNext.Count != 0)
            {
                history.Add(curDir.ToString());
                while (!Directory.Exists(historyNext.Last()))
                {
                    historyNext.RemoveAt(historyNext.Count - 1);
                    if (historyNext.Count == 0)
                    {
                        break;
                    }
                }
                GoToDirectory(historyNext.Last() ?? curDir.ToString());
                historyNext.RemoveAt(historyNext.Count - 1);
                if (historyNext.Count == 0)
                {
                    next_Button.Enabled = false;
                }
                prev_Button.Enabled = true;
            }
            else
            {
                next_Button.Enabled = false;
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            if (history.Count != 0)
            {
                historyNext.Add(curDir.ToString());
                while (!Directory.Exists(history.Last()))
                {
                    history.RemoveAt(history.Count - 1);
                }
                GoToDirectory(history.Last());
                history.RemoveAt(history.Count - 1);
                if (history.Count == 0)
                {
                    prev_Button.Enabled = false;
                }
                next_Button.Enabled = true;
            }
            else
            {
                prev_Button.Enabled = false;
                
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesToCopy.Clear();
            directoriesToCopy.Clear();
            var copies = ExplorerWnds.SelectedIndices;
            for(int i = 0; i < copies.Count;i++)
            {
                if (!FileList[copies[i]].IsFolder)
                    filesToCopy.Add(Path.Combine(curDir.ToString(), FileList[copies[i]].Name));
                else
                    directoriesToCopy.Add(Path.Combine(curDir.ToString(), FileList[copies[i]].Name));
            }
            contextMenuStrip1.Items[2].Enabled = true;
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string file in filesToCopy)
                {
                    File.Copy(file, Path.Combine(curDir.ToString(), Path.GetFileName(file)), true);
                }
                foreach (var directory in directoriesToCopy)
                {
                    var newPath = Path.Combine(curDir.ToString(), Path.GetFileName(directory));
                    Directory.CreateDirectory(newPath);
                    CopyDirectory(directory, newPath);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't paste this file here");
            }
            GoToDirectory(curDir.ToString());
        }
        private void CopyDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string newDirPath = Path.Combine(dest, Path.GetFileName(directory));
                if (!Directory.Exists(newDirPath))
                {
                    Directory.CreateDirectory(newDirPath);
                }
                CopyDirectory(directory, newDirPath);
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), true);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var files = ExplorerWnds.SelectedIndices;
            for(int i = 0; i< files.Count;i++)
            {
                if (!FileList[files[i]].IsFolder)
                {
                    File.Delete(Path.Combine(curDir.ToString(), FileList[files[i]].Name));
                }
                else
                {
                    Directory.Delete(Path.Combine(curDir.ToString(), FileList[files[i]].Name), true);
                }
            }
            GoToDirectory(curDir.ToString());
        }

        private void FolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm newFile = new CreateForm(curDir.ToString(), true);
            newFile.Show();
            GoToDirectory(curDir.ToString());
            UpdateListView();
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form rename = new RenameForm(Path.Combine(curDir.ToString(), FileList[ExplorerWnds.SelectedIndices[0]].Name));
            rename.Show();
            Refresh_Button_Click(sender, e);
           
        }

        public void Refresh_Button_Click(object sender, EventArgs e)
        {
            if (historyNext.Count != 0)
            {
                while (!Directory.Exists(historyNext.Last()))
                {
                    historyNext.RemoveAt(historyNext.Count - 1);
                    if (historyNext.Count == 0)
                    {
                        next_Button.Enabled = false;
                        break;
                    }
                }
            }
            GoToDirectory(curDir.ToString());
        }
        private string CreateUniqueDirectory(ref string path)
        {
            var newPath = new StringBuilder(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                for (int i = 1; i > 0; i++)
                {
                    newPath = new StringBuilder(path + " (" + i + ")");
                    if (!Directory.Exists(newPath.ToString()))
                    {
                        Directory.CreateDirectory(newPath.ToString());
                        break;
                    }
                }
            }
            path = newPath.ToString();
            return newPath.ToString();
        }
        private string CreateUniqueZip(ref string path)
        {
            var newPath = new StringBuilder(path + ".zip");
            if (!File.Exists(path + ".zip"))
            {
                ZipFile.CreateFromDirectory(path, newPath.ToString());
            }
            else
            {
                for (int i = 1; i > 0; i++)
                {
                    newPath = new StringBuilder(path + " (" + i + ").zip");
                    if (!File.Exists(newPath.ToString()))
                    {
                        ZipFile.CreateFromDirectory(path, newPath.ToString());
                        break;
                    }
                }
            }
            path = newPath.ToString();
            return newPath.ToString();
        }
        private void ZipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var archivePath = Path.Combine(curDir.ToString(), "archive");
            var collection = ExplorerWnds.SelectedIndices;

            CreateUniqueDirectory(ref archivePath);
            for (int i = 0; i < collection.Count; i++)
            {
                var file = FileList[collection[i]];
                file.Path = Path.Combine(curDir.ToString(), file.Name);
                if (file.IsFolder)
                {
                    var newPath = Path.Combine(archivePath, file.Name);
                    CreateUniqueDirectory(ref newPath);
                    CopyDirectory(file.Path, newPath);
                }
                else
                {
                    File.Copy(file.Path, Path.Combine(archivePath, file.Name), true);
                }
            }
            string folderPath = archivePath;
            CreateUniqueZip(ref archivePath);
            Directory.Delete(folderPath, true);

            var newNameForm = new RenameForm(archivePath);
            newNameForm.Show();

            Refresh_Button_Click(sender, e);
        }

        private void UnzipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var archievePath = Path.Combine(curDir.ToString(), ExplorerWnds.SelectedItems[0].Text);
            var newPath = Path.Combine(curDir.ToString(), Path.GetFileNameWithoutExtension(archievePath));
            CreateUniqueDirectory(ref newPath);
            ZipFile.ExtractToDirectory(archievePath, newPath);
            Refresh_Button_Click(sender, e);
        }
    }
}

