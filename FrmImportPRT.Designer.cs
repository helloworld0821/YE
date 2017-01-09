namespace SCH
{
    partial class FrmImportPRT
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblOutput = new DevExpress.XtraEditors.LabelControl();
            this.txtOutput = new DevExpress.XtraEditors.TextEdit();
            this.btnOutput = new DevExpress.XtraEditors.SimpleButton();
            this.lblInput = new DevExpress.XtraEditors.LabelControl();
            this.txtInput = new DevExpress.XtraEditors.TextEdit();
            this.lblOpenPRT = new DevExpress.XtraEditors.LabelControl();
            this.lblOpenSJLFile = new DevExpress.XtraEditors.LabelControl();
            this.txtOpenSJLFile = new DevExpress.XtraEditors.TextEdit();
            this.btnOpenSJLFile = new DevExpress.XtraEditors.SimpleButton();
            this.txtOpenPRT = new DevExpress.XtraEditors.TextEdit();
            this.btnOpenPRT = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::SCH.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpenSJLFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpenPRT.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(516, 377);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.lblOutput);
            this.panelControl1.Controls.Add(this.txtOutput);
            this.panelControl1.Controls.Add(this.btnOutput);
            this.panelControl1.Controls.Add(this.lblInput);
            this.panelControl1.Controls.Add(this.txtInput);
            this.panelControl1.Controls.Add(this.lblOpenPRT);
            this.panelControl1.Controls.Add(this.lblOpenSJLFile);
            this.panelControl1.Controls.Add(this.txtOpenSJLFile);
            this.panelControl1.Controls.Add(this.btnOpenSJLFile);
            this.panelControl1.Controls.Add(this.txtOpenPRT);
            this.panelControl1.Controls.Add(this.btnOpenPRT);
            this.panelControl1.Location = new System.Drawing.Point(12, 33);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(580, 315);
            this.panelControl1.TabIndex = 15;
            // 
            // lblOutput
            // 
            this.lblOutput.Location = new System.Drawing.Point(24, 230);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(77, 14);
            this.lblOutput.TabIndex = 13;
            this.lblOutput.Text = "文件输出(.inc)";
            // 
            // txtOutput
            // 
            this.txtOutput.Enabled = false;
            this.txtOutput.Location = new System.Drawing.Point(98, 261);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(356, 20);
            this.txtOutput.TabIndex = 12;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(472, 258);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 11;
            this.btnOutput.Text = "打开";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // lblInput
            // 
            this.lblInput.Location = new System.Drawing.Point(24, 165);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(48, 14);
            this.lblInput.TabIndex = 10;
            this.lblInput.Text = "倍数输入";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(98, 195);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(356, 20);
            this.txtInput.TabIndex = 9;
            // 
            // lblOpenPRT
            // 
            this.lblOpenPRT.Location = new System.Drawing.Point(24, 94);
            this.lblOpenPRT.Name = "lblOpenPRT";
            this.lblOpenPRT.Size = new System.Drawing.Size(100, 14);
            this.lblOpenPRT.TabIndex = 8;
            this.lblOpenPRT.Text = "PRT文件选择(.prt)";
            // 
            // lblOpenSJLFile
            // 
            this.lblOpenSJLFile.Location = new System.Drawing.Point(24, 18);
            this.lblOpenSJLFile.Name = "lblOpenSJLFile";
            this.lblOpenSJLFile.Size = new System.Drawing.Size(113, 14);
            this.lblOpenSJLFile.TabIndex = 6;
            this.lblOpenSJLFile.Text = "数据流文件选择(.inc)";
            // 
            // txtOpenSJLFile
            // 
            this.txtOpenSJLFile.Enabled = false;
            this.txtOpenSJLFile.Location = new System.Drawing.Point(98, 50);
            this.txtOpenSJLFile.Name = "txtOpenSJLFile";
            this.txtOpenSJLFile.Size = new System.Drawing.Size(356, 20);
            this.txtOpenSJLFile.TabIndex = 2;
            // 
            // btnOpenSJLFile
            // 
            this.btnOpenSJLFile.Location = new System.Drawing.Point(472, 49);
            this.btnOpenSJLFile.Name = "btnOpenSJLFile";
            this.btnOpenSJLFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenSJLFile.TabIndex = 0;
            this.btnOpenSJLFile.Text = "打开";
            this.btnOpenSJLFile.Click += new System.EventHandler(this.btnOpenSJLFile_Click);
            // 
            // txtOpenPRT
            // 
            this.txtOpenPRT.Enabled = false;
            this.txtOpenPRT.Location = new System.Drawing.Point(98, 125);
            this.txtOpenPRT.Name = "txtOpenPRT";
            this.txtOpenPRT.Size = new System.Drawing.Size(356, 20);
            this.txtOpenPRT.TabIndex = 3;
            // 
            // btnOpenPRT
            // 
            this.btnOpenPRT.Location = new System.Drawing.Point(472, 122);
            this.btnOpenPRT.Name = "btnOpenPRT";
            this.btnOpenPRT.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPRT.TabIndex = 1;
            this.btnOpenPRT.Text = "打开";
            this.btnOpenPRT.Click += new System.EventHandler(this.btnOpenPRT_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(426, 377);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(76, 27);
            this.btnSubmit.TabIndex = 16;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // FrmImportPRT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 437);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmImportPRT";
            this.Text = "导入PRT文件";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpenSJLFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpenPRT.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblOutput;
        private DevExpress.XtraEditors.TextEdit txtOutput;
        private DevExpress.XtraEditors.SimpleButton btnOutput;
        private DevExpress.XtraEditors.LabelControl lblInput;
        private DevExpress.XtraEditors.TextEdit txtInput;
        private DevExpress.XtraEditors.LabelControl lblOpenPRT;
        private DevExpress.XtraEditors.LabelControl lblOpenSJLFile;
        private DevExpress.XtraEditors.TextEdit txtOpenSJLFile;
        private DevExpress.XtraEditors.SimpleButton btnOpenSJLFile;
        private DevExpress.XtraEditors.TextEdit txtOpenPRT;
        private DevExpress.XtraEditors.SimpleButton btnOpenPRT;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}