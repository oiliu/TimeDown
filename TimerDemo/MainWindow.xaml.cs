using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimerDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded; InitialTray();
        }
        DispatcherTimer timer;
        int hour = -1;
        int minus = -1;
        int second = -1;

        public void InitData()
        {
            btn_start.IsEnabled = true;
            timer.Stop();
            hour = -1;
            minus = -1;
            second = -1;
            bd.Visibility = Visibility.Collapsed;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (timer == null)
                timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hour == 0 && minus == 0 && second == 0)
            {
                TipWin tw = new TipWin();
                tw.main = this;
                tw.ShowDialog(); 
            }
            else
            {
                if (second == 0)
                {
                    if (minus == 0)
                    {
                        hour--;
                        minus = 60;
                    }
                    else
                    {
                        minus--;
                        second = 60;
                    }
                }
                else
                {
                    second--;
                }

                txtBd.Text = hour + ":" + minus + ":" + second;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hour = Convert.ToInt32(txt1.Text.ToString());
                minus = Convert.ToInt32(txt2.Text.ToString());
                second = Convert.ToInt32(txt3.Text.ToString());

                btn_start.IsEnabled = false;
                notifyIcon.BalloonTipText = "计时开始";
                this.Visibility = Visibility.Hidden;
                bd.Visibility = Visibility.Visible;
                timer.Start();
            }
            catch
            {
                MessageBox.Show("异常！");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定重置？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                InitData();
            }
        }


        private void numberWheelchange(object sender, MouseWheelEventArgs e)
        {
            if (txt1.IsSelectionActive)
            {
                aa(txt1, e);
            }
            if (txt2.IsSelectionActive)
            {
                aa(txt2, e);
            }
            if (txt3.IsSelectionActive)
            {
                aa(txt3, e);
            }
        }

        void aa(object a, MouseWheelEventArgs e)
        {
            if (a is TextBox)
            {
                TextBox t = (TextBox)a;
                if (t.Text != null)
                {
                    int r = (Convert.ToInt16(t.Text) + e.Delta / 120);
                    if (r <= 0)
                    {
                        t.Text = "59";
                    }
                    else
                    {
                        if (r >= 60)
                        {
                            t.Text = "0";
                        }
                        else
                            t.Text = r.ToString();
                    }
                }
            }
        }

        #region 托盘图标
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        private void InitialTray()
        {
            //设置托盘的各个属性
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.BalloonTipText = "计时器启动无异常";
            notifyIcon.Text = "小t计时";
            notifyIcon.Icon = new System.Drawing.Icon(System.Windows.Forms.Application.StartupPath + "\\t.ico");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(SysTray_StateChanged);
        }

        //窗体状态改变时候触发
        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        //退出选项
        private void exit_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("确定要关闭吗?",
            //                                   "退出",
            //                                    MessageBoxButton.YesNo,
            //                                    MessageBoxImage.Question,
            //                                    MessageBoxResult.No) == MessageBoxResult.Yes)
            //{
            notifyIcon.Dispose();
            Application.Current.Shutdown();
            //}
        }

        // 鼠标单击
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
