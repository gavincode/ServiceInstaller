using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        String filePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName + "\\testlog.txt";

        static Timer timer = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(p =>
            {
                AppendLine("Working");
            }, null, 0, 1000);
        }

        protected override void OnStop()
        {
            AppendLine("OnStop");

            if (timer != null)
                timer.Dispose();
        }

        private void AppendLine(String content)
        {
            File.AppendAllText(filePath, String.Format("{0}   【{1}】{2}", content, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.NewLine));
        }
    }
}
