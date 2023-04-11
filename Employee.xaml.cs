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
    /// Логика взаимодействия для Employee.xaml
    /// </summary>
    public partial class Employee : Page
    {
        BD bd = new BD();

        public Employee()
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
            NavigationService.Content = new Employee();
        }

        private void ButtonTimesheetlClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new AdministratorReportCard();
        }

        private void ButtonArchivelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new ReportExcel();
        }

        private void ButtonRegistrationClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new Registration1();
        }

        private void DGridStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonNewStaffClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new NewStaff();
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new EmployeeRegistration();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridStaff.ItemsSource = BD.GetContext().Staff.ToList();
            }
        }
    }
}
