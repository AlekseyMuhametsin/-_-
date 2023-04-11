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
    /// Логика взаимодействия для EmployeeRegistration.xaml
    /// </summary>
    public partial class EmployeeRegistration : Page
    {
        BD bd = new BD();

        public EmployeeRegistration()
        {
            InitializeComponent();
        }
        private void TBStaff_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBStaff.Clear();
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


        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridStaff.ItemsSource = BD.GetContext().Staff.ToList();
            }
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            var Staff1 = DGridStaff.SelectedItems.Cast<Staff>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить {Staff1.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BD.GetContext().Staff.RemoveRange(Staff1);
                    BD.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridStaff.ItemsSource = BD.GetContext().Staff.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Poisk_Staff(object sender, TextChangedEventArgs e)
        {
            var WihtThisName = bd.Staff.Where(w => w.Initials.StartsWith(TBStaff.Text)).ToList();
            DGridStaff.ItemsSource = WihtThisName;
        }

        private void EditStaffClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditorEmployee(((sender as Button).DataContext as Staff))); 
        }
    }
}
