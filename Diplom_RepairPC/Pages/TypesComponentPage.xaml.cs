using Diplom_RepairPC.Classes;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diplom_RepairPC.Pages
{
    public partial class TypesComponentPage : Page
    {
        public TypesComponentPage()
        {
            InitializeComponent();
            
            ComboBoxSortByName.SelectedIndex = 0;
            LoadData();
            DataGridsClass.DataGridTypeComponent = DataGridTypeComponent;
            DataGridsClass.DataGridTypesComponent = DataGridTypesComponent;
        }

        private void LoadData()
        {
            try
            {
                var typeComponent = App.Context.Diplom_TypeComponent.ToList();
                switch (ComboBoxSortByName.SelectedIndex)
                {
                    case 0:
                        typeComponent = typeComponent.OrderBy(x => x.Name).ToList();
                        break;
                    case 1:
                        typeComponent = typeComponent.OrderByDescending(x => x.Name).ToList();
                        break;
                    default:
                        typeComponent = typeComponent.OrderBy(x => x.Name).ToList();
                        break;
                }
                if (!String.IsNullOrWhiteSpace(TextBoxSearch.Text))
                    typeComponent = typeComponent.Where(x => x.Name.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                else
                    typeComponent = typeComponent.ToList();
                DataGridTypeComponent.ItemsSource = typeComponent;
                DataGridTypesComponent.ItemsSource = typeComponent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var typeComponent = (sender as TextBlock).DataContext as Entites.Diplom_TypeComponent;
            if (typeComponent.ID == -1)
            {
                MessageBox.Show("Изменение невозможно", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainTextBlockClass.MainTextBlock.Text = $"Изменение вида комплектующего {typeComponent.Name}";
            MainFrameClass.MainFrame.Navigate(new AddEditTypeComponentPage(
                (sender as TextBlock).DataContext as Entites.Diplom_TypeComponent));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var typeComponentDel = (sender as TextBlock).DataContext as Entites.Diplom_TypeComponent;
            if (typeComponentDel.ID == -1)
            {
                MessageBox.Show("Удаление невозможно", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить вид комплектующего?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    App.Context.Diplom_TypeComponent.Remove(typeComponentDel);
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
            MainTextBlockClass.MainTextBlock.Text = $"Добавление нового вида комплектующего";
            MainFrameClass.MainFrame.Navigate(new AddEditTypeComponentPage(null));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {App.CurrentUser.Login}";
            MainFrameClass.MainFrame.GoBack();
        }

        private void ComboBoxSortByName_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    for (i = 0; i < DataGridTypesComponent.Columns.Count; i++)
                    {
                        Excel.Range range = (Excel.Range)worksheet.Cells[1, i + 1];
                        worksheet.Cells[1, i + 1].Font.Bold = true;
                        worksheet.Columns[i + 1].ColumnWidth = 30;
                        range.Value2 = DataGridTypesComponent.Columns[i].Header;
                    }
                    for (i = 0; i <= DataGridTypesComponent.Columns.Count; i++)
                    {
                        for (int j = 0; j < DataGridTypesComponent.Items.Count; j++)
                        {
                            try
                            {
                                TextBlock textBlock = DataGridTypesComponent.Columns[i].
                                GetCellContent(DataGridTypesComponent.Items[j]) as TextBlock;
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