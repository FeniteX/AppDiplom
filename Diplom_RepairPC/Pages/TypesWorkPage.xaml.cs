using Diplom_RepairPC.Classes;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diplom_RepairPC.Pages
{
    public partial class TypesWorkPage : Page
    {
        public TypesWorkPage()
        {
            InitializeComponent();

            ComboBoxSortByCost.SelectedIndex = 0;
            LoadData();
            DataGridsClass.DataGridTypeWork = DataGridTypeWork;
            DataGridsClass.DataGridTypesWork = DataGridTypesWork;
        }

        private void LoadData()
        {
            try
            {
                var typeWork= App.Context.Diplom_TypeWork.ToList();
                switch (ComboBoxSortByCost.SelectedIndex)
                {
                    case 0:
                        typeWork = typeWork.OrderBy(x => x.Cost).ToList();
                        break;
                    case 1:
                        typeWork = typeWork.OrderByDescending(x => x.Cost).ToList();
                        break;
                    default:
                        typeWork = typeWork.OrderBy(x => x.Cost).ToList();
                        break;
                }
                if (!String.IsNullOrWhiteSpace(TextBoxSearch.Text))
                    typeWork = typeWork.Where(x => x.Name.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                else
                    typeWork = typeWork.ToList();
                DataGridTypeWork.ItemsSource = typeWork;
                DataGridTypesWork.ItemsSource = typeWork;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var typeWork = (sender as TextBlock).DataContext as Entites.Diplom_TypeWork;
            MainTextBlockClass.MainTextBlock.Text = $"Изменение услуги {typeWork.Name}";
            MainFrameClass.MainFrame.Navigate(new AddEditTypeWorkPage(
                (sender as TextBlock).DataContext as Entites.Diplom_TypeWork));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить услугу?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var typeWorkDel = (sender as TextBlock).DataContext as Entites.Diplom_TypeWork;
                    App.Context.Diplom_TypeWork.Remove(typeWorkDel);
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
            MainTextBlockClass.MainTextBlock.Text = $"Добавление новой услуги";
            MainFrameClass.MainFrame.Navigate(new AddEditTypeWorkPage(null));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {App.CurrentUser.Login}";
            MainFrameClass.MainFrame.GoBack();
        }

        private void ComboBoxSortByCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                    for (i = 0; i < DataGridTypesWork.Columns.Count; i++)
                    {
                        Excel.Range range = (Excel.Range)worksheet.Cells[1, i + 1];
                        worksheet.Cells[1, i + 1].Font.Bold = true;
                        worksheet.Columns[i + 1].ColumnWidth = 30;
                        range.Value2 = DataGridTypesWork.Columns[i].Header;
                    }
                    for (i = 0; i <= DataGridTypesWork.Columns.Count; i++)
                    {
                        for (int j = 0; j < DataGridTypesWork.Items.Count; j++)
                        {
                            try
                            {
                                TextBlock textBlock = DataGridTypesWork.Columns[i].
                                GetCellContent(DataGridTypesWork.Items[j]) as TextBlock;
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