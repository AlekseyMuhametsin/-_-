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
    /// Логика взаимодействия для EditorEmployee.xaml
    /// </summary>
    public partial class EditorEmployee : Page
    {
        private Staff _currenStaff = new Staff();
        public EditorEmployee(Staff selectedStaff)
        {
            InitializeComponent();
            if (selectedStaff != null)
                _currenStaff = selectedStaff;

            ComboBoxDepartmentStaff.ItemsSource = BD.GetContext().Department.ToList();
            ComboBoxDecryptionStaff.ItemsSource = BD.GetContext().Decryption.ToList();
            DataContext = _currenStaff;
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

        private void ButtonRegistrClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currenStaff.Initials))
                errors.AppendLine("Укажите Инициалы Пользователя");
            if (string.IsNullOrWhiteSpace(_currenStaff.PositionStaff))
                errors.AppendLine("Укажите Расшифровка Пользователя");
            if (_currenStaff.DecryptionStaff == null)
                errors.AppendLine("Укажите Должность Пользователя");
            if (_currenStaff.DepartmentStaff == null)
                errors.AppendLine("Укажите Отдел Пользователя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currenStaff.PersonnelNumber == 0)
                BD.GetContext().Staff.Add(_currenStaff);
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
    }
}
