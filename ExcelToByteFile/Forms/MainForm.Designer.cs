
namespace ExcelToByteFile
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_selectFiles = new System.Windows.Forms.Button();
            this.dialog_selectFile = new System.Windows.Forms.OpenFileDialog();
            this.lsBox_selectedFiles = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除 = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_selectByteFileOutputDir = new System.Windows.Forms.Button();
            this.byteFileOutputDir = new System.Windows.Forms.Label();
            this.dialog_selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.codeFileOutputDir = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressLab = new System.Windows.Forms.Label();
            this.selectStructDir = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.generateStructCs = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.selectStructDir.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_selectFiles
            // 
            this.btn_selectFiles.Location = new System.Drawing.Point(10, 12);
            this.btn_selectFiles.Name = "btn_selectFiles";
            this.btn_selectFiles.Size = new System.Drawing.Size(150, 50);
            this.btn_selectFiles.TabIndex = 0;
            this.btn_selectFiles.Text = "选择文件";
            this.btn_selectFiles.UseVisualStyleBackColor = true;
            this.btn_selectFiles.Click += new System.EventHandler(this.OnClick_SelectFiles);
            // 
            // dialog_selectFile
            // 
            this.dialog_selectFile.Multiselect = true;
            // 
            // lsBox_selectedFiles
            // 
            this.lsBox_selectedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsBox_selectedFiles.ContextMenuStrip = this.contextMenuStrip1;
            this.lsBox_selectedFiles.FormattingEnabled = true;
            this.lsBox_selectedFiles.ItemHeight = 21;
            this.lsBox_selectedFiles.Location = new System.Drawing.Point(178, 0);
            this.lsBox_selectedFiles.Name = "lsBox_selectedFiles";
            this.lsBox_selectedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsBox_selectedFiles.Size = new System.Drawing.Size(582, 382);
            this.lsBox_selectedFiles.TabIndex = 1;
            this.lsBox_selectedFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsBox_selectedFiles_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // 删除
            // 
            this.删除.Name = "删除";
            this.删除.Size = new System.Drawing.Size(136, 22);
            this.删除.Text = "移除选中项";
            // 
            // btn_generate
            // 
            this.btn_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_generate.Location = new System.Drawing.Point(596, 556);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(150, 50);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "生成";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.OnClick_Generate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "byte文件输出路径：";
            // 
            // btn_selectByteFileOutputDir
            // 
            this.btn_selectByteFileOutputDir.Location = new System.Drawing.Point(157, 403);
            this.btn_selectByteFileOutputDir.Name = "btn_selectByteFileOutputDir";
            this.btn_selectByteFileOutputDir.Size = new System.Drawing.Size(80, 30);
            this.btn_selectByteFileOutputDir.TabIndex = 4;
            this.btn_selectByteFileOutputDir.Text = "选择";
            this.btn_selectByteFileOutputDir.UseVisualStyleBackColor = true;
            this.btn_selectByteFileOutputDir.Click += new System.EventHandler(this.OnClick_SelectByteFileOutputDir);
            // 
            // byteFileOutputDir
            // 
            this.byteFileOutputDir.AutoSize = true;
            this.byteFileOutputDir.ForeColor = System.Drawing.Color.DodgerBlue;
            this.byteFileOutputDir.Location = new System.Drawing.Point(243, 408);
            this.byteFileOutputDir.Name = "byteFileOutputDir";
            this.byteFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.byteFileOutputDir.TabIndex = 5;
            this.byteFileOutputDir.Text = "输出路径";
            this.byteFileOutputDir.MouseHover += new System.EventHandler(this.byteFileOutputDir_MouseHover);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 50);
            this.button1.TabIndex = 14;
            this.button1.Text = "选择文件夹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnClick_SelectFileFolder);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 446);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 21);
            this.label4.TabIndex = 18;
            this.label4.Text = "代码文件输出路径：";
            this.toolTip1.SetToolTip(this.label4, "代码文件生成路径");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(157, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 19;
            this.button2.Text = "选择";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnClick_SelectCodeFileOutputDir);
            // 
            // codeFileOutputDir
            // 
            this.codeFileOutputDir.AutoSize = true;
            this.codeFileOutputDir.ForeColor = System.Drawing.Color.DodgerBlue;
            this.codeFileOutputDir.Location = new System.Drawing.Point(243, 446);
            this.codeFileOutputDir.Name = "codeFileOutputDir";
            this.codeFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.codeFileOutputDir.TabIndex = 20;
            this.codeFileOutputDir.Text = "输出路径";
            this.codeFileOutputDir.MouseHover += new System.EventHandler(this.codeFileOutputDir_MouseHover);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.progressBar1.Location = new System.Drawing.Point(10, 41);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(719, 25);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 24;
            this.progressBar1.Value = 30;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // progressLab
            // 
            this.progressLab.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressLab.AutoSize = true;
            this.progressLab.Location = new System.Drawing.Point(271, 15);
            this.progressLab.Name = "progressLab";
            this.progressLab.Size = new System.Drawing.Size(169, 21);
            this.progressLab.TabIndex = 25;
            this.progressLab.Text = "正在生成：sada.bytes";
            this.progressLab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressLab.Click += new System.EventHandler(this.progressLab_Click);
            // 
            // selectStructDir
            // 
            this.selectStructDir.Controls.Add(this.label2);
            this.selectStructDir.Controls.Add(this.generateStructCs);
            this.selectStructDir.Controls.Add(this.btn_selectFiles);
            this.selectStructDir.Controls.Add(this.button1);
            this.selectStructDir.Controls.Add(this.btn_generate);
            this.selectStructDir.Controls.Add(this.lsBox_selectedFiles);
            this.selectStructDir.Controls.Add(this.label1);
            this.selectStructDir.Controls.Add(this.codeFileOutputDir);
            this.selectStructDir.Controls.Add(this.btn_selectByteFileOutputDir);
            this.selectStructDir.Controls.Add(this.button2);
            this.selectStructDir.Controls.Add(this.byteFileOutputDir);
            this.selectStructDir.Controls.Add(this.label4);
            this.selectStructDir.Location = new System.Drawing.Point(12, 12);
            this.selectStructDir.Name = "selectStructDir";
            this.selectStructDir.Size = new System.Drawing.Size(760, 620);
            this.selectStructDir.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(3, 476);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(754, 28);
            this.label2.TabIndex = 30;
            this.label2.Text = "※注意：生成byte文件和代码文件会先清空选择的目录，所以确保输出路径不包含其他重要文件！！！";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // generateStructCs
            // 
            this.generateStructCs.AutoSize = true;
            this.generateStructCs.Location = new System.Drawing.Point(3, 507);
            this.generateStructCs.Name = "generateStructCs";
            this.generateStructCs.Size = new System.Drawing.Size(141, 25);
            this.generateStructCs.TabIndex = 29;
            this.generateStructCs.Text = "生成结构信息类";
            this.toolTip1.SetToolTip(this.generateStructCs, "行数据的的结构体定义，可更方便取得数据");
            this.generateStructCs.UseVisualStyleBackColor = true;
            this.generateStructCs.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_3);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.progressLab);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(12, 638);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 70);
            this.panel2.TabIndex = 27;
            this.panel2.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 711);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.selectStructDir);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 750);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unity导表工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.selectStructDir.ResumeLayout(false);
            this.selectStructDir.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_selectFiles;
        private System.Windows.Forms.OpenFileDialog dialog_selectFile;
        private System.Windows.Forms.ListBox lsBox_selectedFiles;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_selectByteFileOutputDir;
        private System.Windows.Forms.Label byteFileOutputDir;
        private System.Windows.Forms.FolderBrowserDialog dialog_selectFolder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label codeFileOutputDir;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressLab;
        private System.Windows.Forms.Panel selectStructDir;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox generateStructCs;
        private System.Windows.Forms.Label label2;
    }
}