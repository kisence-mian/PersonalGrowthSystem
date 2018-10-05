using Microsoft.WindowsAPICodePack.Dialogs;
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
        ConfigData cData;

        public SettingWindow()
        {
            InitializeComponent();

            cData = Config.GetConfig<ConfigData>();

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
                cData.CredentialsPath = this.Text_Path.Text;
            }
        }

        #region 开机启动

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            bool result = cb.IsChecked ?? false;

            if(result != cData.IsStartRun)
            {
                if (!PermissionTool.IsAdministrator())
                {
                    MessageBox.Show("请以管理员权限运行再设置开机启动");
                    return;
                }
                else
                {
                    RegistryTool.SelfRunning(cb.IsChecked ?? true, "PersonalGrowthSystem", PathTool.GetFullPath());
                    cData.IsStartRun = result;
                }
            }
        }

        #endregion

        private void Button_ScreenShotClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                this.Text_ShotPath.Text = dialog.FileName;
                cData.ShotPosition = this.Text_ShotPath.Text;
            }
        }
    }
}
