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
    /// Логика взаимодействия для SalaryDataEditor.xaml
    /// </summary>
    public partial class SalaryDataEditor : Page
    {
        BD bd = new BD();

        public SalaryDataEditor()
        {
            InitializeComponent();
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            var HoursMonthlySummary1 = DGridTableReports.SelectedItems.Cast<HoursMonthlySummary>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить {HoursMonthlySummary1.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BD.GetContext().HoursMonthlySummary.RemoveRange(HoursMonthlySummary1);
                    BD.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridTableReports.ItemsSource = BD.GetContext().HoursMonthlySummary.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Poisk_Table(object sender, TextChangedEventArgs e)
        {
            var result1 = bd.HoursMonthlySummary.Join(bd.Staff,
            a => a.PersonnelNumberSum,
            b => b.PersonnelNumber,
            (a, b) => new { a.PersonnelNumberSum, b.Initials, a.SumDays, a.SumHours, a.MonthSum }).ToList();


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

        private void ButtonNewStaffClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new CountZP();
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SalaryDataChanges(((sender as Button).DataContext as HoursMonthlySummary)));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridTableReports.ItemsSource = BD.GetContext().HoursMonthlySummary.ToList();
            }
        }

        private void DGridTableReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TBTableReports_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBTableReports.Clear();
        }
    }
}
