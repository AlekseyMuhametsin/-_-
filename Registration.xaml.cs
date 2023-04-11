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
    /// Логика взаимодействия для Registration1.xaml
    /// </summary>
    public partial class Registration1 : Page
    {
        private User _currenUser = new User();

        public Registration1()
        {
            InitializeComponent();
            ComboBoxRole.ItemsSource = BD.GetContext().Role.ToList();
            DataContext = _currenUser;
        }

        private void ButtonRegistrClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currenUser.FIOUser))
                errors.AppendLine("Укажите ФИО Пользователя");
            if (string.IsNullOrWhiteSpace(_currenUser.Login))
                errors.AppendLine("Укажите Логин Пользователя");
            if (string.IsNullOrWhiteSpace(_currenUser.Password))
                errors.AppendLine("Укажите Пароль Пользователя");
            if (_currenUser.RoleUser == null)
                errors.AppendLine("Укажите Роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            if (_currenUser.UserNumber == 0)
                BD.GetContext().User.Add(_currenUser);
            try
            {
                var log = BD.GetContext().User.Where(a => a.Login == TextLogin.Text).FirstOrDefault();
                if (log == null)
                {
                    BD.GetContext().SaveChanges();
                    borderosh.Visibility = Visibility.Visible;
                }                       
                else
                    MessageBox.Show("Такой Логин уже существует!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

        private void ButtonEditorClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new RegistrationEditor();
        }
    }
}
