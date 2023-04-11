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
    /// Логика взаимодействия для AddReports.xaml
    /// </summary>
    public partial class AddReports : Page
    {
        private AccountingForWorkingHours _curren = new AccountingForWorkingHours();

        public AddReports()
        {
            InitializeComponent();
            ComboBoxInitials.ItemsSource = BD.GetContext().Staff.ToList();
            ComboBoxDayCode.ItemsSource = BD.GetContext().DayCode.ToList();
            ComboBoxMonth.ItemsSource = BD.GetContext().Month.ToList(); 
            DataContext = _curren;
        }

        private void ButtonFinishedClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (ComboBoxInitials.SelectedIndex == -1)
                errors.AppendLine("Укажите Сотрудника");
            if (ComboBoxDayCode.SelectedIndex == -1)
                errors.AppendLine("Укажите Явку на рабочее место");
            if (string.IsNullOrWhiteSpace(_curren.DaysAccounting.ToString()))
                errors.AppendLine("Укажите День");
            if (string.IsNullOrWhiteSpace(_curren.HoursAccounting.ToString()))
                errors.AppendLine("Укажите Время");
            if (_curren.MonthAccounting == null)
                errors.AppendLine("Укажите Месяц");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _curren.PersonnelNumberAccounting = (ComboBoxInitials.SelectedItem as Staff).PersonnelNumber;
            _curren.DayCodeAccounting = (ComboBoxDayCode.SelectedItem as DayCode).Sign;
            if (_curren.NumberAccounting == 0)
                BD.GetContext().AccountingForWorkingHours.Add(_curren);
            try
            {
                BD.GetContext().SaveChanges();
                borderosh.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

        private void TextDen_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextChas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxTN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
