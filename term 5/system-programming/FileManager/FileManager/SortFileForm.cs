using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

namespace FileManager
{
    public partial class SortFileForm : Form
    {
        private int count = 1;

        public SortFileForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            fileListBox.Items.Add(openFileDialog1.FileName);
            
            MessageBox.Show("File was successfully added!");
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            OnSortButtonClick(Sorter.QuicksortSequential<string>);
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            OnGenerateButtonClick(FileManager.MakeFile);
        }

        private void sortParallelButton_Click(object sender, EventArgs e)
        {
            OnSortButtonClick(Sorter.QuicksortParallelOptimised<string>);
        }

        private void OnSortButtonClick(Action<string[], int, int> action)
        {
            if (fileListBox.Items.Count == 0 || fileListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose item to sort!", "Warning", MessageBoxButtons.OK);
                return;
            }

            var fileName = fileListBox.SelectedItem.ToString();

            Stopwatch stopWatch = new Stopwatch();

            var input = FileManager.ReadFile(fileName);

            stopWatch.Start();
            action.Invoke(input, 0, input.Length - 1);
            stopWatch.Stop();

            FileManager.RewriteFile(fileName, input);

            double elapsedTime = (double)stopWatch.ElapsedMilliseconds * 0.001;
            MessageBox.Show($"Successfully sorted! Elapsed time : {elapsedTime.ToString(CultureInfo.InvariantCulture)} sec",
                "Success!", MessageBoxButtons.OK);
        }

        private void OnGenerateButtonClick(Func<int, string> func)
        {
            int size;
            if (!int.TryParse(myTextBox1.Text, out size))
            {
                MessageBox.Show("Size must be integer, ", "Error", MessageBoxButtons.OK);
                return;
            }

            fileListBox.Items.Add(func.Invoke(size));
        }

        private async void asyncTaskButton_Click(object sender, EventArgs e)
        {
            if (fileListBox.Items.Count == 0 || fileListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose item!", "Warning", MessageBoxButtons.OK);
                return;
            }

            AsyncFileProcessor fileProcessor = new AsyncFileProcessor();
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            await fileProcessor.MakeAnotherFile(fileListBox.SelectedItem.ToString(), $"Processed_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt").ConfigureAwait(false);
            stopWatch.Stop();

            MessageBox.Show($"Success, elapsed time: {(double)stopWatch.ElapsedMilliseconds * 0.001}", "Success", MessageBoxButtons.OK);
        }

        private void taskButton_Click(object sender, EventArgs e)
        {
            if (fileListBox.Items.Count == 0 || fileListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose item!", "Warning", MessageBoxButtons.OK);
                return;
            }

            FileProcessor fileProcessor = new FileProcessor();
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            fileProcessor.MakeAnotherFile(fileListBox.SelectedItem.ToString(), $"Processed_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt");
            stopWatch.Stop();

            MessageBox.Show($"Success, elapsed time: {((double)stopWatch.ElapsedMilliseconds * 0.001).ToString(CultureInfo.InvariantCulture)}", "Success", MessageBoxButtons.OK);
        }
    }
}
