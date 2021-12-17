using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkUtility.Code
{
    class Pinger
    {
        Panel _updateControl;
        TextBox _txtPingHost;
        ListView _pingView;
        BackgroundWorker _worker;

        public Pinger(Panel updatePanel)
        {
            _updateControl = updatePanel;
            _updateControl.Dock = DockStyle.Fill;
            _updateControl.Controls.Clear();

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Bottom;
            flp.Padding = new Padding(20);

            Label lblPingHost = new Label();
            lblPingHost.Text = "Host";
            lblPingHost.AutoSize = false;
            lblPingHost.Width = 50;
            lblPingHost.TextAlign = ContentAlignment.MiddleRight;

            _txtPingHost = new TextBox();
            _txtPingHost.BorderStyle = BorderStyle.FixedSingle;
            _txtPingHost.BackColor = Color.FromArgb(230, 230, 230);
            _txtPingHost.Width = 200;

            Button btnPing = new Button();
            btnPing.FlatAppearance.BorderSize = 0;
            btnPing.FlatStyle = FlatStyle.Flat;
            btnPing.BackColor = Color.FromArgb(230, 230, 230);
            btnPing.Text = "Ping";
            btnPing.Click += btnPing_Click;

            flp.Controls.Add(lblPingHost);
            flp.Controls.Add(_txtPingHost);
            flp.Controls.Add(btnPing);

            _pingView = new ListView();
            _pingView.View = View.Details;
            _pingView.Dock = DockStyle.Fill; 
            _pingView.BorderStyle = BorderStyle.None;
            _pingView.Columns.Add("Address", 200);
            _pingView.Columns.Add("Bytes", 200);
            _pingView.Columns.Add("Time", 100);
            _pingView.Columns.Add("TTL", 100);

            _updateControl.Controls.Add(_pingView);
            _updateControl.Controls.Add(flp);
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            _pingView.Controls.Clear();
            string host = _txtPingHost.Text;

            if (host == string.Empty)
            {

            }
            if (!_worker.IsBusy)
            {
                _pingView.Items.Clear();
                _worker.RunWorkerAsync(host);
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListView statView = (ListView)_updateControl.Controls[0];
            List<PingDetails> details = (List<PingDetails>)e.Result;

            foreach (PingDetails detail in details)
            {
                ListViewItem list = new ListViewItem(detail.Address);
                list.SubItems.Add(detail.Length);
                list.SubItems.Add(detail.Time);
                list.SubItems.Add(detail.TTL);
                _pingView.Items.Add(list);
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string host = (string)e.Argument;

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;

            string data = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            List<PingDetails> list  = new List<PingDetails>();

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    PingReply reply = pingSender.Send(host, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        PingDetails details = new PingDetails();
                        details.Address = reply.Address.ToString();
                        details.Length = reply.Buffer.Length.ToString();
                        details.Time = reply.RoundtripTime.ToString();
                        details.TTL = reply.Options.Ttl.ToString();

                        list.Add(details);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
            }

            e.Result = list;
            _worker.ReportProgress(100);
        }

        public class PingDetails
        {
            public string Address { get; set; }

            public string Length { get; set; }

            public string Time { get; set; }

            public string TTL { get; set; }
        }
    }
}
