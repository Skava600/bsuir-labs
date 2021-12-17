using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    internal class MyTextBox : TextBox
    {
        private ComboBox comboBox1;
        string hint;
        public string Hint
        {
            get { return hint; }
            set { hint = value; this.Invalidate(); }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf)
            {
                if (!this.Focused && string.IsNullOrEmpty(this.Text)
                    && !string.IsNullOrEmpty(this.Hint))
                {
                    using (var g = this.CreateGraphics())
                    {
                        TextRenderer.DrawText(g, this.Hint, this.Font,
                            this.ClientRectangle, SystemColors.GrayText, this.BackColor,
                            TextFormatFlags.Top | TextFormatFlags.Left);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 37);
            this.comboBox1.TabIndex = 0;
            this.ResumeLayout(false);

        }
    }
}
