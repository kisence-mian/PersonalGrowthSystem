using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public static class RegistryTool
{
    public static bool IsExistKey(string keyName)
    {
        try
        {
            bool _exist = false;
            RegistryKey local = Registry.LocalMachine;
            RegistryKey runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (runs == null)
            {
                RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                RegistryKey key3 = key2.CreateSubKey("Microsoft");
                RegistryKey key4 = key3.CreateSubKey("Windows");
                RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                RegistryKey key6 = key5.CreateSubKey("Run");
                runs = key6;
            }
            string[] runsName = runs.GetValueNames();
            foreach (string strName in runsName)
            {
                if (strName.ToUpper() == keyName.ToUpper())
                {
                    _exist = true;
                    return _exist;
                }
            }
            return _exist;

        }
        catch
        {
            return false;
        }
    }

    ///isStart--是否开机自启动
    ///exeName--应用程序名
    ///path--应用程序路径
    public static bool SelfRunning(bool isStart, string exeName, string path)
    {
        try
        {
            RegistryKey local = Registry.LocalMachine;
            RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key == null)
            {
                local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
            }
            if (isStart)//若开机自启动则添加键值对
            {
                key.SetValue(exeName, path);
                key.Close();
            }
            else//否则删除键值对
            {
                string[] keyNames = key.GetValueNames();
                foreach (string keyName in keyNames)
                {
                    if (keyName.ToUpper() == exeName.ToUpper())
                    {
                        key.DeleteValue(exeName);
                        key.Close();
                    }
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            return false;
            //throw;
        }

        return true;
    }
}
