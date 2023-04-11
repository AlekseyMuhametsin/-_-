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
    /// Логика взаимодействия для NewLabourPayment.xaml
    /// </summary>
    public partial class NewLabourPayment : Page
    {
        BD bd = new BD();

        private DataForPayroll _curren = new DataForPayroll();

        public NewLabourPayment()
        {
            InitializeComponent();
            ComboBoxPersonnelNumber.ItemsSource = BD.GetContext().Staff.ToList();
            ComboBoxPaymentType.ItemsSource = BD.GetContext().PaymentTypeCode.ToList();
            ComboBoxOffsettingAccount.ItemsSource = BD.GetContext().OffsettingAccount.ToList();
            ComboBoxMonth.ItemsSource = BD.GetContext().Month.ToList();
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

        private void ButtonRegistrClick(object sender, RoutedEventArgs e)
        {
            var id = Convert.ToInt32((ComboBoxPersonnelNumber.SelectedItem as Staff).PersonnelNumber);
            var idPaymentType = Convert.ToInt32((ComboBoxPaymentType.SelectedItem as PaymentTypeCode).PaymentType);
            var idCodeOffsettingAccount = Convert.ToInt32((ComboBoxOffsettingAccount.SelectedItem as OffsettingAccount).CodeOffsettingAccount);
            var mouth = ComboBoxMonth.Text;
            var tabnum = bd.HoursMonthlySummary.Where(c => (c.PersonnelNumberSum == id) && (c.MonthSum == mouth)).Select(a => a.NumberSum).ToList().Last();
            _curren.DaysSalary = Convert.ToInt32(TextDaysSalary.Text);
            _curren.HoursSalary = Convert.ToInt32(TextHoursSalary.Text);
            _curren.MonthSalary = Convert.ToInt32(tabnum);
            _curren.PaymentTypeCodeSalary = Convert.ToInt32(idPaymentType);
            _curren.OffsettingAccountSalary = Convert.ToInt32(idCodeOffsettingAccount);

            StringBuilder errors = new StringBuilder();
            if (ComboBoxPersonnelNumber.SelectedIndex == -1)
                errors.AppendLine("Укажите Сотрудника");
            if (_curren.PaymentTypeCodeSalary == 0)
                errors.AppendLine("Укажите Код вида оплаты ");
            if (_curren.OffsettingAccountSalary == 0)
                errors.AppendLine("Укажите Корреспондирующий счет");
            //if (_curren.MonthSalary == 0)
            //    errors.AppendLine("Укажите М");
            if (string.IsNullOrWhiteSpace(_curren.DaysSalary.ToString()))
                errors.AppendLine("Укажите День");
            if (string.IsNullOrWhiteSpace(_curren.HoursSalary.ToString()))
                errors.AppendLine("Укажите Время");

            //AccountingForWorkingHoursSum = номер


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _curren.PersonnelNumberSalary = (ComboBoxPersonnelNumber.SelectedItem as Staff).PersonnelNumber;
            if (_curren.NumberSalary == 0)
                BD.GetContext().DataForPayroll.Add(_curren);
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

        private void ComboBoxPersonnelNumber_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void ComboBoxPaymentType_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void ComboBoxOffsettingAccount_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void ComboBoxMonth_DropDownClosed(object sender, EventArgs e)
        {
            var id = Convert.ToInt32((ComboBoxPersonnelNumber.SelectedItem as Staff).PersonnelNumber);
            var mouth = ComboBoxMonth.Text;
            var maxday = bd.AccountingForWorkingHours.Where(a => (a.PersonnelNumberAccounting == id) && (a.MonthAccounting == mouth)).ToList().Last();
            TextDaysSalary.Text = maxday.DaysAccounting.ToString();
            var sumdays = bd.AccountingForWorkingHours.Where(a => (a.PersonnelNumberAccounting == id) && (a.MonthAccounting == mouth)).ToList();
            var sumdays2 = sumdays.Sum(s => s.HoursAccounting);
            TextHoursSalary.Text = sumdays2.ToString();
        }
    }
}
