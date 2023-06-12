using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Diplom_RepairPC.Classes;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diplom_RepairPC.Pages
{
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();

            ComboBoxSortByFIO.SelectedIndex = 0;
            ComboBoxFilterByCard.SelectedIndex = 0;
            LoadData();
            ListViewsClass.ListViewClient = ListViewClients;
            DataGridsClass.DataGridClients = DataGridClients;
        }

        private void LoadData()
        {
            try
            {
                var clients = App.Context.Diplom_Client.ToList();
                switch (ComboBoxSortByFIO.SelectedIndex)
                {
                    case 0:
                        clients = clients.OrderBy(x => x.FIO).ToList();
                        break;
                    case 1:
                        clients = clients.OrderByDescending(x => x.FIO).ToList();
                        break;
                    default:
                        clients = clients.OrderBy(x => x.FIO).ToList();
                        break;
                }
                switch (ComboBoxFilterByCard.SelectedIndex)
                {
                    case 1:
                        clients = clients.Where(x => x.DiscountCard == true).ToList();
                        break;
                    case 2:
                        clients = clients.Where(x => x.DiscountCard == false).ToList();
                        break;
                    default:
                        clients = clients.ToList();
                        break;
                }
                if (!String.IsNullOrWhiteSpace(TextBoxSearch.Text))
                    clients = clients.Where(x => x.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
                else
                    clients = clients.ToList();
                ListViewClients.ItemsSource = clients;
                DataGridClients.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button).DataContext as Entites.Diplom_Client;
            MainTextBlockClass.MainTextBlock.Text = $"Изменение клиента {client.FIO}";
            MainFrameClass.MainFrame.Navigate(new AddEditClientPage(
                (sender as Button).DataContext as Entites.Diplom_Client));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить клиента?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var clientDel = (sender as Button).DataContext as Entites.Diplom_Client;
                    App.Context.Diplom_Client.Remove(clientDel);
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
            MainTextBlockClass.MainTextBlock.Text = $"Добавление нового клиента";
            MainFrameClass.MainFrame.Navigate(new AddEditClientPage(null));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {App.CurrentUser.Login}";
            MainFrameClass.MainFrame.GoBack();
        }

        private void ComboBoxSortByFIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void ComboBoxFilterByCard_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    for (i = 0; i < DataGridClients.Columns.Count; i++)
                    {
                        Excel.Range range = (Excel.Range)worksheet.Cells[1, i + 1];
                        worksheet.Cells[1, i + 1].Font.Bold = true;
                        worksheet.Columns[i + 1].ColumnWidth = 30;
                        range.Value2 = DataGridClients.Columns[i].Header;
                    }
                    for (i = 0; i <= DataGridClients.Columns.Count; i++)
                    {
                        for (int j = 0; j < DataGridClients.Items.Count; j++)
                        {
                            try
                            {
                                TextBlock textBlock = DataGridClients.Columns[i].
                                GetCellContent(DataGridClients.Items[j]) as TextBlock;
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