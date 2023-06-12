using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Diplom_RepairPC.Windows
{
    public partial class OrderDetalied : Window
    {
        public OrderDetalied(Entites.Diplom_Order order)
        {
            InitializeComponent();

            this.Title =  $"Состав заказа №{order.ID}";
            ListViewOrderTypeWork.ItemsSource = App.Context.Diplom_OrderTypeWork.Where(x => x.IDOrder == order.ID).ToList();
            ListViewOrderComponent.ItemsSource = App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == order.ID).ToList();
            if (App.Context.Diplom_OrderComponent.Where(x => x.IDOrder == order.ID).ToList().Count() <= 0)
                TextBlockComponent.Visibility = Visibility.Collapsed;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                if (MessageBox.Show("Вы действительно хотите вернуться?",
                "Вопрос", MessageBoxButton.OKCancel, MessageBoxImage.Warning) ==
                MessageBoxResult.Cancel)
                    e.Cancel = true;
            }

        }
    }
}