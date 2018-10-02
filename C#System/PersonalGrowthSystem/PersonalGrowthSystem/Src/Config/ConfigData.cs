using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConfigData
{
    public bool isStartRun;
    public string shotPosition;

    public bool IsStartRun
    {
        get => isStartRun;
        set  {
            isStartRun = value;
            Save();
        }
    }
    public string ShotPosition {
        get => shotPosition;
        set { shotPosition = value;
            Save();
        }
    }

    public void Save()
    {
        Config.SaveConfig<ConfigData>(this);
    }
}
