using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        }


        /// <summary>
        /// 点击选择文件按钮
        /// </summary>
        private void OnClick_SelectFiles(object sender, EventArgs e)
        {
            dialog_selectFile.InitialDirectory = MainConfig.Ins.lastSelectExcelPath;
            dialog_selectFile.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            DialogResult result = dialog_selectFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                // 清空之前选择的文件
                lsBox_selectedFiles.Items.Clear();

                // 更新最近一次打开的目录
                int lastIndex = dialog_selectFile.FileName.LastIndexOf("\\");
                MainConfig.Ins.lastSelectExcelPath = dialog_selectFile.FileName.Substring(0, lastIndex);

                // 将文件路径添加到列表
                for (int i = 0; i < dialog_selectFile.FileNames.Length; i++)
                {
                    string fileName = dialog_selectFile.FileNames[i];
                    lsBox_selectedFiles.Items.Add(fileName);
                }
            }
        }

        /// <summary>
        /// 选择字节文件输出目录点击
        /// </summary>
        private void OnClick_SelectByteFileOutputDir(object sender, EventArgs e)
        {
            MainConfig.Ins.byteFileOutputDir = OpenSelectFolderDialog(MainConfig.Ins.byteFileOutputDir);
            lab_byteFileOutputDir.Text = MainConfig.Ins.byteFileOutputDir;
        }

        /// <summary>
        /// 选择日志输出目录
        /// </summary>
        private void OnClick_SelectLogOutputDir(object sender, EventArgs e)
        {
            MainConfig.Ins.logOutputDir = OpenSelectFolderDialog(MainConfig.Ins.logOutputDir);
            lab_logOutputDir.Text = MainConfig.Ins.logOutputDir;
        }

        /// <summary>
        /// 点击生成/导出 按钮
        /// </summary>
        private void OnClick_Generte(object sender, EventArgs e)
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

    }
}
