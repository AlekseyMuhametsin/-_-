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
    /// Логика взаимодействия для NoShowEditor.xaml
    /// </summary>
    public partial class NoShowEditor : Page
    {
        BD bd = new BD();

        public NoShowEditor()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridTableReports.ItemsSource = BD.GetContext().AbsentForAReason.ToList();
            }
        }

        private void TBTableReports_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBTableReports.Clear();
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
        private void DGridTableReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Poisk_Table(object sender, TextChangedEventArgs e)
        {
            var result1 = bd.AbsentForAReason.Join(bd.Staff,
            a => a.PersonnelNumberAbsence,
            b => b.PersonnelNumber,
            (a, b) => new { a.PersonnelNumberAbsence, b.Initials, a.CodeAbsence, a.DaysAbsence, a.HoursAbsence, a.MonthAbsence }).ToList();


            var WihtThisName = result1.Where(w => w.Initials.StartsWith(TBTableReports.Text)).ToList();
            DGridTableReports.ItemsSource = WihtThisName;
        }

        private void ButtonNewStaffClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new NewNoShow();
        }

        private void ButtonEditClick(object sender, RoutedEventArgs e)
        {
            var AbsentForAReason1 = DGridTableReports.SelectedItems.Cast<AbsentForAReason>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить {AbsentForAReason1.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BD.GetContext().AbsentForAReason.RemoveRange(AbsentForAReason1);
                    BD.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridTableReports.ItemsSource = BD.GetContext().AbsentForAReason.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new NoShowChanges(((sender as Button).DataContext as AbsentForAReason)));
        }
    }
}
