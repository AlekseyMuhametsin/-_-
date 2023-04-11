using System;
using System.Collections.Generic;
using System.Linq;
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

namespace КП_МДК._03
{
    /// <summary>
    /// Логика взаимодействия для Wages.xaml
    /// </summary>
    public partial class Wages : Page
    {
        public Wages()
        {
            InitializeComponent();
        }
        private void ButtonPersonnelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new ViewingEmployees();
        }

        private void ButtonTimesheetlClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new TableReports();
        }

        private void ButtonArchivelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new ReportExcel();
        }

        private void ButtonRegistrationClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new Wages();
        }


        private void ButtonMonthlyDataffClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new SalaryData();
        }

        private void ButtonNoShowClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new NoShow();
        }

        private void ButtonEnrollmentClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new LabourPayment();
        }
    }
}
