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
    /// Логика взаимодействия для ViewingEmployees.xaml
    /// </summary>
    public partial class ViewingEmployees : Page
    {
        BD bd = new BD();

        public ViewingEmployees()
        {
            InitializeComponent();
        }

        private void TBStaff_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBStaff.Clear();
        }

        private void Poisk_Staff(object sender, TextChangedEventArgs e)
        {
            var WihtThisName = bd.Staff.Where(w => w.Initials.StartsWith(TBStaff.Text)).ToList();
            DGridStaff.ItemsSource = WihtThisName;
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

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridStaff.ItemsSource = BD.GetContext().Staff.ToList();
            }
        }

        private void DGridStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
