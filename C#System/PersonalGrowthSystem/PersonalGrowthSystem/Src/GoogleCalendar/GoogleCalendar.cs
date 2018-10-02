using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
        }
        catch
        {

        }
    }

    public static void Report(DateTime date,string name,int minutes)
    {
        if(service != null)
        {
            try
            {
                Event e = new Event();
                e.Summary = name;
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
            MessageBox.Show("Google 服务未启动");
        }
    }

}
