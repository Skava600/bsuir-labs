namespace lab1form
{
    partial class RenameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rename_Button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rename_Button
            // 
            this.rename_Button.Location = new System.Drawing.Point(264, 84);
            this.rename_Button.Name = "rename_Button";
            this.rename_Button.Size = new System.Drawing.Size(75, 23);
            this.rename_Button.TabIndex = 0;
            this.rename_Button.Text = "Save";
            this.rename_Button.UseVisualStyleBackColor = true;
            this.rename_Button.Click += new System.EventHandler(this.rename_Button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 116);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rename_Button);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rename_Button;
        private System.Windows.Forms.TextBox textBox1;
    }
}