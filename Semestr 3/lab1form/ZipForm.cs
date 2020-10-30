using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1form
{
    public partial class ZipForm : Form
    {
        private string path { get; }
        public ZipForm(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            var newPath = nameBox.Text;
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

            if (File.Exists(newPath))
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
    }
}
