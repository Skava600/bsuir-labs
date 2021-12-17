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
    class NetStat
    {
        Panel _updatePanel;
        ListView _statView;
        BackgroundWorker _worker;

        public NetStat(Panel updatePanel)
        {
            _updatePanel = updatePanel;
            _updatePanel.Dock = DockStyle.Fill;
            _updatePanel.Controls.Clear();

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

            _statView = new ListView();
            _statView.View = View.Details;
            _statView.Dock = DockStyle.Fill; _statView.BorderStyle = BorderStyle.None;
            _statView.Columns.Add("Local Adress", 100);
            _statView.Columns.Add("Local Port", 100);
            _statView.Columns.Add("Remote Adress", 100);
            _statView.Columns.Add("Remote Port", 100);
            _statView.Columns.Add("Type", 100);
            _statView.Columns.Add("Protocol", 100);

            _updatePanel.Controls.Add(_statView);
        }

        public void Execute()
        {
            _worker.RunWorkerAsync();
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListView statView = (ListView)_updatePanel.Controls[0];
            List<ConnectionDetails> details = (List<ConnectionDetails>)e.Result;

            foreach (ConnectionDetails info in details)
            {
                ListViewItem list = new ListViewItem(info.LocalAddress);
                list.SubItems.Add(info.LocalPort);
                list.SubItems.Add(info.RemoteAddress);
                list.SubItems.Add(info.RemotePort);
                list.SubItems.Add(info.ApplicationProtocol);
                list.SubItems.Add(info.Protocol);
                statView.Items.Add(list);
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            List<ConnectionDetails> list = new List<ConnectionDetails>();

            foreach (TcpConnectionInformation info in connections)
            {
                string host;

                host = info.RemoteEndPoint.Address.ToString();

                ConnectionDetails details = new ConnectionDetails();
                details.LocalAddress = info.LocalEndPoint.Address.ToString();
                details.LocalPort = info.LocalEndPoint.Port.ToString();
                details.RemoteAddress = host;
                details.RemotePort = info.RemoteEndPoint.Port.ToString();
                details.Protocol = "TCP";

                list.Add(details);
            }

            e.Result = list;
            _worker.ReportProgress(100);
        }

        public class ConnectionDetails 
        {
            Dictionary<string, string> _appProtocolList = new Dictionary<string, string>();

            public ConnectionDetails()
            {
                _appProtocolList.Add("22", "SSH");
                _appProtocolList.Add("80", "HTTP");
                _appProtocolList.Add("443", "HTTPS");
                _appProtocolList.Add("445", "Active Directory, Windows shares");
            }

            public string LocalAddress{ set; get; }

            public string LocalPort{ set; get; }

            public string RemoteAddress{ set; get; }

            public string RemotePort{ set; get; }

            public string ApplicationProtocol
            {
                get
                {
                    if (_appProtocolList.ContainsKey(RemotePort))
                    {
                        return _appProtocolList[RemotePort];
                    }
                    return "";
                }
            }

            public string Protocol { set; get; }
        }
    }
}
