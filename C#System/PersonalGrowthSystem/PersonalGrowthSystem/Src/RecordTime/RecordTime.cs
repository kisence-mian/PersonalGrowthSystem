using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

public class RecordTime
{
    public RecordTime()
    {
        GoogleCalendar.LoadCredential();

        TimerTask(null, null);
    }

    //每10分钟执行一次
    public void TimerTask(object source, ElapsedEventArgs e)
    {
        string processes = GetCurrentProcesses();

        GoogleCalendar.Report(DateTime.Now, processes, processes, 10);

        //屏幕截屏
        ScreenShot.ShotAll(".\\ScreenShot");
    }

    string GetCurrentProcesses()
    {
        string content = "";

        int id = GetForegroundWindow().ToInt32();

        Process[] ps = Process.GetProcesses();
        foreach (Process p in ps)
        {
            string info = "";
            try
            {
                if(p.MainWindowHandle.ToInt32() == id)
                {
                    content += p.ProcessName + " -> " + p.MainWindowTitle + "\n";
                }
            }
            catch (Exception e)
            {
                info = e.Message;
            }
        }

        MessageBox.Show(content);

        return content;

    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
}