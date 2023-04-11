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
    /// Логика взаимодействия для RegistrationEditor.xaml
    /// </summary>
    public partial class RegistrationEditor : Page
    {
        public RegistrationEditor()
        {
            InitializeComponent();
            //DGUser.ItemsSource = BD_KP.GetContext().User.ToList();
        }

        private void ButtonRemovalClick(object sender, RoutedEventArgs e)
        {
            var User = DGUser.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить {User.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BD.GetContext().User.RemoveRange(User);
                    BD.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGUser.ItemsSource = BD.GetContext().User.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BD.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGUser.ItemsSource = BD.GetContext().User.ToList();
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

        private void DGUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
