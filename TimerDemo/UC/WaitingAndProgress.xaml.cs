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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimerDemo
{
    public enum WaitAndProgressType
    {
        WaitingAndProgress,
        Waiting,
        Progress
    }
    /// <summary>
    /// WaitingAndProgress.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingAndProgress : UserControl
    {
        #region 属性

        private WaitAndProgressType showType = WaitAndProgressType.WaitingAndProgress;
        /// <summary>
        /// 当前样式类型
        /// </summary>
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Appreance"), System.ComponentModel.Description("设置或获取当前样式类型")]
        public WaitAndProgressType ShowType
        {
            get
            {
                return showType;
            }
            set
            {
                showType = value;
            }
        }

        #endregion

        public WaitingAndProgress()
        {
            InitializeComponent();
        }

        #region 方法

        public void setPrecent(double d)
        {
            if (showType == WaitAndProgressType.Waiting)
            {
                return;
            }
            Storyboard b = (Storyboard)this.Resources["FillStoryboard"];
            DoubleAnimationUsingKeyFrames df = (DoubleAnimationUsingKeyFrames)b.Children[0];
            ColorAnimationUsingKeyFrames cf = (ColorAnimationUsingKeyFrames)b.Children[1];
            if (d >= 0 && d <= 10)
            {
                cf.KeyFrames[1].Value = ToColor("#FFFF3300");
            }
            if (d > 10 && d <= 20)
            {
                cf.KeyFrames[1].Value = ToColor("#FFFF6600");
            }
            if (d > 20 && d <= 30)
            {
                cf.KeyFrames[1].Value = ToColor("#FFFF9900");
            }
            if (d > 30 && d <= 40)
            {
                cf.KeyFrames[1].Value = ToColor("#FFFFCC00");
            }
            if (d > 40 && d <= 50)
            {
                cf.KeyFrames[1].Value = ToColor("#FFFFFF00");
            }
            if (d > 50 && d <= 60)
            {
                cf.KeyFrames[1].Value = ToColor("#FFCCFF00");
            }
            if (d > 60 && d <= 70)
            {
                cf.KeyFrames[1].Value = ToColor("#FF99FF00");
            }
            if (d > 70 && d <= 80)
            {
                cf.KeyFrames[1].Value = ToColor("#FF66FF00");
            }
            if (d > 80 && d <= 90)
            {
                cf.KeyFrames[1].Value = ToColor("#FF33FF00");
            }
            if (d > 90 && d <= 100)
            {
                cf.KeyFrames[1].Value = ToColor("#FF00FF00");
            }
            df.KeyFrames[1].Value = d * 3.6;
            b.Begin();
        }

        /// <summary>
        /// 将blend的8位颜色值转为color
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public Color ToColor(string colorName)
        {
            if (colorName.StartsWith("#"))
                colorName = colorName.Replace("#", string.Empty);
            int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
            return new Color()
            {
                A = Convert.ToByte((v >> 24) & 255),
                R = Convert.ToByte((v >> 16) & 255),
                G = Convert.ToByte((v >> 8) & 255),
                B = Convert.ToByte((v >> 0) & 255)
            };
        }

        #endregion

        #region 事件

        //载入时事件处理
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (showType != WaitAndProgressType.Progress)
            {
                Storyboard b1 = (Storyboard)this.Resources["MainStoryboard"];
                b1.Begin();
                if (showType == WaitAndProgressType.Waiting)
                {
                    ShowLabel.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    ShowLabel.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                Storyboard b1 = (Storyboard)this.Resources["MainStoryboard"];
                b1.Stop();
                ShowLabel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //渐变动画完成时
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Storyboard b = (Storyboard)this.Resources["FillStoryboard"];
            ColorAnimationUsingKeyFrames cf = (ColorAnimationUsingKeyFrames)b.Children[1];
            DoubleAnimationUsingKeyFrames df = (DoubleAnimationUsingKeyFrames)b.Children[0];
            df.KeyFrames[0].Value = df.KeyFrames[1].Value;
            cf.KeyFrames[0].Value = cf.KeyFrames[1].Value;
        }

        #endregion

    }
}
