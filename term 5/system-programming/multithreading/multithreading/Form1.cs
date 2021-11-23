using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multithreading
{
    public partial class Form1 : Form
    {
        private int[] valueArray;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            valueArray = textBox1.Text.Split().Select(n => Convert.ToInt32(n)).ToArray();

            long startTime = DateTime.Now.Ticks;
            new QuickSort(valueArray, Convert.ToInt32(threadCountBox.Text)).QuickSorting();
            long endTime = DateTime.Now.Ticks;
            labelSorted.Text = String.Join("", new List<int>(valueArray).ConvertAll(i => i.ToString()).ToArray());
            timeLabel.Text = $"Threads {threadCountBox.Text}, Elasped time: {Convert.ToString((endTime - startTime) / 10_000_000d)}";
        }


    }
}
