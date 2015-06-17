namespace WinformInstaller
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnServiceInstall = new System.Windows.Forms.Button();
            this.btnServiceStart = new System.Windows.Forms.Button();
            this.btnServiceStop = new System.Windows.Forms.Button();
            this.btnServiceUninsatll = new System.Windows.Forms.Button();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnServiceInstall
            // 
            this.btnServiceInstall.Location = new System.Drawing.Point(35, 100);
            this.btnServiceInstall.Name = "btnServiceInstall";
            this.btnServiceInstall.Size = new System.Drawing.Size(75, 23);
            this.btnServiceInstall.TabIndex = 6;
            this.btnServiceInstall.Text = "安装";
            this.btnServiceInstall.UseVisualStyleBackColor = true;
            this.btnServiceInstall.Click += new System.EventHandler(this.btnServiceInstall_Click);
            // 
            // btnServiceStart
            // 
            this.btnServiceStart.Location = new System.Drawing.Point(180, 100);
            this.btnServiceStart.Name = "btnServiceStart";
            this.btnServiceStart.Size = new System.Drawing.Size(75, 23);
            this.btnServiceStart.TabIndex = 7;
            this.btnServiceStart.Text = "启动";
            this.btnServiceStart.UseVisualStyleBackColor = true;
            this.btnServiceStart.Click += new System.EventHandler(this.btnServiceStart_Click);
            // 
            // btnServiceStop
            // 
            this.btnServiceStop.Location = new System.Drawing.Point(35, 139);
            this.btnServiceStop.Name = "btnServiceStop";
            this.btnServiceStop.Size = new System.Drawing.Size(75, 23);
            this.btnServiceStop.TabIndex = 8;
            this.btnServiceStop.Text = "停止";
            this.btnServiceStop.UseVisualStyleBackColor = true;
            this.btnServiceStop.Click += new System.EventHandler(this.btnServiceStop_Click);
            // 
            // btnServiceUninsatll
            // 
            this.btnServiceUninsatll.Location = new System.Drawing.Point(180, 139);
            this.btnServiceUninsatll.Name = "btnServiceUninsatll";
            this.btnServiceUninsatll.Size = new System.Drawing.Size(75, 23);
            this.btnServiceUninsatll.TabIndex = 9;
            this.btnServiceUninsatll.Text = "卸载";
            this.btnServiceUninsatll.UseVisualStyleBackColor = true;
            this.btnServiceUninsatll.Click += new System.EventHandler(this.btnServiceUninsatll_Click);
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.AutoSize = true;
            this.lblServiceStatus.ForeColor = System.Drawing.Color.Black;
            this.lblServiceStatus.Location = new System.Drawing.Point(40, 62);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(65, 12);
            this.lblServiceStatus.TabIndex = 11;
            this.lblServiceStatus.Text = "服务状态: ";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.ForeColor = System.Drawing.Color.Black;
            this.lblServiceName.Location = new System.Drawing.Point(40, 30);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(65, 12);
            this.lblServiceName.TabIndex = 10;
            this.lblServiceName.Text = "服务名称: ";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCreate});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(156, 26);
            // 
            // toolStripMenuItemCreate
            // 
            this.toolStripMenuItemCreate.Name = "toolStripMenuItemCreate";
            this.toolStripMenuItemCreate.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItemCreate.Text = "  创建批处理安装包";
            this.toolStripMenuItemCreate.Click += new System.EventHandler(this.toolStripMenuItemCreate_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 190);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.lblServiceStatus);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.btnServiceInstall);
            this.Controls.Add(this.btnServiceStart);
            this.Controls.Add(this.btnServiceStop);
            this.Controls.Add(this.btnServiceUninsatll);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServiceInstaller";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnServiceInstall;
        private System.Windows.Forms.Button btnServiceStart;
        private System.Windows.Forms.Button btnServiceStop;
        private System.Windows.Forms.Button btnServiceUninsatll;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreate;
    }
}

