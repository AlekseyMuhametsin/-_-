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
    /// Логика взаимодействия для CountZP.xaml
    /// </summary>
    public partial class CountZP : Page
    {
        BD bd = new BD();

        private HoursMonthlySummary _curren = new HoursMonthlySummary();

        public CountZP()
        {
            InitializeComponent();
            ComboBoxPersonnelNumberSum.ItemsSource = BD.GetContext().Staff.ToList();
            ComboBoxMonthSum.ItemsSource = BD.GetContext().Month.ToList();
            DataContext = _curren;
        }
        private void ButtonRegistrClick(object sender, RoutedEventArgs e)
        {
            _curren.SumDays = Convert.ToInt32(TextInitials.Text);
            _curren.SumHours = Convert.ToInt32(TextPosition.Text);

            StringBuilder errors = new StringBuilder();
            if (ComboBoxPersonnelNumberSum.SelectedIndex == -1)
                errors.AppendLine("Укажите Сотрудника");
            if (_curren.MonthSum == null)
                errors.AppendLine("Укажите Месяц");
            if (string.IsNullOrWhiteSpace(_curren.SumDays.ToString()))
                errors.AppendLine("Укажите День");
            if (string.IsNullOrWhiteSpace(_curren.SumHours.ToString()))
                errors.AppendLine("Укажите Время");

            //AccountingForWorkingHoursSum = номер


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _curren.PersonnelNumberSum = (ComboBoxPersonnelNumberSum.SelectedItem as Staff).PersonnelNumber;
           
            if (_curren.NumberSum == 0)
                BD.GetContext().HoursMonthlySummary.Add(_curren);
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

        private void ComboBoxSubdivision_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextInitials_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextPosition_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxPersonnelNumberSum_DropDownClosed(object sender, EventArgs e)
        {
            

            
        }

        private void ComboBoxMonthSum_DropDownClosed(object sender, EventArgs e)
        {
            
            if ((ComboBoxPersonnelNumberSum.Text != "") && (ComboBoxMonthSum.Text != ""))
            {
                var id = Convert.ToInt32((ComboBoxPersonnelNumberSum.SelectedItem as Staff).PersonnelNumber);
                var mouth = ComboBoxMonthSum.Text;
                var maxday = bd.AccountingForWorkingHours.Where(a => (a.PersonnelNumberAccounting == id) && (a.MonthAccounting == mouth)).ToList().Last();
                TextInitials.Text = maxday.DaysAccounting.ToString();
                var sumdays = bd.AccountingForWorkingHours.Where(a => (a.PersonnelNumberAccounting == id) && (a.MonthAccounting == mouth)).ToList();
                var sumdays2 = sumdays.Sum(s => s.HoursAccounting);
                TextPosition.Text = sumdays2.ToString();


            }



        }
    }
}
