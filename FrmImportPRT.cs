using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SCH.Utils;
using SCH.DAL;

namespace SCH
{
    public partial class FrmImportPRT : DevExpress.XtraEditors.XtraForm
    {
        public static double dblTimes = 0;
        public FrmImportPRT()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtOpenPRT.Text)) && (!string.IsNullOrEmpty(txtInput.Text)) && (!string.IsNullOrEmpty(txtOpenSJLFile.Text)))
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("正在处理");
                    splashScreenManager1.SetWaitFormDescription("请稍后...");
                    ReadPrtDAL rpd = new ReadPrtDAL();
                    dblTimes = Convert.ToDouble(txtInput.Text);
                    rpd.Print(txtOpenPRT.Text, txtOpenSJLFile.Text, txtOutput.Text);
                    splashScreenManager1.CloseWaitForm();
                }
                else
                {
                    XtraMessageBox.Show("请检查文本框是否输入正确");
                }

            }
            catch (Exception)
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            txtOutput.Text = FileDialogHelper.Save("文件另存为", "全部文件(*.*)|*.*") + ".INC";
        }

        private void btnOpenPRT_Click(object sender, EventArgs e)
        {
            txtOpenPRT.Text = FileDialogHelper.Open("请选择文件", "全部文件(*.*)|*.*");
        }

        private void btnOpenSJLFile_Click(object sender, EventArgs e)
        {
            txtOpenSJLFile.Text = FileDialogHelper.Open("请选择文件", "全部文件(*.*)|*.*");
        }
    }
}