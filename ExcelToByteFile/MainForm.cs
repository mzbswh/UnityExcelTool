using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ExcelToByteFile
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitShow();
        }

        private void InitShow()
        {
            byteFileOutputDir.Text = GlobalConfig.Ins.byteFileOutputDir;
            codeFileOutputDir.Text = GlobalConfig.Ins.codeFileOutputDir;
            typeNullIsNote.Checked = GlobalConfig.Ins.typeNullIsNoteCol;
            defaultSkip.Value = GlobalConfig.Ins.skipRowBeginRead;
            autoCompletion.Checked = GlobalConfig.Ins.autoCompletion;
            autoCompletionVal.Text = GlobalConfig.Ins.autoCompletionVal;
            commetInFirstRow.Checked = GlobalConfig.Ins.commentInFirstRow;
            onlyOneSheet.Checked = GlobalConfig.Ins.onlyOneSheet;
        }

        /// <summary>
        /// 点击选择文件按钮
        /// </summary>
        private void OnClick_SelectFiles(object sender, EventArgs e)
        {
            dialog_selectFile.InitialDirectory = GlobalConfig.Ins.lastSelectExcelPath;
            dialog_selectFile.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            DialogResult result = dialog_selectFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                // 清空之前选择的文件
                lsBox_selectedFiles.Items.Clear();

                // 更新最近一次打开的目录
                int lastIndex = dialog_selectFile.FileName.LastIndexOf("\\");
                GlobalConfig.Ins.lastSelectExcelPath = dialog_selectFile.FileName.Substring(0, lastIndex);
                // 将文件路径添加到列表
                for (int i = 0; i < dialog_selectFile.FileNames.Length; i++)
                {
                    string fileName = dialog_selectFile.FileNames[i];
                    lsBox_selectedFiles.Items.Add(fileName);
                }
            }
        }

        /// <summary>
        /// 点击选择文件夹
        /// </summary>
        private void OnClick_SelectFileFolder(object sender, EventArgs e)
        {
            dialog_selectFolder.SelectedPath = GlobalConfig.Ins.lastSelectExcelPath;
            DialogResult result = dialog_selectFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                // 清空之前选择的文件
                lsBox_selectedFiles.Items.Clear();

                // 更新最近一次打开的目录
                GlobalConfig.Ins.lastSelectExcelPath = dialog_selectFolder.SelectedPath;

                // 将文件路径添加到列表
                DirectoryInfo dInfo = new DirectoryInfo(dialog_selectFolder.SelectedPath);
                foreach (var file in dInfo.GetFiles())
                {
                    if (file.Extension == ".xlsx" || file.Extension == ".xls")
                    {
                        lsBox_selectedFiles.Items.Add(file.FullName);
                    }
                }
            }
        }

        /// <summary>
        /// 选择字节文件输出目录点击
        /// </summary>
        private void OnClick_SelectByteFileOutputDir(object sender, EventArgs e)
        {
            GlobalConfig.Ins.byteFileOutputDir = OpenSelectFolderDialog(GlobalConfig.Ins.byteFileOutputDir);
            byteFileOutputDir.Text = GlobalConfig.Ins.byteFileOutputDir;
        }

        /// <summary>
        /// 选择代码文件输出路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick_SelectCodeFileOutputDir(object sender, EventArgs e)
        {
            GlobalConfig.Ins.codeFileOutputDir = OpenSelectFolderDialog(GlobalConfig.Ins.byteFileOutputDir);
            codeFileOutputDir.Text = GlobalConfig.Ins.codeFileOutputDir;
        }

        /// <summary>
        /// 点击生成/导出 按钮
        /// </summary>
        private void OnClick_Generate(object sender, EventArgs e)
        {
            // 获取选择的Excel文件列表
            List<string> fileList = new List<string>();
            for (int i = 0; i < lsBox_selectedFiles.Items.Count; i++)
            {
                string filePath = (string)lsBox_selectedFiles.Items[i];
                fileList.Add(filePath);
            }

            // 导出Excel文件列表
            ExportMgr.Export(fileList);

            MessageBox.Show("导表完成.");
        }

        /// <summary>
        /// 打开选择目录对话框
        /// </summary>
        /// <param name="selectPath">初始显示目录</param>
        private string OpenSelectFolderDialog(string selectPath)
        {
            dialog_selectFolder.SelectedPath = selectPath;

            DialogResult result = dialog_selectFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                return dialog_selectFolder.SelectedPath;
            }
            return selectPath;
        }
        
        public void LogMessage(string msg, Color col)
        {
            if (!string.IsNullOrEmpty(logTextBox.Text)) msg = Environment.NewLine + msg;
            logTextBox.SelectionStart = logTextBox.TextLength;
            logTextBox.SelectionLength = 0;
            logTextBox.SelectionColor = col;
            logTextBox.AppendText(msg);
            logTextBox.SelectionColor = logTextBox.ForeColor;
            logTextBox.Select(logTextBox.Text.Length, 0);
            logTextBox.ScrollToCaret();
        }

        private void typeNullIsNote_CheckedChanged(object sender, EventArgs e)
        {
            GlobalConfig.Ins.typeNullIsNoteCol = typeNullIsNote.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GlobalConfig.Ins.skipRowBeginRead = (int)defaultSkip.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GlobalConfig.Ins.autoCompletion = autoCompletion.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GlobalConfig.Ins.autoCompletionVal = autoCompletionVal.Text;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            GlobalConfig.Ins.commentInFirstRow = commetInFirstRow.Checked;
        }

        private void onlyOneSheet_CheckedChanged(object sender, EventArgs e)
        {
            GlobalConfig.Ins.onlyOneSheet = onlyOneSheet.Checked;
        }

        private void intro_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Excel必须设置id列\n" +
                "2. 列表填写格式 xx,xx,xx 逗号为分隔符\n" +
                "3. 字典填写格式 {key1, val1}, {k2, v2}\n" +
                "4. 列表或字典里各元素及分隔符间可加任意长度空格\n" +
                "5. 列表或字典里的字符串需要加\"\", 不加的话会删除前后空白\n" +
                "6. 列表或字典里的字符串如果在双引号间，则认为字符串的值为引号间的元素\n" +
                "7. 列表或字典里的字符串，如果有 ,(英文逗号) 需要在前面加 \\ 符号\n" +
                "8. 列表或字典里的元素类型不可为列表或字典，只能为简单类型\n" +
                "9. 变量类型不区分大小写，自动忽略空格\n\n" +
                "支持类型：\n" +
                "   bool, sbyte, byte, ushort, short, uint, int\n" +
                "   ulong long float double list dict\n\n" +
                "命令行参数：(参数与参数值间为空格，先读取之前配置，再应用参数)\n" +
                "   -e excel目录\n" +
                "   -o byte文件输出路径\n" +
                "   -c 代码文件输出路径\n");
        }
    }
}
