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

namespace PersonalGrowthSystem.Src.UI
{
    /// <summary>
    /// ReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }
        bool isReport = false; //正在上报

        //定义Timer类
        System.Timers.Timer timer;
        int timerCount = 0;

        #region 跑步

        bool isRun = false;

        private void Button_run_Click(object sender, RoutedEventArgs e)
        {
            //停止跑步
            if (isRun)
            {
                if (Text_distance.Text != "")
                {
                    MainWindow.StartTimer();
                    MainWindow.Report("跑步", "里程: " + Text_distance.Text, GetMinute(), "2");
                    EndTimer();

                    isRun = false;
                    Text_distance.Text = "";
                    Button_run.Content = "开始跑步";
                }
            }
            //开始跑步
            else
            {
                MainWindow.PauseTimer();

                StartTimer();

                isRun = true;
                Button_run.Content = "停止跑步";
            }
        }

        #endregion

        #region 读书

        bool isRead = false;

        private void Button_read_Click(object sender, RoutedEventArgs e)
        {
            //停止读书
            if (isRead)
            {
                if (Text_BookName.Text != "")
                {
                    MainWindow.StartTimer();
                    MainWindow.Report("读书", "书名: " + Text_BookName.Text, GetMinute(), "1");
                    EndTimer();

                    isRead = false;
                    Button_read.Content = "开始读书";
                }
            }
            //开始读书
            else
            {
                MainWindow.PauseTimer();

                StartTimer();

                isRead = true;
                Button_read.Content = "停止读书";
            }
        }

        #endregion

        #region 练琴

        bool isGuitar = false;

        private void Button_guitar_Click(object sender, RoutedEventArgs e)
        {
            //停止练琴
            if (isGuitar)
            {
                MainWindow.StartTimer();
                MainWindow.Report("练琴", "", GetMinute(), "6");
                EndTimer();

                isGuitar = false;
                Button_read.Content = "开始读书";
            }
            //开始练琴
            else
            {
                MainWindow.PauseTimer();

                StartTimer();

                isGuitar = true;
                Button_read.Content = "停止读书";
            }
        }

        #endregion

        #region 计时

        void InitTimer()
        {

            //设置定时间隔(毫秒为单位)
            int interval = 1000;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
        }

        void StartTimer()
        {
            if (timer == null)
            {
                InitTimer();
            }

            timer.Start();
        }

        void EndTimer()
        {
            timer.Stop();
            timerCount = 0;
            Label_Time.Content = "";
        }

        void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timerCount += 1;
                TimeSpan ts = new TimeSpan(0, 0, timerCount);

                this.Dispatcher.BeginInvoke(new Action(() => 
                    Label_Time.Content = "时间： " + ts
                ));
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行定时到点事件失败:" + ex.Message);
            }
        }

        int GetMinute()
        {
            return timerCount / 60;
        }


        #endregion


    }
}
