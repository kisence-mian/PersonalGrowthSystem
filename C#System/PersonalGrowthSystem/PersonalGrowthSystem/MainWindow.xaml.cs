using PersonalGrowthSystem.Src.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalGrowthSystem
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        WindowState ws;
        WindowState wsl;
        static public NotifyIcon notifyIcon;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                InitIcon();

                TimeRecord();
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow window = new SettingWindow();
            window.ShowDialog();
        }

        #region 任务栏小图标

        /// <summary>
        /// 任务栏通知
        /// </summary>
        /// <param name="description">内容</param>
        /// <param name="time">时间 单位是毫秒</param>
        public static void Notify(string description,int time)
        {
            MainWindow.notifyIcon.BalloonTipText = description;  //设置程序启动时显示的文本
            MainWindow.notifyIcon.ShowBalloonTip(time);
        }

        private void InitIcon()
        {
            //保证窗体显示在上方。
            wsl = WindowState;

            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "个人成长系统已启动";  //设置程序启动时显示的文本
            notifyIcon.Text = "个人成长系统";                  //最小化到托盘时，鼠标点击时显示的文本
            notifyIcon.Icon = new System.Drawing.Icon("Res/Icon.ico");//程序图标
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;
            notifyIcon.ShowBalloonTip(1000);
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = wsl;
           
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ws = WindowState;

            if (ws == WindowState.Minimized)
            {
                this.Hide();
            }

        }

        #endregion

        #region 时间记录

        System.Timers.Timer timer;

        void TimeRecord()
        {
            RecordTime rt = new RecordTime();

            timer = new System.Timers.Timer(10 * 60 * 1000);//10分钟
            timer.AutoReset = true;//AutoReset 属性为 true 时，每隔指定时间循环一次；如果为 false，则只执行一次。
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(rt.TimerTask);
            timer.Start();
        }

        #endregion
    }
}
