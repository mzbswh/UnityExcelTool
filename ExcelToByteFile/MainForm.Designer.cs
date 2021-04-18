
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
            this.btn_selectFiles = new System.Windows.Forms.Button();
            this.dialog_selectFile = new System.Windows.Forms.OpenFileDialog();
            this.lsBox_selectedFiles = new System.Windows.Forms.ListBox();
            this.btn_generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_selectByteFileOutputDir = new System.Windows.Forms.Button();
            this.lab_byteFileOutputDir = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_selectLogOutputDir = new System.Windows.Forms.Button();
            this.lab_logOutputDir = new System.Windows.Forms.Label();
            this.dialog_selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_selectFiles
            // 
            this.btn_selectFiles.Location = new System.Drawing.Point(12, 28);
            this.btn_selectFiles.Name = "btn_selectFiles";
            this.btn_selectFiles.Size = new System.Drawing.Size(150, 50);
            this.btn_selectFiles.TabIndex = 0;
            this.btn_selectFiles.Text = "选择文件";
            this.btn_selectFiles.UseVisualStyleBackColor = true;
            this.btn_selectFiles.Click += new System.EventHandler(this.OnClick_SelectFiles);
            // 
            // lsBox_selectedFiles
            // 
            this.lsBox_selectedFiles.FormattingEnabled = true;
            this.lsBox_selectedFiles.ItemHeight = 21;
            this.lsBox_selectedFiles.Location = new System.Drawing.Point(168, 14);
            this.lsBox_selectedFiles.Name = "lsBox_selectedFiles";
            this.lsBox_selectedFiles.Size = new System.Drawing.Size(636, 256);
            this.lsBox_selectedFiles.TabIndex = 1;
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(625, 617);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(150, 50);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "生成";
            this.btn_generate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "byte文件输出路径：";
            // 
            // btn_selectByteFileOutputDir
            // 
            this.btn_selectByteFileOutputDir.Location = new System.Drawing.Point(168, 337);
            this.btn_selectByteFileOutputDir.Name = "btn_selectByteFileOutputDir";
            this.btn_selectByteFileOutputDir.Size = new System.Drawing.Size(80, 30);
            this.btn_selectByteFileOutputDir.TabIndex = 4;
            this.btn_selectByteFileOutputDir.Text = "选择";
            this.btn_selectByteFileOutputDir.UseVisualStyleBackColor = true;
            this.btn_selectByteFileOutputDir.Click += new System.EventHandler(this.OnClick_SelectByteFileOutputDir);
            // 
            // lab_byteFileOutputDir
            // 
            this.lab_byteFileOutputDir.AutoSize = true;
            this.lab_byteFileOutputDir.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lab_byteFileOutputDir.Location = new System.Drawing.Point(254, 342);
            this.lab_byteFileOutputDir.Name = "lab_byteFileOutputDir";
            this.lab_byteFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.lab_byteFileOutputDir.TabIndex = 5;
            this.lab_byteFileOutputDir.Text = "输出路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "log日志输出路径：";
            // 
            // btn_selectLogOutputDir
            // 
            this.btn_selectLogOutputDir.Location = new System.Drawing.Point(168, 378);
            this.btn_selectLogOutputDir.Name = "btn_selectLogOutputDir";
            this.btn_selectLogOutputDir.Size = new System.Drawing.Size(80, 30);
            this.btn_selectLogOutputDir.TabIndex = 7;
            this.btn_selectLogOutputDir.Text = "选择";
            this.btn_selectLogOutputDir.UseVisualStyleBackColor = true;
            this.btn_selectLogOutputDir.Click += new System.EventHandler(this.OnClick_SelectLogOutputDir);
            // 
            // lab_logOutputDir
            // 
            this.lab_logOutputDir.AutoSize = true;
            this.lab_logOutputDir.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lab_logOutputDir.Location = new System.Drawing.Point(254, 383);
            this.lab_logOutputDir.Name = "lab_logOutputDir";
            this.lab_logOutputDir.Size = new System.Drawing.Size(74, 21);
            this.lab_logOutputDir.TabIndex = 8;
            this.lab_logOutputDir.Text = "输出路径";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 718);
            this.Controls.Add(this.lab_logOutputDir);
            this.Controls.Add(this.btn_selectLogOutputDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lab_byteFileOutputDir);
            this.Controls.Add(this.btn_selectByteFileOutputDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.lsBox_selectedFiles);
            this.Controls.Add(this.btn_selectFiles);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Excel转字节文件";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_selectFiles;
        private System.Windows.Forms.OpenFileDialog dialog_selectFile;
        private System.Windows.Forms.ListBox lsBox_selectedFiles;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_selectByteFileOutputDir;
        private System.Windows.Forms.Label lab_byteFileOutputDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_selectLogOutputDir;
        private System.Windows.Forms.Label lab_logOutputDir;
        private System.Windows.Forms.FolderBrowserDialog dialog_selectFolder;
    }
}