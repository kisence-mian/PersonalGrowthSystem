using PersonalGrowthSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Forms;

public class RecordTime
{
    public RecordTime()
    {
        GoogleCalendar.LoadCredential();
        GoogleCalendar.Report(DateTime.Now, "程序启动", null, 1,"5");
    }

    //每10分钟执行一次
    public void TimerTask(object source, ElapsedEventArgs e)
    {
        Process processes = GetCurrentProcesses();

        if(processes != null)
        {
            GoogleCalendar.Report(DateTime.Now, processes.ProcessName, processes.MainWindowTitle, 10,GetEventColor(processes.ProcessName));
            MainWindow.Notify("已上报 ->" + processes.ProcessName);
        }
        else
        {
            GoogleCalendar.Report(DateTime.Now, "无活跃程序", "", 10,"8");
        }

        //屏幕截屏
        ScreenShot.ShotAll(Config.GetConfig<ConfigData>().ShotPosition);
    }

    #region 分析进程
    Process GetCurrentProcesses()
    {
        IntPtr hWnd = GetForegroundWindow();    //获取活动窗口句柄        
        int calcID = 0;    //进程ID           
        int calcTD = 0;    //线程ID           
        calcTD = GetWindowThreadProcessId(hWnd, out calcID);
        Process myProcess = Process.GetProcessById(calcID);

        return myProcess;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);   //获取线程ID


    #endregion

    #region 上报颜色

    string GetEventColor(string processesName)
    {
        return RecordManager.GetRecord(Const.EventColorConfig, processesName, "0");
    }

    #endregion
}