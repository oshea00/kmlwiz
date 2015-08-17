using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace kmlwiz
{
    class Program
    {
        static void Main(string[] args)
        {
            var latdeg = 58;
            var latmin = 13;
            var latsec = 15;

            var londeg = 151;
            var lonmin = 11;
            var lonsec = 25;

            var coord = new GeoAngle();
            coord.Degrees = latdeg;
            coord.Minutes = latmin;
            coord.Seconds = latsec;

            var towerHeightFt = 80.0;
            var towerElevationFt = 2047.0;
            var totalFt = towerHeightFt + towerElevationFt;

            var elevM =  totalFt / 3.28084;
            
            var latdec = ConvertDMSToDouble(latdeg, latmin, latsec);
            Console.WriteLine(string.Format("Lat (decimal) {0:0.000000}",latdec));
            Console.WriteLine(string.Format("Lat (DMS) {0:0.000000}", GeoAngle.FromDouble(latdec)));

            var londec = ConvertDMSToDouble(londeg, lonmin, lonsec);
            Console.WriteLine(string.Format("Lon (decimal) {0:0.000000}", londec));
            Console.WriteLine(string.Format("Lon (DMS) {0:0.000000}", GeoAngle.FromDouble(londec)));

            Console.WriteLine(string.Format("Height (meters) {0:0.0}", elevM));

            Console.ReadLine();

        }

        static double ConvertDMSToDouble(double degrees, double minutes, double seconds)
        {
            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600

            return degrees + (minutes / 60) + (seconds / 3600);
        }
    }
}
