
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
            this.typeNullIsNote = new System.Windows.Forms.CheckBox();
            this.defaultSkip = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.autoCompletion = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.autoCompletionVal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.codeFileOutputDir = new System.Windows.Forms.Label();
            this.commetInFirstRow = new System.Windows.Forms.CheckBox();
            this.onlyOneSheet = new System.Windows.Forms.CheckBox();
            this.intro = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressLab = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.idColName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultSkip)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_selectFiles
            // 
            this.btn_selectFiles.Location = new System.Drawing.Point(22, 12);
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
            this.lsBox_selectedFiles.Location = new System.Drawing.Point(217, 0);
            this.lsBox_selectedFiles.Name = "lsBox_selectedFiles";
            this.lsBox_selectedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsBox_selectedFiles.Size = new System.Drawing.Size(543, 298);
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
            this.btn_generate.Location = new System.Drawing.Point(566, 425);
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
            this.label1.Location = new System.Drawing.Point(0, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "byte文件输出路径：";
            // 
            // btn_selectByteFileOutputDir
            // 
            this.btn_selectByteFileOutputDir.Location = new System.Drawing.Point(157, 325);
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
            this.byteFileOutputDir.Location = new System.Drawing.Point(243, 330);
            this.byteFileOutputDir.Name = "byteFileOutputDir";
            this.byteFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.byteFileOutputDir.TabIndex = 5;
            this.byteFileOutputDir.Text = "输出路径";
            // 
            // typeNullIsNote
            // 
            this.typeNullIsNote.AutoSize = true;
            this.typeNullIsNote.Location = new System.Drawing.Point(10, 439);
            this.typeNullIsNote.Name = "typeNullIsNote";
            this.typeNullIsNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.typeNullIsNote.Size = new System.Drawing.Size(253, 25);
            this.typeNullIsNote.TabIndex = 11;
            this.typeNullIsNote.Text = "类型单元格为空时认为是注释列";
            this.typeNullIsNote.UseVisualStyleBackColor = true;
            this.typeNullIsNote.CheckedChanged += new System.EventHandler(this.typeNullIsNote_CheckedChanged);
            // 
            // defaultSkip
            // 
            this.defaultSkip.Location = new System.Drawing.Point(377, 535);
            this.defaultSkip.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.defaultSkip.Name = "defaultSkip";
            this.defaultSkip.ReadOnly = true;
            this.defaultSkip.Size = new System.Drawing.Size(63, 28);
            this.defaultSkip.TabIndex = 12;
            this.defaultSkip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.defaultSkip.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 537);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(375, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "读取头部固定3行数据后，默认跳过行数不读取数据:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 50);
            this.button1.TabIndex = 14;
            this.button1.Text = "选择文件夹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnClick_SelectFileFolder);
            // 
            // autoCompletion
            // 
            this.autoCompletion.AutoSize = true;
            this.autoCompletion.Location = new System.Drawing.Point(10, 499);
            this.autoCompletion.Name = "autoCompletion";
            this.autoCompletion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.autoCompletion.Size = new System.Drawing.Size(247, 25);
            this.autoCompletion.TabIndex = 15;
            this.autoCompletion.Text = "自动填充空单元格(仅数值类型)";
            this.autoCompletion.UseVisualStyleBackColor = true;
            this.autoCompletion.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(263, 500);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "填充值：";
            // 
            // autoCompletionVal
            // 
            this.autoCompletionVal.Location = new System.Drawing.Point(335, 497);
            this.autoCompletionVal.Name = "autoCompletionVal";
            this.autoCompletionVal.Size = new System.Drawing.Size(100, 28);
            this.autoCompletionVal.TabIndex = 17;
            this.autoCompletionVal.Text = "0";
            this.autoCompletionVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.autoCompletionVal.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 21);
            this.label4.TabIndex = 18;
            this.label4.Text = "代码文件输出路径：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(157, 363);
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
            this.codeFileOutputDir.Location = new System.Drawing.Point(243, 368);
            this.codeFileOutputDir.Name = "codeFileOutputDir";
            this.codeFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.codeFileOutputDir.TabIndex = 20;
            this.codeFileOutputDir.Text = "输出路径";
            // 
            // commetInFirstRow
            // 
            this.commetInFirstRow.AutoSize = true;
            this.commetInFirstRow.Location = new System.Drawing.Point(10, 408);
            this.commetInFirstRow.Name = "commetInFirstRow";
            this.commetInFirstRow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.commetInFirstRow.Size = new System.Drawing.Size(317, 25);
            this.commetInFirstRow.TabIndex = 21;
            this.commetInFirstRow.Text = "变量注释在第一行（不勾选默认第三行）";
            this.commetInFirstRow.UseVisualStyleBackColor = true;
            this.commetInFirstRow.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // onlyOneSheet
            // 
            this.onlyOneSheet.AutoSize = true;
            this.onlyOneSheet.Location = new System.Drawing.Point(10, 470);
            this.onlyOneSheet.Name = "onlyOneSheet";
            this.onlyOneSheet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.onlyOneSheet.Size = new System.Drawing.Size(168, 25);
            this.onlyOneSheet.TabIndex = 22;
            this.onlyOneSheet.Text = "只读取第一个Sheet";
            this.onlyOneSheet.UseVisualStyleBackColor = true;
            this.onlyOneSheet.CheckedChanged += new System.EventHandler(this.onlyOneSheet_CheckedChanged);
            // 
            // intro
            // 
            this.intro.Location = new System.Drawing.Point(48, 140);
            this.intro.Name = "intro";
            this.intro.Size = new System.Drawing.Size(87, 32);
            this.intro.TabIndex = 23;
            this.intro.Text = "说明";
            this.intro.UseVisualStyleBackColor = true;
            this.intro.Click += new System.EventHandler(this.intro_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.progressBar1.Location = new System.Drawing.Point(10, 48);
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
            this.progressLab.Location = new System.Drawing.Point(271, 14);
            this.progressLab.Name = "progressLab";
            this.progressLab.Size = new System.Drawing.Size(169, 21);
            this.progressLab.TabIndex = 25;
            this.progressLab.Text = "正在生成：sada.bytes";
            this.progressLab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressLab.Click += new System.EventHandler(this.progressLab_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.idColName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.commetInFirstRow);
            this.panel1.Controls.Add(this.btn_selectFiles);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_generate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.defaultSkip);
            this.panel1.Controls.Add(this.autoCompletionVal);
            this.panel1.Controls.Add(this.onlyOneSheet);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.intro);
            this.panel1.Controls.Add(this.autoCompletion);
            this.panel1.Controls.Add(this.lsBox_selectedFiles);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.codeFileOutputDir);
            this.panel1.Controls.Add(this.btn_selectByteFileOutputDir);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.typeNullIsNote);
            this.panel1.Controls.Add(this.byteFileOutputDir);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 604);
            this.panel1.TabIndex = 26;
            // 
            // idColName
            // 
            this.idColName.Location = new System.Drawing.Point(420, 568);
            this.idColName.Name = "idColName";
            this.idColName.Size = new System.Drawing.Size(208, 28);
            this.idColName.TabIndex = 25;
            this.idColName.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 571);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(415, 21);
            this.label5.TabIndex = 24;
            this.label5.Text = "主列列名（默认为 id 必须是int且不能一个sheet内重复）";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.progressLab);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(12, 622);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 81);
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
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 750);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel转字节文件";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultSkip)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.CheckBox typeNullIsNote;
        private System.Windows.Forms.NumericUpDown defaultSkip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox autoCompletion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox autoCompletionVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label codeFileOutputDir;
        private System.Windows.Forms.CheckBox commetInFirstRow;
        private System.Windows.Forms.CheckBox onlyOneSheet;
        private System.Windows.Forms.Button intro;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressLab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox idColName;
    }
}