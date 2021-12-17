namespace FileManager
{
    partial class SortFileForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.addFileButton = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sortButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.sortParallelButton = new System.Windows.Forms.Button();
            this.asyncTaskButton = new System.Windows.Forms.Button();
            this.taskButton = new System.Windows.Forms.Button();
            this.myTextBox1 = new global::FileManager.MyTextBox();
            this.SuspendLayout();
            // 
            // addFileButton
            // 
            this.addFileButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.addFileButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.addFileButton.FlatAppearance.BorderSize = 2;
            this.addFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addFileButton.Location = new System.Drawing.Point(18, 104);
            this.addFileButton.Margin = new System.Windows.Forms.Padding(7);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(268, 74);
            this.addFileButton.TabIndex = 0;
            this.addFileButton.Text = "Add File";
            this.addFileButton.UseVisualStyleBackColor = false;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.BackColor = System.Drawing.Color.LightBlue;
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.ItemHeight = 29;
            this.fileListBox.Location = new System.Drawing.Point(658, 39);
            this.fileListBox.Margin = new System.Windows.Forms.Padding(7);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(583, 410);
            this.fileListBox.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sortButton
            // 
            this.sortButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sortButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sortButton.FlatAppearance.BorderSize = 2;
            this.sortButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sortButton.Location = new System.Drawing.Point(16, 223);
            this.sortButton.Margin = new System.Windows.Forms.Padding(7);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(268, 74);
            this.sortButton.TabIndex = 3;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = false;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.generateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.generateButton.FlatAppearance.BorderSize = 2;
            this.generateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.generateButton.Location = new System.Drawing.Point(16, 16);
            this.generateButton.Margin = new System.Windows.Forms.Padding(7);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(268, 74);
            this.generateButton.TabIndex = 4;
            this.generateButton.Text = "Generate File";
            this.generateButton.UseVisualStyleBackColor = false;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // sortParallelButton
            // 
            this.sortParallelButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sortParallelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sortParallelButton.FlatAppearance.BorderSize = 2;
            this.sortParallelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortParallelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sortParallelButton.Location = new System.Drawing.Point(328, 223);
            this.sortParallelButton.Margin = new System.Windows.Forms.Padding(7);
            this.sortParallelButton.Name = "sortParallelButton";
            this.sortParallelButton.Size = new System.Drawing.Size(268, 74);
            this.sortParallelButton.TabIndex = 5;
            this.sortParallelButton.Text = "Parallel Sort";
            this.sortParallelButton.UseVisualStyleBackColor = false;
            this.sortParallelButton.Click += new System.EventHandler(this.sortParallelButton_Click);
            // 
            // asyncTaskButton
            // 
            this.asyncTaskButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.asyncTaskButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.asyncTaskButton.FlatAppearance.BorderSize = 2;
            this.asyncTaskButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.asyncTaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.asyncTaskButton.Location = new System.Drawing.Point(330, 348);
            this.asyncTaskButton.Margin = new System.Windows.Forms.Padding(7);
            this.asyncTaskButton.Name = "asyncTaskButton";
            this.asyncTaskButton.Size = new System.Drawing.Size(268, 101);
            this.asyncTaskButton.TabIndex = 6;
            this.asyncTaskButton.Text = "Async mod. File";
            this.asyncTaskButton.UseVisualStyleBackColor = false;
            this.asyncTaskButton.Click += new System.EventHandler(this.asyncTaskButton_Click);
            // 
            // taskButton
            // 
            this.taskButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.taskButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.taskButton.FlatAppearance.BorderSize = 2;
            this.taskButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskButton.Location = new System.Drawing.Point(18, 349);
            this.taskButton.Margin = new System.Windows.Forms.Padding(7);
            this.taskButton.Name = "taskButton";
            this.taskButton.Size = new System.Drawing.Size(268, 101);
            this.taskButton.TabIndex = 7;
            this.taskButton.Text = "Modify file";
            this.taskButton.UseVisualStyleBackColor = false;
            this.taskButton.Click += new System.EventHandler(this.taskButton_Click);
            // 
            // myTextBox1
            // 
            this.myTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.myTextBox1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myTextBox1.Hint = "array size";
            this.myTextBox1.Location = new System.Drawing.Point(328, 25);
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.Size = new System.Drawing.Size(270, 56);
            this.myTextBox1.TabIndex = 8;
            this.myTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SortFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1272, 465);
            this.Controls.Add(this.myTextBox1);
            this.Controls.Add(this.taskButton);
            this.Controls.Add(this.asyncTaskButton);
            this.Controls.Add(this.sortParallelButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.addFileButton);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "SortFileForm";
            this.Text = "SortFileForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button sortParallelButton;
        private System.Windows.Forms.Button asyncTaskButton;
        private System.Windows.Forms.Button taskButton;
        private MyTextBox myTextBox1;
    }
}

