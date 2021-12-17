using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkUtility.Code;

namespace NetworkUtility
{
    public partial class Main : Form
    {
        Panel _netStatPanel;
        Panel _pingPanel;
        Panel _portScanPanel;

        public Main()
        {
            InitializeComponent();
        }

        private void btnNetStatNonDNS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SelectMenu(btnNetStat);
            NetStat netStat = new NetStat(_netStatPanel);
            netStat.Execute();

            this.splitContainer.Panel2.Controls.RemoveAt(0);
            this.splitContainer.Panel2.Controls.Add(_netStatPanel);
        }

        private void btnPing_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SelectMenu(btnPing);
            Pinger ping = new Pinger(_pingPanel);

            this.splitContainer.Panel2.Controls.RemoveAt(0);
            this.splitContainer.Panel2.Controls.Add(_pingPanel);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _netStatPanel = new Panel();
            _pingPanel = new Panel();
            _portScanPanel = new Panel();

            this.SelectMenu(btnNetStat);
            NetStat netStat = new NetStat(_netStatPanel);
            netStat.Execute();

            this.splitContainer.Panel2.Controls.Add(_netStatPanel);
        }

        protected void SelectMenu(LinkLabel control)
        {
            Control.ControlCollection collecion = this.splitContainer.Panel1.Controls;

            foreach (Control ctr in collecion)
            {
                try
                {
                    LinkLabel linkControl = (LinkLabel)ctr;
                    linkControl.BackColor = Color.Transparent;
                    linkControl.LinkColor = Color.White;
                }
                catch { }
            }

            control.BackColor = Color.CornflowerBlue;
            control.LinkColor = Color.Black;
        }

        private void btnPortScan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SelectMenu(btnPortScan);
            PortScan scan = new PortScan(_portScanPanel);

            this.splitContainer.Panel2.Controls.RemoveAt(0);
            this.splitContainer.Panel2.Controls.Add(_portScanPanel);
        }
    }
}
