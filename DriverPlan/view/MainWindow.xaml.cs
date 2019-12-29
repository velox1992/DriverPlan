using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using DriverPlan.model;
using DriverPlan.viewmodel;

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

        }


        private void MenuItem_OnClick(object _Sender, RoutedEventArgs _E)
        {
            var hPrintDialog = new PrintDialog();
            if (hPrintDialog.ShowDialog() != true)
                return;

            var hFahrerPlanDocument = new FixedDocument();
            hFahrerPlanDocument.DocumentPaginator.PageSize = new Size(hPrintDialog.PrintableAreaWidth, hPrintDialog.PrintableAreaHeight);
         

            var hPage1 = new FixedPage();
            hPage1.Width = hFahrerPlanDocument.DocumentPaginator.PageSize.Width;
            hPage1.Height = hFahrerPlanDocument.DocumentPaginator.PageSize.Height;


            var hSchedule = new DriverSchedule();
            //hSchedule.Content = DriverPlanScheduleControl.Content;
            hSchedule.DataContext = DataContext;

            // ToDo: Für jedes Child-Control eine eigene Seite. ggf. muss doch der Content kopiert werden
           
            hPage1.Children.Add(hSchedule);

            PageContent hPage1Content = new PageContent();
            ((IAddChild)hPage1Content).AddChild(hPage1);
            hFahrerPlanDocument.Pages.Add(hPage1Content);




            //hPrintDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            hPrintDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            hPrintDialog.PrintDocument(hFahrerPlanDocument.DocumentPaginator, "Fahrerpläne");

        }

        public static T CloneXaml<T>(T source)
        {
            string xaml = XamlWriter.Save(source);
            StringReader sr = new StringReader(xaml);
            XmlReader xr = XmlReader.Create(sr);
            return (T)XamlReader.Load(xr);
        }

        private void DriverPlanEntriesGridOnKeyUp(object _Sender, KeyEventArgs _E)
        {
            if (DataContext is MainWindowViewModel hViewModel && _E.Key is Key.Delete && hViewModel.DeleteItemCommand.CanExecute(null))
            {
                var hDriverPlanEntries = ((DataGrid) _Sender).SelectedItems;
                foreach (var hDriverPlanEntry in hDriverPlanEntries)
                {
                    hViewModel.DeleteItemCommand.Execute(hDriverPlanEntry);
                   
                }
                
            }
            
        }
    }
}
