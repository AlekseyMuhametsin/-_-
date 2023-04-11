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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Page = System.Windows.Controls.Page;

namespace КП_МДК._03
{
    /// <summary>
    /// Логика взаимодействия для ReportExcel.xaml
    /// </summary>
    public partial class ReportExcel : Page
    {
        BD bd = new BD();

        public ReportExcel()
        {
            InitializeComponent();

            DGridTableReports.ItemsSource = bd.AccountingForWorkingHours.Join(bd.DayCode,
                a => a.DayCodeAccounting,
                b => b.Sign,
                (a, b) => new { a.PersonnelNumberAccounting, a.DaysAccounting, b.Letter, a.HoursAccounting, a.MonthAccounting }).ToList();
        }

        private void ButtonExcelClick(object sender, RoutedEventArgs e)
        {
            var info = new BD().AccountingForWorkingHours.ToList();

            Application excel = new Application();
            Workbook workbook1 = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet = (Worksheet)workbook1.Sheets[1];

            ((Range)sheet.Cells[1, 1]).Value2 = "Табельный номер";
            ((Range)sheet.Cells[1, 2]).Value2 = "Инициалы";
            ((Range)sheet.Cells[1, 3]).Value2 = "День";
            ((Range)sheet.Cells[1, 4]).Value2 = "Явка на рабочее место";
            ((Range)sheet.Cells[1, 5]).Value2 = "Часы";
            ((Range)sheet.Cells[1, 6]).Value2 = "Месяц";


            for (int r = 0; r < info.Count(); r++)
            {
                ((Range)sheet.Cells[r + 2, 1]).Value2 = info[r].PersonnelNumberAccounting;
                ((Range)sheet.Cells[r + 2, 2]).Value2 = info[r].Staff.Initials;
                ((Range)sheet.Cells[r + 2, 3]).Value2 = info[r].DaysAccounting;
                //((Range)sheet.Cells[r + 2, 3]).Value2 = info[r].DayCodeAccounting;
                ((Range)sheet.Cells[r + 2, 4]).Value2 = info[r].DayCode.Letter;
                ((Range)sheet.Cells[r + 2, 5]).Value2 = info[r].HoursAccounting;
                ((Range)sheet.Cells[r + 2, 6]).Value2 = info[r].MonthAccounting;
            }
            excel.Visible = true;


        }


        private void DGridTableReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Poisk_Table(object sender, TextChangedEventArgs e)
        {
            var result1 = bd.AccountingForWorkingHours.Join(bd.Staff,
            a => a.PersonnelNumberAccounting,
            b => b.PersonnelNumber,
            (a, b) => new { a.PersonnelNumberAccounting, b.Initials, a.DaysAccounting, a.DayCodeAccounting, a.HoursAccounting, a.MonthAccounting }).ToList();



            if (DGridTableReports != null)
            {
                var WihtThisName = result1.Where(w => w.Initials.StartsWith(TBTableReports.Text)).ToList();
                DGridTableReports.ItemsSource = WihtThisName;
            }

        }
        private void TBTableReports_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TBTableReports.Clear();
        }
    }
}
