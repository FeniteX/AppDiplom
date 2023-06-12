using Diplom_RepairPC.Classes;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diplom_RepairPC.Pages
{
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();

            ComboBoxSortByCost.SelectedIndex = 0;
            LoadData();
            DataGridsClass.DataGridOrder = DataGridOrder;
            DataGridsClass.DataGridOrders = DataGridOrders;
        }

        private void LoadData()
        {
            try
            {
                var order = App.Context.Diplom_Order.ToList();
                switch (ComboBoxSortByCost.SelectedIndex)
                {
                    case 0:
                        order = order.OrderBy(x => x.Cost).ToList();
                        break;
                    case 1:
                        order = order.OrderByDescending(x => x.Cost).ToList();
                        break;
                    default:
                        order = order.OrderBy(x => x.Cost).ToList();
                        break;
                }
                if (!String.IsNullOrWhiteSpace(TextBoxSearch.Text))
                    order = order.Where(x => x.Diplom_Client.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower()) || 
                    x.Diplom_Employee.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                else
                    order = order.ToList();
                DataGridOrder.ItemsSource = order;
                DataGridOrders.ItemsSource = order;
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
            var order = (sender as TextBlock).DataContext as Entites.Diplom_Order;
            MainTextBlockClass.MainTextBlock.Text = $"Изменение заказа №{order.ID}";
            MainFrameClass.MainFrame.Navigate(new EditOrderPage(
                (sender as TextBlock).DataContext as Entites.Diplom_Order));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить заказ?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var orderComponentDel = App.Context.Diplom_OrderComponent.ToList();
                    orderComponentDel = orderComponentDel.Where(x => x.IDOrder == ((sender as TextBlock).
                        DataContext as Entites.Diplom_Order).ID).ToList();
                    var orderTypeWorkDel = App.Context.Diplom_OrderTypeWork.ToList();
                    orderTypeWorkDel = orderTypeWorkDel.Where(x => x.IDOrder == ((sender as TextBlock).
                        DataContext as Entites.Diplom_Order).ID).ToList();
                    var orderDel = (sender as TextBlock).DataContext as Entites.Diplom_Order;
                    App.Context.Diplom_OrderComponent.RemoveRange(orderComponentDel);
                    App.Context.Diplom_OrderTypeWork.RemoveRange(orderTypeWorkDel);
                    App.Context.Diplom_Order.Remove(orderDel);
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
            MainTextBlockClass.MainTextBlock.Text = $"Добавление нового заказа";
            MainFrameClass.MainFrame.Navigate(new AddOrderAdminPage());
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
                    for (i = 0; i < DataGridOrders.Columns.Count; i++)
                    {
                        Excel.Range range = (Excel.Range)worksheet.Cells[1, i + 1];
                        worksheet.Cells[1, i + 1].Font.Bold = true;
                        worksheet.Columns[i + 1].ColumnWidth = 30;
                        range.Value2 = DataGridOrders.Columns[i].Header;
                    }
                    for (i = 0; i <= DataGridOrders.Columns.Count; i++)
                    {
                        for (int j = 0; j < DataGridOrders.Items.Count; j++)
                        {
                            try
                            {
                                TextBlock textBlock = DataGridOrders.Columns[i].
                                GetCellContent(DataGridOrders.Items[j]) as TextBlock;
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

        private void BtnDetalied_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.Count <= 2)
            {
                Windows.OrderDetalied orderDetalied = new Windows.OrderDetalied(
                    (sender as TextBlock).DataContext as Entites.Diplom_Order);
                orderDetalied.Show();
            }
            else
                MessageBox.Show("Окно уже открыто", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}