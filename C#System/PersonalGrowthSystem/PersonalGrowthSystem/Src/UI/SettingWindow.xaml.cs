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
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();

            ConfigData cData = Config.GetConfig<ConfigData>();
            //GoogleCalendarConfig gData = Config.GetConfig<GoogleCalendarConfig>();

            DataContext = cData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "APK Files (*.json)|*.json"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.Text_Path.Text = openFileDialog.FileName;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            RegistryTool.SelfRunning(cb.IsChecked ?? true, PathTool.GetApplicationName(), PathTool.GetCurrentPath());
        }
    }
}
