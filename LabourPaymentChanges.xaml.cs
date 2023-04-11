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
    /// Логика взаимодействия для LabourPaymentChanges.xaml
    /// </summary>
    public partial class LabourPaymentChanges : Page
    {
        BD bd = new BD();

        public LabourPaymentChanges()
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
        private void ButtonNewStaffClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new NewLabourPayment();
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            var DataForPayroll1 = DGridTableReports.SelectedItems.Cast<DataForPayroll>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить {DataForPayroll1.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BD.GetContext().DataForPayroll.RemoveRange(DataForPayroll1);
                    BD.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridTableReports.ItemsSource = BD.GetContext().DataForPayroll.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void DGridTableReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Poisk_Table(object sender, TextChangedEventArgs e)
        {
            var result1 = bd.DataForPayroll.Join(bd.Staff,
            a => a.PersonnelNumberSalary,
            b => b.PersonnelNumber,
            (a, b) => new { a.PersonnelNumberSalary, b.Initials, a.PaymentTypeCodeSalary, a.OffsettingAccountSalary, a.DaysSalary, a.HoursSalary }).ToList();


            var WihtThisName = result1.Where(w => w.Initials.StartsWith(TBTableReports.Text)).ToList();
            DGridTableReports.ItemsSource = WihtThisName;
        }

        private void TBTableReports_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBTableReports.Clear();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridTableReports.ItemsSource = BD.GetContext().DataForPayroll.ToList();
            }
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new LabourPaymentEditor(((sender as Button).DataContext as DataForPayroll)));
        }
    }
}
