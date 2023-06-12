using Diplom_RepairPC.Classes;
using System;
using System.Windows;
using System.Windows.Input;

namespace Diplom_RepairPC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrameClass.MainFrame = MainFrame;
            MainFrameClass.MainFrame.Navigate(new Pages.AutorPage());
            MainTextBlockClass.MainTextBlock = MainTextBlock;
            MainTextBlockClass.MainTextBlock.Text = "Авторизация";
            MainWindowClass.MainWindow = this;
        }

        private void CloseApp()
        {
            this.Close();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            CloseApp();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) CloseApp();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Вопрос",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
                e.Cancel = true;
            else App.Current.Shutdown();
        }
    }
}