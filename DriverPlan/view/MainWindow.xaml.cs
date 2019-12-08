using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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
using DriverPlan.model;

namespace DriverPlan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /*
          var hDriverInfos = new List<DriverInfo>()
          {
              new DriverInfo()
              {
                  DeliveryLocation = "Kesternich",
                  DeliveryTime = DateTime.Now,
                  Driver = "Georg",
                  Note = "Links am Haus vorbei"
              }
          };



          var hExporter = new JsonExporter(cFilePath);
          hExporter.ExportData(hDriverInfos);
          */
          

        }
    }
}
