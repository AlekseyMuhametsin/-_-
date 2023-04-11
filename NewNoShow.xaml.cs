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
    /// Логика взаимодействия для NewNoShow.xaml
    /// </summary>
    public partial class NewNoShow : Page
    {
        BD bd = new BD();

        private AbsentForAReason _curren = new AbsentForAReason();

        public NewNoShow()
        {
            InitializeComponent();

            ComboBoxPersonnelNumber.ItemsSource = BD.GetContext().Staff.ToList();
            ComboBoxMonth.ItemsSource = BD.GetContext().Month.ToList();
            ComboBoxCode.ItemsSource = BD.GetContext().DayCode.ToList();
            DataContext = _curren;
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

        private void ButtonRegistrClick(object sender, RoutedEventArgs e)
        {
            _curren.DaysAbsence = Convert.ToInt32(TextDays.Text);
            _curren.HoursAbsence = Convert.ToInt32(TextHours.Text);

            StringBuilder errors = new StringBuilder();
            if (ComboBoxPersonnelNumber.SelectedIndex == -1)
                errors.AppendLine("Укажите Сотрудника");
            if (_curren.MonthAbsence == null)
                errors.AppendLine("Укажите Месяц"); 
             if (ComboBoxCode.SelectedIndex == -1)
                errors.AppendLine("Укажите Явку");
            if (string.IsNullOrWhiteSpace(_curren.DaysAbsence.ToString()))
                errors.AppendLine("Укажите День");
            if (string.IsNullOrWhiteSpace(_curren.HoursAbsence.ToString()))
                errors.AppendLine("Укажите Время");

            //AccountingForWorkingHoursSum = номер


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _curren.PersonnelNumberAbsence = (ComboBoxPersonnelNumber.SelectedItem as Staff).PersonnelNumber;
            _curren.CodeAbsence = (ComboBoxCode.SelectedItem as DayCode).Sign;
            if (_curren.NumberAbsence == 0)
                BD.GetContext().AbsentForAReason.Add(_curren);
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
            
        }

        private void ComboBoCod_DropDownClosed(object sender, EventArgs e)
        {
            if ((ComboBoxPersonnelNumber.Text != "") && (ComboBoxMonth.Text != "") && (ComboBoxCode.Text !=""))
            {
                var id = Convert.ToInt32((ComboBoxPersonnelNumber.SelectedItem as Staff).PersonnelNumber);
                var mouth = ComboBoxMonth.Text;
                var yavka = (ComboBoxCode.SelectedItem as DayCode).Sign;
                var maxday1 = bd.AccountingForWorkingHours.Join(bd.DayCode,
                    a => a.DayCodeAccounting,
                    b => b.Sign,
                    (a,b) => new { a.MonthAccounting, a.PersonnelNumberAccounting, a.DayCodeAccounting, a.HoursAccounting}).
                     Where(c => (c.PersonnelNumberAccounting == id) && (c.MonthAccounting == mouth) && (c.DayCodeAccounting == yavka)).ToList();
                var maxday = maxday1.Count();
                var kol = maxday1.Sum(p => p.HoursAccounting);
                TextDays.Text = maxday.ToString();
                TextHours.Text = kol.ToString();
                //TextDays.Text = maxday.DaysAccounting.ToString();
                //var sumdays = bd.AccountingForWorkingHours.Where(a => (a.PersonnelNumberAccounting == id) && (a.MonthAccounting == mouth)).ToList();
                //var sumdays2 = sumdays.Sum(s => s.HoursAccounting);
                //TextPosition.Text = sumdays2.ToString();


            }
        }
    }
}
