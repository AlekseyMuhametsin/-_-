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
    /// Логика взаимодействия для EditorTableReports.xaml
    /// </summary>
    public partial class EditorTableReports : Page
    {
        BD bd = new BD();

        public EditorTableReports()
        {
            InitializeComponent();
            DGridTableReports.ItemsSource = bd.AccountingForWorkingHours.Join(bd.Staff,
                a => a.PersonnelNumberAccounting,
                b => b.PersonnelNumber,
                (a, b) => new { a.PersonnelNumberAccounting, b.Initials, a.DaysAccounting, a.DayCodeAccounting, a.HoursAccounting, a.MonthAccounting }).ToList();
        }

        private void DGridTableReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonNewStaffClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new AddReports();
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            var AccountingForWorkingHours1 = DGridTableReports.SelectedItems.Cast<AccountingForWorkingHours>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить {AccountingForWorkingHours1.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BD.GetContext().AccountingForWorkingHours.RemoveRange(AccountingForWorkingHours1);
                    BD.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridTableReports.ItemsSource = BD.GetContext().AccountingForWorkingHours.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Poisk_Table(object sender, TextChangedEventArgs e)
        {
            var result1 = bd.AccountingForWorkingHours.Join(bd.Staff,
            a => a.PersonnelNumberAccounting,
            b => b.PersonnelNumber,
            (a, b) => new { a.PersonnelNumberAccounting, b.Initials, a.DaysAccounting, a.DayCodeAccounting, a.HoursAccounting, a.MonthAccounting }).ToList();


            var WihtThisName = result1.Where(w => w.Initials.StartsWith(TBTableReports.Text)).ToList();
            DGridTableReports.ItemsSource = WihtThisName;
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

        private void TBTableReports_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBTableReports.Clear();
        }

        private void EditTabelClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditingAccounts(((sender as Button).DataContext as AccountingForWorkingHours)));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridTableReports.ItemsSource = BD.GetContext().AccountingForWorkingHours.ToList();
            }
        }

        private void TBStaff_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBTableReports.Clear();
        }
    }
}
