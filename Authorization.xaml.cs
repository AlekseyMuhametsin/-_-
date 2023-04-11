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
using System.Windows.Shapes;

namespace КП_МДК._03
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void TextBLogin_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBLogin.Clear();
        }

        private void TextBParol_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBParol.Clear();
        }

        private void ButtonLoginClick(object sender, RoutedEventArgs e)
        {
            var login = TextBLogin.Text;
            var pas = TextBParol.Password;
            var db = new BD();
            var user = db.User.Where(u => u.Login == login && u.Password == pas).FirstOrDefault();
            if (user != null)
            {
                new MainWindow(user).Show();
                this.Close();
            }
            else
            { borderosh.Visibility = Visibility.Visible; }
        }

        private void TextBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
