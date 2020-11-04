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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimerDemo
{
    /// <summary>
    /// TipWin.xaml 的交互逻辑
    /// </summary>
    public partial class TipWin : Window
    {
        public TipWin()
        {
            InitializeComponent();
            this.Loaded += TipWin_Loaded;
        }

        private void TipWin_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(230);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (txt.Visibility == Visibility.Hidden)
            {
                txt.Visibility = Visibility.Visible;
            }
            else { txt.Visibility = Visibility.Hidden; }
        }

        public MainWindow main = null;
        Random r = new Random();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = r.Next(0, 1620);
            this.Top = r.Next(0,880);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (main != null)
            {
                main.InitData();
            }
            this.Close();
        }
    }
}
