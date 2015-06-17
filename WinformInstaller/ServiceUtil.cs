// ****************************************
// FileName:ServiceInstallBLL.cs
// Description:
// Tables:
// Author:Gavin
// Create Date:2015/1/15 16:14:54
// Revision History:
// ****************************************

using System;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace Util.Service
{
    /// <summary>
    /// 服务操作帮助类
    /// </summary>
    public class ServiceUtil
    {
        #region 安装

        /// <summary>
        /// 安装Windows服务
        /// </summary>
        /// <param name="filepath">服务exe文件路径</param>
        public static void Install(String filepath)
        {
            ManagedInstallerClass.InstallHelper(new[] { filepath });
        }

        #endregion

        #region 卸载

        /// <summary>
        /// 卸载Windows服务
        /// </summary>
        /// <param name="filepath">服务exe文件路径</param>
        public static void UnInstall(String filepath)
        {
            ManagedInstallerClass.InstallHelper(new[] { "/u", filepath });
        }

        #endregion

        #region 启动

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="waitSeconds">等待时间 %秒</param>
        /// <returns>启动成功true;否则false</returns>
        public static Boolean Start(String serviceName, Int32 waitSeconds = 30)
        {
            //检验服务存在性
            if (Exists(serviceName))
            {
                ServiceController service = new ServiceController(serviceName);

                //仅当服务停止后才能启动
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 0, waitSeconds));
                    service.Close();

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 停止

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="waitSeconds">等待时间 %秒</param>
        /// <returns>停止成功true;否则false</returns>
        public static Boolean Stop(String serviceName, Int32 waitSeconds = 30)
        {
            //检验服务存在性
            if (Exists(serviceName))
            {
                ServiceController service = new ServiceController(serviceName);

                //仅当服务正在运行时才能停止
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, waitSeconds));
                    service.Close();

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 辅助

        /// <summary>
        /// 获取当前路径下的第一个服务应用程序
        /// </summary>
        /// <returns>当前路径下的第一个服务应用程序 或者 String.Empty</returns>
        public static String GetCurrentServiceEXE()
        {
            foreach (var item in Directory.GetFiles(Environment.CurrentDirectory, "*.exe"))
            {
                Assembly assembly = Assembly.LoadFile(item);

                foreach (var type in assembly.GetTypes())
                {
                    if (type.BaseType == typeof(Installer)) return item;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// 获取指定服务.exe程序的服务名; 失败返回String.Empty
        /// </summary>
        /// <param name="filepath">.exe程序路径</param>
        /// <returns>服务名</returns>
        public static String GetServiceName(String filepath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(filepath);

                foreach (var type in assembly.GetTypes())
                {
                    if (type.BaseType != typeof(Installer)) continue;

                    //创建一个安装实例
                    var installer = ((Installer)Activator.CreateInstance(type));

                    //遍历该类型所有实例成员的非公开属性
                    foreach (var item in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                    {
                        if (item.FieldType != typeof(ServiceInstaller)) continue;

                        //获取安装实例中ServiceInstaller成员的值
                        var serviceInstaller = (ServiceInstaller)item.GetValue(installer);
                        return serviceInstaller.ServiceName;
                    }
                }
            }
            catch (Exception) { }

            return String.Empty;
        }

        /// <summary>
        /// 检查服务存在的存在性
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>存在返回true,否则返回false;</returns>
        public static Boolean Exists(String serviceName)
        {
            //遍历系统服务
            foreach (ServiceController controller in ServiceController.GetServices())
            {
                if (controller.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 服务是否已经停止
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>已停止true;否则false</returns>
        public static Boolean IsStopped(String serviceName)
        {
            try
            {
                ServiceController controller = new ServiceController(serviceName);

                if (controller.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 服务是否正在运行
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>正在运行true;否则false</returns>
        public static Boolean IsRunning(String serviceName)
        {
            try
            {
                ServiceController controller = new ServiceController(serviceName);

                if (controller.Status.Equals(ServiceControllerStatus.Running) || controller.Status.Equals(ServiceControllerStatus.StartPending))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取服务状态枚举ServiceControllerStatus
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>ServiceControllerStatus状态枚举</returns>
        public static ServiceControllerStatus GetServiceStatus(String serviceName)
        {
            try
            {
                return new ServiceController(serviceName).Status;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}