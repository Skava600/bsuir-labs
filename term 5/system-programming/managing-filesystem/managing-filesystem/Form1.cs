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

namespace managing_filesystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string filePath = String.Empty;
        private async void OpenFileDialog_Click(object sender, EventArgs e)
        {
            Task myTask = Task.Factory.StartNew(GetFile);
            myTask.Wait();

        }

        internal async void GetFile()
        {
            Stream stream = null;
            OpenFileDialog dialog = new OpenFileDialog();
            string workingDirectory = Environment.CurrentDirectory;
            dialog.InitialDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                label1.Text = dialog.FileName;
                try
                {
                    if ((stream = dialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                textBox1.Text = reader.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
        }
    }
}
