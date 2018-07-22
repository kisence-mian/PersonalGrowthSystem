using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

public class ScreenShot
{
    public static void ShotAll(string path)
    {
        string name = "ScreenShot" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        int offset = 0;
        for (int i = 0; i < Screen.AllScreens.Length; i++)
        {
            string nameTmp = name + "_" + i + ".jpg";

            if(path != null)
            {
                nameTmp = path + "/" + nameTmp;
            }

            Shot(nameTmp, offset,i);

            offset += Screen.AllScreens[i].Bounds.Size.Width;
        }
    }


    public static void Shot(string path,int offset,int screenID)
    {
        FileTool.CreatFilePath(path);

        Bitmap bitmap = new Bitmap(Screen.AllScreens[screenID].Bounds.Size.Width, Screen.AllScreens[screenID].Bounds.Size.Height);
        Graphics g = Graphics.FromImage(bitmap);
        g.CopyFromScreen(offset, 0, 0, 0, Screen.AllScreens[screenID].Bounds.Size);
        g.Dispose();
        bitmap.Save(path);
    }
}
