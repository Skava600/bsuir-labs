using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1form
{
    public partial class EditFileForm : Form
    {
        private string path { get; }
        public EditFileForm(string path)
        {
            InitializeComponent();
            this.path = path;
            textBox1.Text = File.ReadAllText(path);
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(path, textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Save The File");
            }
            this.Close();
        }
    }
}
