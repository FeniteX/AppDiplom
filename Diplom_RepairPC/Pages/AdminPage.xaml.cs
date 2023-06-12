using System;
using System.Windows;
using System.Windows.Controls;
using Diplom_RepairPC.Classes;

namespace Diplom_RepairPC.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Клиенты";
            MainFrameClass.MainFrame.Navigate(new ClientsPage());
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Заказы";
            MainFrameClass.MainFrame.Navigate(new OrdersPage());
        }

        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Сотрудники";
            MainFrameClass.MainFrame.Navigate(new EmployeesPage());
        }

        private void BtnTypeWork_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Услуги";
            MainFrameClass.MainFrame.Navigate(new TypesWorkPage());
        }

        private void BtnComponent_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Комплектующие";
            MainFrameClass.MainFrame.Navigate(new ComponentsPage());
        }

        private void BtnTypeComponent_Click(object sender, RoutedEventArgs e)
        {
            MainTextBlockClass.MainTextBlock.Text = "Виды комплектующих";
            MainFrameClass.MainFrame.Navigate(new TypesComponentPage());
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