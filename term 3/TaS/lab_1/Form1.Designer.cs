namespace lab1form
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.explorerInput = new System.Windows.Forms.TextBox();
            this.ExplorerWnds = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unzipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prev_Button = new System.Windows.Forms.Button();
            this.next_Button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filePreview = new System.Windows.Forms.TextBox();
            this.refresh_Button = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // explorerInput
            // 
            this.explorerInput.Location = new System.Drawing.Point(38, 56);
            this.explorerInput.Name = "explorerInput";
            this.explorerInput.Size = new System.Drawing.Size(709, 22);
            this.explorerInput.TabIndex = 1;
            // 
            // ExplorerWnds
            // 
            this.ExplorerWnds.ContextMenuStrip = this.contextMenuStrip1;
            this.ExplorerWnds.HideSelection = false;
            this.ExplorerWnds.Location = new System.Drawing.Point(38, 99);
            this.ExplorerWnds.Name = "ExplorerWnds";
            this.ExplorerWnds.Size = new System.Drawing.Size(369, 291);
            this.ExplorerWnds.TabIndex = 2;
            this.ExplorerWnds.UseCompatibleStateImageBehavior = false;
            this.ExplorerWnds.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            this.ExplorerWnds.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Explorer_Click);
            this.ExplorerWnds.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Explorer_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.zipToolStripMenuItem,
            this.unzipToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 148);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Enabled = false;
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.PasteToolStripMenuItem.Text = "Paste";
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            // 
            // zipToolStripMenuItem
            // 
            this.zipToolStripMenuItem.Name = "zipToolStripMenuItem";
            this.zipToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.zipToolStripMenuItem.Text = "Zip";
            this.zipToolStripMenuItem.Click += new System.EventHandler(this.ZipToolStripMenuItem_Click);
            // 
            // unzipToolStripMenuItem
            // 
            this.unzipToolStripMenuItem.Name = "unzipToolStripMenuItem";
            this.unzipToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.unzipToolStripMenuItem.Text = "Unzip";
            this.unzipToolStripMenuItem.Click += new System.EventHandler(this.UnzipToolStripMenuItem_Click);
            // 
            // prev_Button
            // 
            this.prev_Button.Enabled = false;
            this.prev_Button.Location = new System.Drawing.Point(38, 27);
            this.prev_Button.Name = "prev_Button";
            this.prev_Button.Size = new System.Drawing.Size(75, 23);
            this.prev_Button.TabIndex = 3;
            this.prev_Button.Text = "Previous";
            this.prev_Button.UseVisualStyleBackColor = true;
            this.prev_Button.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // next_Button
            // 
            this.next_Button.Enabled = false;
            this.next_Button.Location = new System.Drawing.Point(189, 27);
            this.next_Button.Name = "next_Button";
            this.next_Button.Size = new System.Drawing.Size(75, 23);
            this.next_Button.TabIndex = 4;
            this.next_Button.Text = "Next";
            this.next_Button.UseVisualStyleBackColor = true;
            this.next_Button.Click += new System.EventHandler(this.Next_Button_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(78, 24);
            this.toolStripMenuItem1.Text = "Explorer";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.folderToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.folderToolStripMenuItem.Text = "Folder";
            this.folderToolStripMenuItem.Click += new System.EventHandler(this.FolderToolStripMenuItem_Click);
            // 
            // filePreview
            // 
            this.filePreview.Location = new System.Drawing.Point(495, 99);
            this.filePreview.Multiline = true;
            this.filePreview.Name = "filePreview";
            this.filePreview.Size = new System.Drawing.Size(252, 291);
            this.filePreview.TabIndex = 6;
            // 
            // refresh_Button
            // 
            this.refresh_Button.Location = new System.Drawing.Point(414, 99);
            this.refresh_Button.Name = "refresh_Button";
            this.refresh_Button.Size = new System.Drawing.Size(75, 23);
            this.refresh_Button.TabIndex = 7;
            this.refresh_Button.Text = "Refresh";
            this.refresh_Button.UseVisualStyleBackColor = true;
            this.refresh_Button.Click += new System.EventHandler(this.Refresh_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.refresh_Button);
            this.Controls.Add(this.filePreview);
            this.Controls.Add(this.next_Button);
            this.Controls.Add(this.prev_Button);
            this.Controls.Add(this.ExplorerWnds);
            this.Controls.Add(this.explorerInput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox explorerInput;
        private System.Windows.Forms.ListView ExplorerWnds;
        private System.Windows.Forms.Button prev_Button;
        private System.Windows.Forms.Button next_Button;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
        private System.Windows.Forms.TextBox filePreview;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.Button refresh_Button;
        private System.Windows.Forms.ToolStripMenuItem zipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unzipToolStripMenuItem;
    }
}

