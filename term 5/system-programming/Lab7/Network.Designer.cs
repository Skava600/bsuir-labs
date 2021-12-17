namespace NetworkUtility
{
    partial class Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnPortScan = new System.Windows.Forms.LinkLabel();
            this.btnPing = new System.Windows.Forms.LinkLabel();
            this.btnNetStat = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitContainer.Panel1.Controls.Add(this.btnPortScan);
            this.splitContainer.Panel1.Controls.Add(this.btnPing);
            this.splitContainer.Panel1.Controls.Add(this.btnNetStat);
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer.Size = new System.Drawing.Size(1404, 1002);
            this.splitContainer.SplitterDistance = 90;
            this.splitContainer.SplitterWidth = 9;
            this.splitContainer.TabIndex = 0;
            // 
            // btnPortScan
            // 
            this.btnPortScan.ActiveLinkColor = System.Drawing.Color.Black;
            this.btnPortScan.DisabledLinkColor = System.Drawing.Color.White;
            this.btnPortScan.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPortScan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPortScan.ForeColor = System.Drawing.Color.White;
            this.btnPortScan.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnPortScan.LinkColor = System.Drawing.Color.White;
            this.btnPortScan.Location = new System.Drawing.Point(443, 0);
            this.btnPortScan.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPortScan.Name = "btnPortScan";
            this.btnPortScan.Padding = new System.Windows.Forms.Padding(23, 22, 23, 22);
            this.btnPortScan.Size = new System.Drawing.Size(217, 90);
            this.btnPortScan.TabIndex = 4;
            this.btnPortScan.TabStop = true;
            this.btnPortScan.Text = "Port Scan";
            this.btnPortScan.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPortScan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnPortScan_LinkClicked);
            // 
            // btnPing
            // 
            this.btnPing.ActiveLinkColor = System.Drawing.Color.Black;
            this.btnPing.DisabledLinkColor = System.Drawing.Color.White;
            this.btnPing.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPing.ForeColor = System.Drawing.Color.White;
            this.btnPing.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnPing.LinkColor = System.Drawing.Color.White;
            this.btnPing.Location = new System.Drawing.Point(298, 0);
            this.btnPing.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPing.Name = "btnPing";
            this.btnPing.Padding = new System.Windows.Forms.Padding(23, 22, 23, 22);
            this.btnPing.Size = new System.Drawing.Size(145, 90);
            this.btnPing.TabIndex = 3;
            this.btnPing.TabStop = true;
            this.btnPing.Text = "Ping";
            this.btnPing.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPing.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnPing_LinkClicked);
            // 
            // btnNetStat
            // 
            this.btnNetStat.ActiveLinkColor = System.Drawing.Color.Black;
            this.btnNetStat.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNetStat.DisabledLinkColor = System.Drawing.Color.White;
            this.btnNetStat.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNetStat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetStat.ForeColor = System.Drawing.Color.White;
            this.btnNetStat.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnNetStat.LinkColor = System.Drawing.Color.White;
            this.btnNetStat.Location = new System.Drawing.Point(0, 0);
            this.btnNetStat.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnNetStat.Name = "btnNetStat";
            this.btnNetStat.Padding = new System.Windows.Forms.Padding(23, 22, 23, 22);
            this.btnNetStat.Size = new System.Drawing.Size(298, 90);
            this.btnNetStat.TabIndex = 2;
            this.btnNetStat.TabStop = true;
            this.btnNetStat.Text = "Network Statistics";
            this.btnNetStat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnNetStatNonDNS_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1404, 1002);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Utility";
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.LinkLabel btnNetStat;
        private System.Windows.Forms.LinkLabel btnPing;
        private System.Windows.Forms.LinkLabel btnPortScan;
    }
}

