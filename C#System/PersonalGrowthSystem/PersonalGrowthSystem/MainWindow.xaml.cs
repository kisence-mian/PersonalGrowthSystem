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
        public MainWindow()
        {
            InitializeComponent();
            GoogleCalendar.LoadCredential();
            GoogleCalendar.Report(DateTime.Now, "Test");

            ScreenShot.ShotAll(".\\ScreenShot");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow window = new SettingWindow();
            window.ShowDialog();
        }
    }
}
