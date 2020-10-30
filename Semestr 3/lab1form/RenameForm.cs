using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using lab1form;

namespace lab1form
{
   
    public partial class RenameForm : Form
    {
        private string path { get; }
        public RenameForm(string path)
        {
            InitializeComponent();
            this.path = path;

        }

        private void rename_Button_Click(object sender, EventArgs e)
        {
            var newPath = textBox1.Text;
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                if (newPath.Contains(ch))
                {
                    MessageBox.Show("Invalid Name!");
                    return;
                }
            }
            if (newPath == "")
            {
                MessageBox.Show("Invalid Name!");
                return;
            }
            newPath = Path.Combine(Path.GetDirectoryName(path), newPath);

            if (path != newPath && File.Exists(newPath))
            {
                MessageBox.Show("The Name is already taken!");
                return;
            }
            try
            {
                File.Move(path, newPath);
            }
            catch (Exception)
            {
                Directory.Move(path, newPath);
            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
