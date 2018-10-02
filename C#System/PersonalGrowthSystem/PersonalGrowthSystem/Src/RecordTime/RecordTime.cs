using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

public class RecordTime
{
    public RecordTime()
    {
        GoogleCalendar.LoadCredential();
    }

    //每10分钟执行一次
    public void TimerTask(object source, ElapsedEventArgs e)
    {
        GoogleCalendar.Report(DateTime.Now, "Test", 10);

        //屏幕截屏
        ScreenShot.ShotAll(".\\ScreenShot");
    }
}