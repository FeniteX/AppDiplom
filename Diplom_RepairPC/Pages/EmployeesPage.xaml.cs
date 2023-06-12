using Diplom_RepairPC.Classes;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diplom_RepairPC.Pages
{
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            
            ComboBoxSortByFIO.SelectedIndex = 0;
            ComboBoxSortByCostWork.SelectedIndex = 0;
            LoadData();
            ListViewsClass.ListViewEmployee = ListViewEmployees;
            DataGridsClass.DataGridEmployees = DataGridEmployees;
        }

        int numComboBox = 1;

        private void LoadData()
        {
            try
            {
                var employees = App.Context.Diplom_Employee.ToList();
                switch (numComboBox)
                {
                    case 1:
                        switch (ComboBoxSortByFIO.SelectedIndex)
                        {
                            case 0:
                                employees = employees.OrderBy(x => x.FIO).ToList();
                                break;
                            case 1:
                                employees = employees.OrderByDescending(x => x.FIO).ToList();
                                break;
                            default:
                                employees = employees.OrderBy(x => x.FIO).ToList();
                                break;
                        }
                        break;
                    case 2:
                        switch (ComboBoxSortByCostWork.SelectedIndex)
                        {
                            case 0:
                                employees = employees.OrderBy(x => x.CostWork).ToList();
                                break;
                            case 1:
                                employees = employees.OrderByDescending(x => x.CostWork).ToList();
                                break;
                            default:
                                employees = employees.ToList();
                                break;
                        }
                        break;
                }
                if (!String.IsNullOrWhiteSpace(TextBoxSearch.Text))
                    employees = employees.Where(x => x.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                else
                    employees = employees.ToList();
                ListViewEmployees.ItemsSource = employees;
                DataGridEmployees.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var employee = (sender as Button).DataContext as Entites.Diplom_Employee;
            MainTextBlockClass.MainTextBlock.Text = $"Изменение сотрудника {employee.FIO}";
            MainFrameClass.MainFrame.Navigate(new AddEditEmployeePage(
                (sender as Button).DataContext as Entites.Diplom_Employee));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить сотрудника?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var employeeDel = (sender as Button).DataContext as Entites.Diplom_Employee;
                    App.Context.Diplom_Employee.Remove(employeeDel);
                    App.Context.SaveChanges();
                    MessageBox.Show("Удаление прошло успешно", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = $"Добавление нового сотрудника";
            MainFrameClass.MainFrame.Navigate(new AddEditEmployeePage(null));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {App.CurrentUser.Login}";
            MainFrameClass.MainFrame.GoBack();
        }

        private void ComboBoxSortByFIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numComboBox = 1;
            LoadData();
        }

        private void ComboBoxSortByCostWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numComboBox = 2;
            LoadData();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void BtnSaveExcel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите сохранить в Excel?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Файлы Excel (*.xls; *.xlsx) | *.xls; *.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    Excel.Application excel = new Excel.Application();
                    excel.Visible = true;
                    Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                    int i;
                    for (i = 0; i < DataGridEmployees.Columns.Count; i++)
                    {
                        Excel.Range range = (Excel.Range)worksheet.Cells[1, i + 1];
                        worksheet.Cells[1, i + 1].Font.Bold = true;
                        worksheet.Columns[i + 1].ColumnWidth = 30;
                        range.Value2 = DataGridEmployees.Columns[i].Header;
                    }
                    for (i = 0; i <= DataGridEmployees.Columns.Count; i++)
                    {
                        for (int j = 0; j < DataGridEmployees.Items.Count; j++)
                        {
                            try
                            {
                                TextBlock textBlock = DataGridEmployees.Columns[i].
                                GetCellContent(DataGridEmployees.Items[j]) as TextBlock;
                                if (textBlock == null)
                                    continue;
                                Excel.Range range = (Excel.Range)worksheet.Cells[j + 2, i + 1];
                                range.Value2 = textBlock.Text;
                            }
                            catch { }
                        }
                    }
                    workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    excel.Quit();
                }
            }
        }
    }
}