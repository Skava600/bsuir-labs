using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace lab1form
{
    public partial class CreateForm : Form
    {
        string path { get; set; }
        bool isFolder { get; set; }
        public CreateForm(string curDir, bool folder)
        {
            InitializeComponent();
            path = curDir;
            isFolder = folder;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            var newPath = fileName.Text;
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                if (newPath.Contains(ch))
                {
                    MessageBox.Show("Invalid Name!");
                    return;
                }
            }
            if (newPath == "") newPath = "NewFile";
            newPath = Path.Combine(path, newPath);
            newPath += !isFolder ? ".txt" : "";
            if (File.Exists(newPath))
            {
                MessageBox.Show("The Name is already taken!");
                return;
            }
            if (!isFolder)
            {
                using (File.CreateText(newPath)) { };
            }
            else
            {
                Directory.CreateDirectory(newPath);
            }
            this.Close();
        }
    }
}
