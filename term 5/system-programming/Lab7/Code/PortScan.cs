using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NetworkUtility.Code
{
    class PortScan
    {

        Panel _updatePanel;
        TextBox _txtHost;
        TextBox _txtPort;
        TextBox _txtStopPort;
        TextBox _txtScanOutput;

        BackgroundWorker _worker;

        public PortScan(Panel updatePanel)
        {
            _updatePanel = updatePanel;
            _updatePanel.Dock = DockStyle.Fill;
            _updatePanel.Controls.Clear();

            FlowLayoutPanel flp1 = new FlowLayoutPanel();
            flp1.Dock = DockStyle.Top;
            flp1.Padding = new Padding(20, 20, 0, 0);
            flp1.FlowDirection = FlowDirection.LeftToRight;
            flp1.Height = 50;

            Label lblHost = new Label();
            lblHost.AutoSize = false;
            lblHost.Width = 50;
            lblHost.TextAlign = ContentAlignment.MiddleRight;
            lblHost.Text = "Host";

            _txtHost = new TextBox();
            _txtHost.Width = 344;
            _txtHost.BorderStyle = BorderStyle.FixedSingle;
            _txtHost.BackColor = Color.FromArgb(230, 230, 230);

            flp1.Controls.Add(lblHost);
            flp1.Controls.Add(_txtHost);

            FlowLayoutPanel flp2 = new FlowLayoutPanel();
            flp2.Dock = DockStyle.Top;
            flp2.Padding = new Padding(20, 0, 0, 0);
            flp2.FlowDirection = FlowDirection.LeftToRight;
            flp2.Height = 40;

            Label lblPort = new Label();
            lblPort.AutoSize = false;
            lblPort.Width = 50;
            lblPort.TextAlign = ContentAlignment.MiddleRight;
            lblPort.Text = "Port";

            _txtPort = new TextBox();
            _txtPort.Width = 100;
            _txtPort.BorderStyle = BorderStyle.FixedSingle;
            _txtPort.BackColor = Color.FromArgb(230, 230, 230);

            Button btnScan = new Button();
            btnScan.Text = "Scan";
            btnScan.FlatAppearance.BorderSize = 0;
            btnScan.FlatStyle = FlatStyle.Flat;
            btnScan.BackColor = Color.FromArgb(230, 230, 230);
            btnScan.Click += btnScan_Click;

            flp2.Controls.Add(lblPort);
            flp2.Controls.Add(_txtPort);
            flp2.Controls.Add(btnScan);

            Panel panel3 = new Panel();
            panel3.Dock = DockStyle.Fill;
            panel3.Padding = new Padding(20, 0, 20, 20);

            _txtScanOutput = new TextBox();
            _txtScanOutput.Dock = DockStyle.Fill;
            _txtScanOutput.Multiline = true;
            _txtScanOutput.BorderStyle = BorderStyle.None;
            _txtScanOutput.ScrollBars = ScrollBars.Vertical;

            panel3.Controls.Add(_txtScanOutput);

            _updatePanel.Controls.Add(panel3);
            _updatePanel.Controls.Add(flp2);
            _updatePanel.Controls.Add(flp1);

        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (_txtPort.Text == string.Empty)
            {
                MessageBox.Show("Enter a port!");
            }
            else
            {
                if (btn.Text == "Scan")
                {
                    _worker = new BackgroundWorker();
                    _worker.WorkerReportsProgress = true;
                    _worker.WorkerSupportsCancellation = true;
                    _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
                    _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);
                    _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
                    _worker.RunWorkerAsync();

                    _txtScanOutput.Text = "";
                    _txtScanOutput.AppendText("Starting port scan..." + Environment.NewLine);
                    _txtScanOutput.AppendText("Port scanning host: " + _txtHost.Text + Environment.NewLine);
                    _txtScanOutput.AppendText(Environment.NewLine);

                    btn.Text = "Stop";
                }
                else
                {
                    _txtScanOutput.Text = "";
                    _worker.CancelAsync();
                    btn.Text = "Scan";
                }
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Port scan has completed");
        }

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PortState state = (PortState)e.UserState;

            if (state.IsOpen)
            {
                _txtScanOutput.AppendText("Port: " + state.Port.ToString() + ", open" + Environment.NewLine);
            }
            else
            {
                _txtScanOutput.AppendText("Port: " + state.Port.ToString() + ", closed" + Environment.NewLine);
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int port = Convert.ToInt32(_txtPort.Text);
                int x = 0;

                if (!_worker.CancellationPending)
                {
                    PortState state = new PortState();
                    decimal progress = (decimal)x * (decimal)100;
                    try
                    {
                        TcpClient client = new TcpClient(_txtHost.Text, port);
                        state.IsOpen = true;
                    }
                    catch (Exception ex) { }
                    state.Port = port;
                    _worker.ReportProgress((int)progress, state);
                    ++x;
                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public class PortState
        {
            public int Port
            {
                set;
                get;
            }

            public bool IsOpen
            {
                set;
                get;
            }
        }
    }
}
