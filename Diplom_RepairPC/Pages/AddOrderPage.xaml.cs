using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class AddOrderPage : Page
    {
        public AddOrderPage()
        {
            InitializeComponent();

            ComboBoxsClass.ComboBoxClient = ComboBoxClient;
            ComboBoxClient.ItemsSource = App.Context.Diplom_Client.ToList();
            ComboBoxEmployee.ItemsSource = App.Context.Diplom_Employee.ToList();
            ComboBoxComponent.ItemsSource = App.Context.Diplom_Component.ToList();
            ComboBoxTypeWork.ItemsSource = App.Context.Diplom_TypeWork.ToList();
        }

        private Entites.Diplom_Order order = new Entites.Diplom_Order();
        private Entites.Diplom_OrderTypeWork orderTypeWork = new Entites.Diplom_OrderTypeWork();
        private Entites.Diplom_OrderComponent orderComponent = new Entites.Diplom_OrderComponent();

        private void BtnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxClient.SelectedIndex < 0 || ComboBoxEmployee.SelectedIndex < 0
                || ListViewTypeWork.Items.Count <= 0)
            {
                MessageBox.Show("Необходимо выбрать клиента, сотрудника и хотя бы одну услугу", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                decimal costOrder = 0;
                order.IDClient = (ComboBoxClient.SelectedItem as Entites.Diplom_Client).ID;
                order.IDEmployee = (ComboBoxEmployee.SelectedItem as Entites.Diplom_Employee).ID;
                order.Cost = 1;
                order.DateOrderStart = DateTime.Now.Date;
                order.Status = "Новый";
                App.Context.Diplom_Order.Add(order);
                App.Context.SaveChanges();
                int idOrder = order.ID;
                int i, IDTypeWork, IDComponent, time = 0;
                for (i = 0; i < ListViewTypeWork.Items.Count; i++)
                {
                    orderTypeWork = new Entites.Diplom_OrderTypeWork();
                    orderTypeWork.IDOrder = idOrder;
                    orderTypeWork.IDTypeWork = IDTypeWork = (ListViewTypeWork.Items[i] as Entites.Diplom_TypeWork).ID;
                    App.Context.Diplom_OrderTypeWork.Add(orderTypeWork);
                    var typeWork = App.Context.Diplom_TypeWork.ToList();
                    costOrder += typeWork.Where(x => x.ID == IDTypeWork).FirstOrDefault().Cost;
                    time += typeWork.Where(x => x.ID == IDTypeWork).FirstOrDefault().TimeWork;
                }
                if (ListViewComponent.Items.Count > 0)
                {
                    for (i = 0; i < ListViewComponent.Items.Count; i++)
                    {
                        orderComponent = new Entites.Diplom_OrderComponent();
                        orderComponent.IDOrder = idOrder;
                        orderComponent.IDComponent = IDComponent = (ListViewComponent.Items[i] 
                            as Entites.Diplom_Component).ID;
                        App.Context.Diplom_OrderComponent.Add(orderComponent);
                        var component = App.Context.Diplom_Component.ToList();
                        costOrder += component.Where(x => x.ID == IDComponent).FirstOrDefault().Cost;
                    }
                }
                App.Context.SaveChanges();
                costOrder += App.Context.Diplom_Employee.Where(x => x.ID == order.IDEmployee).FirstOrDefault().CostWork * time;
                order.Cost = costOrder;
                App.Context.SaveChanges();
                MessageBox.Show("Создание заказа прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                MainTextBlockClass.MainTextBlock.Text = $"Подтверждение правильности заказа №{idOrder}";
                MainFrameClass.MainFrame.Navigate(new Pages.CreatedOrderPage(order));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {App.CurrentUser.Login}";
            MainFrameClass.MainFrame.GoBack();
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