using Diplom_RepairPC.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Создать заказ";
            MainFrameClass.MainFrame.Navigate(new AddOrderPage());
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из аккаунта", "Вопрос",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                MainTextBlockClass.MainTextBlock.Text = "Авторизация";
                App.CurrentUser = null;
                MainFrameClass.MainFrame.GoBack();
            }
        }
    }
}