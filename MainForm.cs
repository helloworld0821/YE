using DevExpress.XtraEditors;
using SCH.DAL;
using SCH.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SCH
{
    public partial class MainFrom : DevExpress.XtraEditors.XtraForm
    {
        public static double dblZRL = 0;
        public MainFrom()
        {
            InitializeComponent();
        }

        private void btnOpenSJLFile_Click(object sender, EventArgs e)
        {
            txtOpenSJLFile.Text = FileDialogHelper.Open("请选择文件", "全部文件(*.*)|*.*");
        }

        private void btnOpenDZ_Click(object sender, EventArgs e)
        {
            txtOpenDZ.Text = FileDialogHelper.Open("请选择文件", "全部文件(*.*)|*.*");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtOpenDZ.Text)) && (!string.IsNullOrEmpty(txtInput.Text)) && (!string.IsNullOrEmpty(txtOpenSJLFile.Text)))
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("正在处理");
                    splashScreenManager1.SetWaitFormDescription("请稍后...");
                    
                    ReadFilesDAL.ReadHD(txtOpenDZ.Text);
                    dblZRL = Convert.ToDouble(txtInput.Text);
                    ReadFilesDAL rd = new ReadFilesDAL();
                    rd.ReadSJL(txtOpenSJLFile.Text, txtOutput.Text);
                    splashScreenManager1.CloseWaitForm();
                }
                else
                {
                    XtraMessageBox.Show("请检查文本框是否输入正确");
                }
            
            }
            catch(Exception)
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

        private void MainFrom_Load(object sender, EventArgs e)
        {
            FrmImportPRT fip = new FrmImportPRT();
            fip.Show();
        }
    }
}
