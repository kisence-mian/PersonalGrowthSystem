using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Config
{
    public static T GetConfig<T>() where T:new()
    {
        return RecordManager.GetRecord<T>(Const.ConfigFile, typeof(T).ToString(), new T());
    }

    public static void SaveConfig<T>(T config) where T : new()
    {
        RecordManager.SaveRecord<T>(Const.ConfigFile, typeof(T).ToString(), config);
    }
}
