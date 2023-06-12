using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class EditOrderPage : Page
    {
        public EditOrderPage(Entites.Diplom_Order order)
        {
            InitializeComponent();
            
            Order = order;
            ComboBoxClient.ItemsSource = App.Context.Diplom_Client.ToList();
            ComboBoxClient.SelectedItem = App.Context.Diplom_Client.Where(x => x.ID == order.IDClient).First();
            ComboBoxEmployee.ItemsSource = App.Context.Diplom_Employee.ToList();
            ComboBoxEmployee.SelectedItem = App.Context.Diplom_Employee.Where(x => x.ID == order.IDEmployee).First();
            ComboBoxComponent.ItemsSource = App.Context.Diplom_Component.ToList();
            ComboBoxTypeWork.ItemsSource = App.Context.Diplom_TypeWork.ToList();
            DataGridComponent.ItemsSource = App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == Order.ID).ToList();
            DataGridTypeWork.ItemsSource = App.Context.Diplom_OrderTypeWork.Where(x => x.IDOrder == Order.ID).ToList();
            if (Order.Status != "Новый") CheckBoxStatus.IsChecked = true;
        }

        private Entites.Diplom_Order Order = new Entites.Diplom_Order();
        private Entites.Diplom_OrderTypeWork orderTypeWork = new Entites.Diplom_OrderTypeWork();
        private Entites.Diplom_OrderComponent orderComponent = new Entites.Diplom_OrderComponent();

        private void BtnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxEmployee.SelectedIndex < 0)
            {
                MessageBox.Show("Сотрудника", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("При добавлении новых услуг или комплектующих старые записи удаляться, Вы действительно хотите продолжить?",
                "Вопрос", MessageBoxButton.OKCancel, MessageBoxImage.Warning) ==
                MessageBoxResult.OK)
            {
                try
                {
                    if (CheckBoxStatus.IsChecked == true)
                    {
                        Order.Status = "Завершённый";
                        Order.DateOrderEnd = DateTime.Now;
                    }
                    Order.IDEmployee = (ComboBoxEmployee.SelectedItem as Entites.Diplom_Employee).ID;
                    Order.IDClient = (ComboBoxClient.SelectedItem as Entites.Diplom_Client).ID;
                    App.Context.SaveChanges();
                    int idOrder = Order.ID;
                    int i;
                    if (ListViewTypeWork.Items.Count > 0)
                    {
                        var orderTypeWorkDel = App.Context.Diplom_OrderTypeWork.Where(x => x.IDOrder == Order.ID).ToList();
                        App.Context.Diplom_OrderTypeWork.RemoveRange(orderTypeWorkDel);
                        for (i = 0; i < ListViewTypeWork.Items.Count; i++)
                        {
                            orderTypeWork = new Entites.Diplom_OrderTypeWork();
                            orderTypeWork.IDOrder = idOrder;
                            orderTypeWork.IDTypeWork = (ListViewTypeWork.Items[i] as Entites.Diplom_TypeWork).ID;
                            App.Context.Diplom_OrderTypeWork.Add(orderTypeWork);
                        }
                    }
                    if (ListViewComponent.Items.Count > 0)
                    {
                        var orderComponentkDel = App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == Order.ID).ToList();
                        App.Context.Diplom_OrderComponent.RemoveRange(orderComponentkDel);
                        for (i = 0; i < ListViewComponent.Items.Count; i++)
                        {
                            orderComponent = new Entites.Diplom_OrderComponent();
                            orderComponent.IDOrder = idOrder;
                            orderComponent.IDComponent = (ListViewComponent.Items[i]
                                as Entites.Diplom_Component).ID;
                            App.Context.Diplom_OrderComponent.Add(orderComponent);
                        }
                    }
                    App.Context.SaveChanges();

                    ListViewComponent.Items.Clear();
                    ListViewTypeWork.Items.Clear();
                    decimal costWorkEmployee = App.Context.Diplom_Employee.Where(x => x.ID == Order.IDEmployee).First().CostWork;
                    ListViewTypeWork.ItemsSource = App.Context.Diplom_OrderTypeWork.Where(x => x.IDOrder == Order.ID).ToList();
                    int[] idTypeWork = new int[ListViewTypeWork.Items.Count];
                    for (i = 0; i < ListViewTypeWork.Items.Count; i++)
                    {
                        idTypeWork[i] = (ListViewTypeWork.Items[i] as Entites.Diplom_OrderTypeWork).IDTypeWork;
                    }
                    decimal costTypeWork = 0;
                    int time = 0;
                    for (i = 0; i < idTypeWork.Length; i++)
                    {
                        var tmp = idTypeWork[i];
                        costTypeWork += App.Context.Diplom_TypeWork.Where(x => x.ID == tmp).First().Cost;
                        time += App.Context.Diplom_TypeWork.Where(x => x.ID == tmp).First().TimeWork;
                    }
                    costWorkEmployee *= time;
                    ListViewComponent.ItemsSource = App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == Order.ID).ToList();
                    int[] idComponent = new int[ListViewComponent.Items.Count];
                    decimal costComponent = 0;
                    for (i = 0; i < ListViewComponent.Items.Count; i++)
                    {
                        idComponent[i] = (ListViewComponent.Items[i] as Entites.Diplom_OrderComponent).IDComponent;
                    }
                    for (i = 0; i < idComponent.Length; i++)
                    {
                        var tmp = idComponent[i];
                        costComponent += App.Context.Diplom_Component.Where(x => x.ID == tmp).First().Cost;
                    }
                    Order.Cost = costTypeWork + costComponent + costWorkEmployee;

                    App.Context.SaveChanges();
                    MessageBox.Show("Именение заказа прошло успешно", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DataGridsClass.DataGridOrder.ItemsSource = App.Context.Diplom_Order.ToList();
                    DataGridsClass.DataGridOrders.ItemsSource = App.Context.Diplom_Order.ToList();
                    MainTextBlockClass.MainTextBlock.Text = "Заказы";
                    MainFrameClass.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите вернуться, введённые данные не будут сохранены",
                "Вопрос", MessageBoxButton.OKCancel, MessageBoxImage.Warning) ==
                MessageBoxResult.OK)
            {
                MainTextBlockClass.MainTextBlock.Text = "Заказы";
                MainFrameClass.MainFrame.GoBack();
            }
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.Count <= 2)
            {
                Windows.AddClientWindow addClientWindow = new Windows.AddClientWindow();
                addClientWindow.Show();
            }
            else
                MessageBox.Show("Окно уже открыто", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAddTypeWork_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTypeWork.SelectedIndex >= 0)
            {
                bool result = true;
                for (int i = 0; i < ListViewTypeWork.Items.Count; i++)
                {
                    if ((ComboBoxTypeWork.SelectedItem as Entites.Diplom_TypeWork).ID ==
                        (ListViewTypeWork.Items[i] as Entites.Diplom_TypeWork).ID)
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                    ListViewTypeWork.Items.Add(ComboBoxTypeWork.SelectedItem);
            }
        }

        private void BtnCleanTypeWork_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите очистить список услуг?", "Вопрос",
                MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                ListViewTypeWork.Items.Clear();
        }

        private void BtnAddComponent_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxComponent.SelectedIndex >= 0)
            {
                bool result = true;
                for (int i = 0; i < ListViewComponent.Items.Count; i++)
                {
                    if ((ComboBoxComponent.SelectedItem as Entites.Diplom_Component).ID ==
                        (ListViewComponent.Items[i] as Entites.Diplom_Component).ID)
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                    ListViewComponent.Items.Add(ComboBoxComponent.SelectedItem);
            }
        }

        private void BtnCleanComponent_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите очистить список комплектующих?", "Вопрос",
                MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                ListViewComponent.Items.Clear();
        }
    }
}