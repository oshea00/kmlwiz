using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;

namespace KMLWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string KMLFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\KML";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCreateKML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // dd mm ss.ss D
                var fromLat = txtFromLat.Text.Split();

                var fmlatdeg = double.Parse(fromLat[0]);
                var fmlatmin = double.Parse(fromLat[1]);
                var fmlatsec = double.Parse(fromLat[2]);

                var fromLon = txtFromLon.Text.Split();

                var fmlondeg = double.Parse(fromLon[0]);
                var fmlonmin = double.Parse(fromLon[1]);
                var fmlonsec = double.Parse(fromLon[2]);

                var toLat = txtToLat.Text.Split();

                var tolatdeg = double.Parse(toLat[0]);
                var tolatmin = double.Parse(toLat[1]);
                var tolatsec = double.Parse(toLat[2]);

                var toLon = txtToLon.Text.Split();

                var tolondeg = double.Parse(toLon[0]);
                var tolonmin = double.Parse(toLon[1]);
                var tolonsec = double.Parse(toLon[2]);

                var towerHeightFt = double.Parse(txtHeight.Text);
                var towerElevationFt = double.Parse(txtElev.Text);
                var totalFt = towerHeightFt + towerElevationFt;

                var elevM = totalFt / 3.28084;

                var totowerHeightFt = double.Parse(txtToHeight.Text);
                var totowerElevationFt = double.Parse(txtToElev.Text);
                var tototalFt = totowerHeightFt + totowerElevationFt;

                var toelevM = tototalFt / 3.28084;

                var fmlatdec = ConvertDMSToDouble(fmlatdeg, fmlatmin, fmlatsec);
                var fmlondec = -ConvertDMSToDouble(fmlondeg, fmlonmin, fmlonsec);
                var tolatdec = ConvertDMSToDouble(tolatdeg, tolatmin, tolatsec);
                var tolondec = -ConvertDMSToDouble(tolondeg, tolonmin, tolonsec);

                var exePath = Assembly.GetExecutingAssembly().Location;
                var currentFolder = System.IO.Path.GetDirectoryName(exePath);
                var KMLTemplate = string.Format(@"{0}\{1}", currentFolder, "temp.KML");

                var KMLstr = File.ReadAllText(KMLTemplate);
                KMLstr = KMLstr.Replace("@lon1", fmlondec.ToString("0.000000"));
                KMLstr = KMLstr.Replace("@lat1", fmlatdec.ToString("0.000000"));
                KMLstr = KMLstr.Replace("@h1", elevM.ToString("0000"));
                KMLstr = KMLstr.Replace("@lon2", tolondec.ToString("0.000000"));
                KMLstr = KMLstr.Replace("@lat2", tolatdec.ToString("0.000000"));
                KMLstr = KMLstr.Replace("@h2", toelevM.ToString("0000"));

                if (!Directory.Exists(KMLFolderPath))
                    Directory.CreateDirectory(KMLFolderPath);

                var kmlFile = KMLFolderPath + "\\" + txtPathName.Text + ".KML";

                File.WriteAllText(kmlFile,KMLstr);

                Process.Start(kmlFile);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
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
