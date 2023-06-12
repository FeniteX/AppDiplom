using Diplom_RepairPC.Classes;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diplom_RepairPC.Pages
{
    public partial class ComponentsPage : Page
    {
        public ComponentsPage()
        {
            InitializeComponent();

            ComboBoxSortByCost.SelectedIndex = 0;
            ComboBoxFilterByTypeComponent.SelectedIndex = 0;
            LoadData();
            DataGridsClass.DataGridComponent = DataGridComponent;
            DataGridsClass.DataGridComponents = DataGridComponents;
            ComboBoxFilterByTypeComponent.ItemsSource = App.Context.Diplom_TypeComponent.ToList();
        }

        private void LoadData()
        {
            try
            {
                var component = App.Context.Diplom_Component.ToList();
                switch (ComboBoxSortByCost.SelectedIndex)
                {
                    case 0:
                        component = component.OrderBy(x => x.Cost).ToList();
                        break;
                    case 1:
                        component = component.OrderByDescending(x => x.Cost).ToList();
                        break;
                    default:
                        component = component.OrderBy(x => x.Cost).ToList();
                        break;
                }
                if ((ComboBoxFilterByTypeComponent.SelectedItem as Entites.Diplom_TypeComponent).ID == -1)
                    component = component.ToList();
                else
                    component = component.Where(x => x.IDTypeComponent == 
                    (ComboBoxFilterByTypeComponent.SelectedItem as Entites.Diplom_TypeComponent).ID).ToList();
                if (!String.IsNullOrWhiteSpace(TextBoxSearch.Text))
                    component = component.Where(x => x.Name.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                else
                    component = component.ToList();
                DataGridComponent.ItemsSource = component;
                DataGridComponents.ItemsSource = component;
            }
            catch (Exception ex)
            {
                if (ex.Message != "Ссылка на объект не указывает на экземпляр объекта.")
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var component = (sender as TextBlock).DataContext as Entites.Diplom_Component;
            MainTextBlockClass.MainTextBlock.Text = $"Изменение комплектующего {component.Name}";
            MainFrameClass.MainFrame.Navigate(new AddEditComponentPage(
                (sender as TextBlock).DataContext as Entites.Diplom_Component));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить комплектующее?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var componentDel = (sender as TextBlock).DataContext as Entites.Diplom_Component;
                    App.Context.Diplom_Component.Remove(componentDel);
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
            MainTextBlockClass.MainTextBlock.Text = $"Добавление нового комплектующего";
            MainFrameClass.MainFrame.Navigate(new AddEditComponentPage(null));
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

        private void ComboBoxFilterByTypeComponent_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    for (i = 0; i < DataGridComponents.Columns.Count; i++)
                    {
                        Excel.Range range = (Excel.Range)worksheet.Cells[1, i + 1];
                        worksheet.Cells[1, i + 1].Font.Bold = true;
                        worksheet.Columns[i + 1].ColumnWidth = 30;
                        range.Value2 = DataGridComponents.Columns[i].Header;
                    }
                    for (i = 0; i <= DataGridComponents.Columns.Count; i++)
                    {
                        for (int j = 0; j < DataGridComponents.Items.Count; j++)
                        {
                            try
                            {
                                TextBlock textBlock = DataGridComponents.Columns[i].
                                GetCellContent(DataGridComponents.Items[j]) as TextBlock;
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

        private void BtnCharacteristics_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.Count <= 2)
            {
                Windows.CharacteristicsComponentWindow characteristicsComponentWindow = new Windows.CharacteristicsComponentWindow(
                    (sender as TextBlock).DataContext as Entites.Diplom_Component);
                characteristicsComponentWindow.Show();
            }
            else
                MessageBox.Show("Окно уже открыто", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}