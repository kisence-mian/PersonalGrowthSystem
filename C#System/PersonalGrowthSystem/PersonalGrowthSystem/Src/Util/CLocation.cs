using CrossPlatformLibrary.Geolocation;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PersonalGrowthSystem.Src.Util
{
    public class CLocation
    {
        public static void watcher_PositionChangedAsync()
        {
            ILocationService locationService = ServiceLocator.Current.GetInstance<ILocationService>();
            Position position = locationService.GetPositionAsync().Result;

            PrintPosition(position.Latitude, position.Longitude);
        }

        static void PrintPosition(double Latitude, double Longitude)
        {
            Console.WriteLine("Latitude: {0}, Longitude {1}", Latitude, Longitude);
            MessageBox.Show("Latitude:"+ Latitude + ", Longitude "+ Longitude);
        }


        public static string GetPosition()
        {
            ILocationService locationService = ServiceLocator.Current.GetInstance<ILocationService>();
            Position position = locationService.GetPositionAsync().Result;

            return "Latitude:" + position.Latitude + ", Longitude " + position.Longitude;
        }
    }

}
