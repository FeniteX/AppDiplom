using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class CreatedOrderPage : Page
    {
        public CreatedOrderPage(Entites.Diplom_Order order)
        {
            InitializeComponent();

            MainWindowClass.MainWindow.MinHeight = 1000;
            MainWindowClass.MainWindow.MinWidth = 1600;
            Order = order;
            ListViewOrder.ItemsSource = App.Context.Diplom_Order.Where(x => x.ID == order.ID).ToList();
            ListViewOrderTypeWork.ItemsSource = App.Context.Diplom_OrderTypeWork.Where(x => x.IDOrder == order.ID).ToList();
            ListViewOrderComponent.ItemsSource = App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == order.ID).ToList();
            if (App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == order.ID).ToList().Count() <= 0)
                TextBlockComponent.Visibility = Visibility.Collapsed;
        }

        private Entites.Diplom_Order Order = new Entites.Diplom_Order();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Заказ составлен правильно?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Warning) 
                == MessageBoxResult.No)
            {
                MainWindowClass.MainWindow.MinHeight = 800;
                MainWindowClass.MainWindow.MinWidth = 1300;
                MainWindowClass.MainWindow.Height = 800;
                MainWindowClass.MainWindow.Width = 1300;
                MainFrameClass.MainFrame.Navigate(new Pages.EditOrderEmployeePage(Order));
                MainTextBlockClass.MainTextBlock.Text = $"Изменение заказа №{Order.ID}";
            }
            else
            {
                MainWindowClass.MainWindow.MinHeight = 800;
                MainWindowClass.MainWindow.MinWidth = 1300;
                MainWindowClass.MainWindow.Height = 800;
                MainWindowClass.MainWindow.Width = 1300;
                MainTextBlockClass.MainTextBlock.Text = $"Вы вошли под {App.CurrentUser.Login}";
                MainFrameClass.MainFrame.GoBack();
                MainFrameClass.MainFrame.GoBack();
            }
        }
    }
}