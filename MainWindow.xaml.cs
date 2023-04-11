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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        public MainWindow(User users)
        {
            InitializeComponent();
            MainFrame.Navigate(new EmployeeRegistration());
            Manager.MainFrame = MainFrame;
            user = users;
            FIO_Text.Text = users.FIOUser;
            if (users.RoleUser == "Администратор") MainFrame.Content = new Administrator1();
            else MainFrame.Content = new Accountant();
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void ButtonExitClick(object sender, RoutedEventArgs e)
        {
            Authorization win2 = new Authorization();
            win2.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
