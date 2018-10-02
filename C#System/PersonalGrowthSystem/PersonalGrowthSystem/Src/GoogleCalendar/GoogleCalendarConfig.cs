using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GoogleCalendarConfig
{
    public string credentialsPath;
    public string location;
    public string timeZone;

    public string CredentialsPath { get => credentialsPath; set => credentialsPath = value; }
    public string Location { get => location; set => location = value; }
    public string TimeZone { get => timeZone; set => timeZone = value; }
}
