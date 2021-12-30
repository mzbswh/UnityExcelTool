using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

#pragma warning disable IDE1006

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
            //panel1.Enabled = false;
        }

        private void InitShow()
        {
            byteFileOutputDir.Text = GlobalConfig.Ins.byteFileOutputDir;
            codeFileOutputDir.Text = GlobalConfig.Ins.codeFileOutputDir;
            generateStructCs.Checked = GlobalConfig.Ins.generateStructInfoCode;
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

            StartExport(fileList.Count);


            Task t = Task.Run(() => ExportMgr.Export(fileList));
            t.ContinueWith(new Action<Task>((t) =>
            {
                MessageBox.Show("生成完成.");
                Invoke(new Action(EndExport));
            }));
            
            // 导出Excel文件列表
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

        private void intro_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. 类型单元格或一行（除前3行）第一个单元格包含 # 时为注释列或行\n" +
                "2. 列表填写格式 xx,xx,xx 逗号为分隔符\n" +
                "3. 字典填写格式 {key1, val1}, {k2, v2}\n" +
                "4. 列表或字典里各元素及分隔符间可加任意长度空格\n" +
                "5. 列表或字典里的字符串需要加\"\", 不加的话会删除前后空白\n" +
                "6. 列表或字典里的字符串如果在双引号间，则认为字符串的值为引号间的元素\n" +
                "7. 列表或字典里的字符串，如果有 ,(英文逗号) 需要在前面加 \\ 符号\n" +
                "8. 列表或字典里的元素类型不可为列表或字典，只能为简单类型\n" +
                "9. 变量类型不区分大小写，自动忽略空格\n" +
                "10. 主列用于解析时作为字典的key值，故不能重复且是值类型或string\n\n" +
                "支持类型：\n" +
                "   bool, sbyte, byte, ushort, short, uint, int\n" +
                "   ulong long float double list dict\n" +
                "   vector2 vector2Int vector3 vector3Int vector4 vector4Int\n\n" +
                "命令行参数：(参数与参数值间为空格，先读取之前配置，再应用参数)\n" +
                "   -e excel目录\n" +
                "   -o byte文件输出路径\n" +
                "   -c 代码文件输出路径\n");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void StartExport(int progressMax)
        {
            selectStructDir.Enabled = false;
            panel2.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = progressMax;
        }

        private void EndExport()
        {
            selectStructDir.Enabled = true;
            panel2.Visible = false;
        }

        public void SetProgress(int val, string str)
        {
            progressLab.Text = str;
            progressBar1.Value = val;
        }

        private void progressLab_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            object[] selected_objs = new object[lsBox_selectedFiles.SelectedItems.Count];
            lsBox_selectedFiles.SelectedItems.CopyTo(selected_objs, 0);
            foreach (object oval in selected_objs)
            {
                lsBox_selectedFiles.Items.Remove(oval);
            }
        }

        private void lsBox_selectedFiles_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lsBox_selectedFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                contextMenuStrip1_ItemClicked(null, null);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void byteFileOutputDir_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(byteFileOutputDir.Text, byteFileOutputDir);
        }

        private void codeFileOutputDir_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(codeFileOutputDir.Text, codeFileOutputDir);
        }

        private void checkBox1_CheckedChanged_3(object sender, EventArgs e)
        {
            GlobalConfig.Ins.generateStructInfoCode = generateStructCs.Checked;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
