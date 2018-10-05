using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConfigData
{
    public bool isStartRun;
    public string shotPosition;
    public string location;
    public string credentialsPath;
    public string screenShotPath;

    public bool IsStartRun
    {
        get => isStartRun;
        set  {
            isStartRun = value;
            Save();
        }
    }
    public string ShotPosition
    {
        get => shotPosition;
        set {
            shotPosition = value;
            Save();
        }
    }

    public string Location
    {
        get => location;
        set
        { 
            location = value;
            RecordManager.SaveRecord(Const.GoogleCalendarConfig, Const.Google_Location, location);
            Save();
        }
    }

    public string CredentialsPath
    {
        get => credentialsPath;

        set
        {
            credentialsPath = value;
            RecordManager.GetRecord(
            Const.GoogleCalendarConfig,
            Const.Google_CredentialsPath, credentialsPath);
            Save();
        }
    }

    public string ScreenShotPath

    { get => screenShotPath;
        set
        {
            screenShotPath = value;
            RecordManager.SaveRecord(Const.ConfigFile, Const.Config_ShotPosition, screenShotPath);
            Save();
        }
    }

    public void Save()
    {
        Config.SaveConfig(this);
    }
}
