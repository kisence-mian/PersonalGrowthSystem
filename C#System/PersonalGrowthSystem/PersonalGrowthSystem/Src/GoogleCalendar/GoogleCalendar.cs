using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using PersonalGrowthSystem;
using PersonalGrowthSystem.Src.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

public class GoogleCalendar
{
    static public bool isInit = false;

    static string[] Scopes = { CalendarService.Scope.Calendar };
    static string ApplicationName = "PersonalGrowthSystem";//PersonalGrowthSystem

    static UserCredential credential;
    static CalendarService service;

    public static string GetCredentialPath()
    {
        return RecordManager.GetRecord(
            Const.GoogleCalendarConfig, 
            Const.Google_CredentialsPath, "Config/credentials.json");
    }

    public static void LoadCredential()
    {
        try
        {
            if (!isInit)
            {
                string credentialPath = GetCredentialPath();

                using (var stream =
                       new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
                {
                    string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/PersonalGrowthSystem-Calendar.json");

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                isInit = true;

                Colors colors = service.Colors.Get().Execute();
            }
        }
        catch(Exception e)
        {
            //MessageBox.Show(e.ToString());
        }
    }

    public static void Report(DateTime date,string name,string description,int minutes,string colorID = "0")
    {
        LoadCredential();

        if (service != null)
        {
            try
            {
                Colors cs = new Colors();
                ColorDefinition c = new ColorDefinition();

                Event e = new Event();
                e.Summary = name;
                e.Description = description;
                e.ColorId = colorID;

                e.Location = RecordManager.GetRecord(Const.GoogleCalendarConfig, Const.Google_Location, "XXX");

                EventDateTime start = new EventDateTime();
                start.DateTime = date;
                start.TimeZone = RecordManager.GetRecord(Const.GoogleCalendarConfig, Const.Google_TimeZone, "Asia/Shanghai");
                e.Start = start;

                EventDateTime end = new EventDateTime();
                end.DateTime = date.AddMinutes(minutes);
                end.TimeZone = RecordManager.GetRecord(Const.GoogleCalendarConfig, Const.Google_TimeZone, "Asia/Shanghai");
                e.End = end;

                String calendarId = "primary";
                service.Events.Insert(e, calendarId).Execute();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        else
        {
            MainWindow.Notify("ERROR: Google 服务未启动");
        }
    }

}
