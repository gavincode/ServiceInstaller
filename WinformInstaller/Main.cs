using System;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;
using Util.Service;

namespace WinformInstaller
{
    public partial class Main : Form
    {
        #region 全局变量

        //当前目录
        private static String baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //服务exe文件路径
        private static String ServiceEXE { get; set; }

        //服务名称
        private static String ServiceName { get; set; }

        #endregion

        #region 服务状态描述

        /// <summary>
        /// 服务状态描述
        /// </summary>
        public class ServiceStatusDESC
        {
            public static String Start = "已启动";
            public static String Stop = "已停止";
            public static String Uninstall = "未安装";
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public Main()
        {
            InitializeComponent();

            //初始化服务信息
            if (!InitServiceInfo())
            {
                EnableServiceButton(false);
            }

            //设置服务状态描述
            SetServiceStatus();
        }

        /// <summary>
        /// 初始化服务信息
        /// </summary>
        /// <returns></returns>
        private static Boolean InitServiceInfo()
        {
            //从当前路径的程序集
            ServiceEXE = ServiceUtil.GetCurrentServiceEXE();
            if (ServiceEXE == String.Empty)
            {
                MessageBox.Show("当前路径未找到符合的服务应用程序!");
                return false;
            }

            ServiceName = ServiceUtil.GetServiceName(ServiceEXE);
            if (ServiceName == String.Empty)
            {
                MessageBox.Show("获取服务名称异常!");
                return false;
            }

            return true;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <returns>状态描述字符串</returns>
        private String GetServiceStatus()
        {
            String strStatus = ServiceStatusDESC.Uninstall;

            //服务是否存在
            if (ServiceUtil.Exists(ServiceName))
            {
                //服务是否已启动
                if (ServiceUtil.IsRunning(ServiceName))
                {
                    strStatus = ServiceStatusDESC.Start;
                }
                else
                {
                    strStatus = ServiceStatusDESC.Stop;
                }
            }

            return strStatus;
        }

        /// <summary>
        /// 设置服务状态
        /// </summary>
        private void SetServiceStatus()
        {
            String strStatus = GetServiceStatus();

            this.lblServiceName.Text = "服务名称: " + ServiceName;
            this.lblServiceStatus.Text = "服务状态: " + strStatus;

            //设置按钮状态
            EnableServiceButton(true, strStatus);
        }

        /// <summary>
        /// 使服务操作按钮可用
        /// </summary>
        /// <param name="enable">是否可用</param>
        /// <param name="status">服务状态</param>
        private void EnableServiceButton(Boolean enable = true, String status = null)
        {
            //当不允许点击时,按钮不可用
            if (!enable || status == null)
            {
                this.btnServiceInstall.Enabled = enable;
                this.btnServiceStart.Enabled = enable;
                this.btnServiceStop.Enabled = enable;
                this.btnServiceUninsatll.Enabled = enable;
                this.lblServiceStatus.Text = "服务状态: 请稍等...";
                this.lblServiceStatus.Refresh();
                return;
            }

            //根据服务当前状态设置按钮状态
            if (status == ServiceStatusDESC.Uninstall)
            {
                this.btnServiceInstall.Enabled = enable;
                this.btnServiceStart.Enabled = false;
                this.btnServiceStop.Enabled = false;
                this.btnServiceUninsatll.Enabled = false;
            }
            else
            {
                if (status == ServiceStatusDESC.Start)
                {
                    this.btnServiceInstall.Enabled = false;
                    this.btnServiceStart.Enabled = false;
                    this.btnServiceStop.Enabled = enable;
                    this.btnServiceUninsatll.Enabled = enable;
                }
                else
                {
                    this.btnServiceInstall.Enabled = false;
                    this.btnServiceStart.Enabled = enable;
                    this.btnServiceStop.Enabled = false;
                    this.btnServiceUninsatll.Enabled = enable;
                }
            }
        }

        #endregion

        #region 界面事件方法

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServiceInstall_Click(object sender, EventArgs e)
        {
            EnableServiceButton(false);  //使操作按钮暂不可用

            try
            {
                ServiceUtil.Install(ServiceEXE);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("不可访问的日志"))
                {
                    MessageBox.Show("权限不足, 请以管理员身份运行vs进行调试!");
                    return;
                }

                MessageBox.Show("服务安装失败!");
                return;
            }
            finally
            {
                SetServiceStatus();
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServiceStart_Click(object sender, EventArgs e)
        {
            EnableServiceButton(false);  //使操作按钮暂不可用

            try
            {
                ServiceUtil.Start(ServiceName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务启动失败!");
                return;
            }
            finally
            {
                SetServiceStatus();
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServiceStop_Click(object sender, EventArgs e)
        {
            EnableServiceButton(false);  //使操作按钮暂不可用

            try
            {
                ServiceUtil.Stop(ServiceName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务停止失败!");
                return;
            }
            finally
            {
                SetServiceStatus();
            }
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServiceUninsatll_Click(object sender, EventArgs e)
        {
            EnableServiceButton(false);  //使操作按钮暂不可用

            try
            {
                if (ServiceUtil.IsRunning(ServiceName))
                {
                    ServiceUtil.Stop(ServiceName);
                }

                ServiceUtil.UnInstall(ServiceEXE);
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务卸载失败!");
                return;
            }
            finally
            {
                SetServiceStatus();
            }
        }

        /// <summary>
        /// 右键菜单-创建批处理安装包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCreate_Click(object sender, EventArgs e)
        {
            String cmdDir = baseDirectory + "CommandInstaller";
            String installFile = Path.Combine(cmdDir, "Installer.bat");
            String unInstallFile = Path.Combine(cmdDir, "Uninstaller.bat");
            String startFile = Path.Combine(cmdDir, "Start.bat");
            String stopFile = Path.Combine(cmdDir, "Stop.bat");

            if (!Directory.Exists(cmdDir))
            {
                Directory.CreateDirectory(cmdDir);
            }

            //安装
            if (!File.Exists(installFile))
            {
                File.AppendAllText(installFile,
                    @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe "
                    + ServiceEXE
                    + Environment.NewLine
                    + "pause");
            }

            //启动
            if (!File.Exists(startFile))
            {
                File.AppendAllText(startFile,
                    "net start "
                    + ServiceName
                    + Environment.NewLine
                    + "pause");
            }

            //暂停
            if (!File.Exists(stopFile))
            {
                File.AppendAllText(stopFile,
                    "net stop "
                    + ServiceName
                    + Environment.NewLine
                    + "pause");
            }

            //卸载
            if (!File.Exists(unInstallFile))
            {
                File.AppendAllText(unInstallFile,
                   @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u "
                    + ServiceEXE
                    + Environment.NewLine
                    + "pause");
            }
        }

        #endregion
    }
}