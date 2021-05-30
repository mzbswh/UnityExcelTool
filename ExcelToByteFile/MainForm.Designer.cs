
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_selectFiles = new System.Windows.Forms.Button();
            this.dialog_selectFile = new System.Windows.Forms.OpenFileDialog();
            this.lsBox_selectedFiles = new System.Windows.Forms.ListBox();
            this.btn_generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_selectByteFileOutputDir = new System.Windows.Forms.Button();
            this.byteFileOutputDir = new System.Windows.Forms.Label();
            this.dialog_selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.defaultSkip)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_selectFiles
            // 
            this.btn_selectFiles.Location = new System.Drawing.Point(7, 26);
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
            this.lsBox_selectedFiles.FormattingEnabled = true;
            this.lsBox_selectedFiles.ItemHeight = 21;
            this.lsBox_selectedFiles.Location = new System.Drawing.Point(168, 14);
            this.lsBox_selectedFiles.Name = "lsBox_selectedFiles";
            this.lsBox_selectedFiles.Size = new System.Drawing.Size(594, 298);
            this.lsBox_selectedFiles.TabIndex = 1;
            // 
            // btn_generate
            // 
            this.btn_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_generate.Location = new System.Drawing.Point(583, 610);
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
            this.label1.Location = new System.Drawing.Point(7, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "byte文件输出路径：";
            // 
            // btn_selectByteFileOutputDir
            // 
            this.btn_selectByteFileOutputDir.Location = new System.Drawing.Point(168, 328);
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
            this.byteFileOutputDir.Location = new System.Drawing.Point(254, 333);
            this.byteFileOutputDir.Name = "byteFileOutputDir";
            this.byteFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.byteFileOutputDir.TabIndex = 5;
            this.byteFileOutputDir.Text = "输出路径";
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(12, 509);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(542, 190);
            this.logTextBox.TabIndex = 10;
            this.logTextBox.Text = "";
            // 
            // typeNullIsNote
            // 
            this.typeNullIsNote.AutoSize = true;
            this.typeNullIsNote.Location = new System.Drawing.Point(12, 404);
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
            this.defaultSkip.Location = new System.Drawing.Point(389, 469);
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
            this.label2.Location = new System.Drawing.Point(12, 471);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(371, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "读取头部固定3行数据后，默认跳过行数不读取数据";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 93);
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
            this.autoCompletion.Location = new System.Drawing.Point(7, 435);
            this.autoCompletion.Name = "autoCompletion";
            this.autoCompletion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
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
            this.label3.Location = new System.Drawing.Point(275, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "填充值：";
            // 
            // autoCompletionVal
            // 
            this.autoCompletionVal.Location = new System.Drawing.Point(337, 433);
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
            this.label4.Location = new System.Drawing.Point(7, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 21);
            this.label4.TabIndex = 18;
            this.label4.Text = "代码文件输出路径：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 366);
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
            this.codeFileOutputDir.Location = new System.Drawing.Point(254, 371);
            this.codeFileOutputDir.Name = "codeFileOutputDir";
            this.codeFileOutputDir.Size = new System.Drawing.Size(74, 21);
            this.codeFileOutputDir.TabIndex = 20;
            this.codeFileOutputDir.Text = "输出路径";
            // 
            // commetInFirstRow
            // 
            this.commetInFirstRow.AutoSize = true;
            this.commetInFirstRow.Location = new System.Drawing.Point(271, 404);
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
            this.onlyOneSheet.Location = new System.Drawing.Point(594, 404);
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
            this.intro.Location = new System.Drawing.Point(35, 167);
            this.intro.Name = "intro";
            this.intro.Size = new System.Drawing.Size(87, 32);
            this.intro.TabIndex = 23;
            this.intro.Text = "说明";
            this.intro.UseVisualStyleBackColor = true;
            this.intro.Click += new System.EventHandler(this.intro_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 711);
            this.Controls.Add(this.intro);
            this.Controls.Add(this.onlyOneSheet);
            this.Controls.Add(this.commetInFirstRow);
            this.Controls.Add(this.codeFileOutputDir);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.autoCompletionVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.autoCompletion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.defaultSkip);
            this.Controls.Add(this.typeNullIsNote);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.byteFileOutputDir);
            this.Controls.Add(this.btn_selectByteFileOutputDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.lsBox_selectedFiles);
            this.Controls.Add(this.btn_selectFiles);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 750);
            this.Name = "MainForm";
            this.Text = "Excel转字节文件";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.defaultSkip)).EndInit();
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
        private System.Windows.Forms.Label byteFileOutputDir;
        private System.Windows.Forms.FolderBrowserDialog dialog_selectFolder;
        private System.Windows.Forms.RichTextBox logTextBox;
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
    }
}