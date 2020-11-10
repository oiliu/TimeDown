using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TimerDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();

            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }
    }
}
