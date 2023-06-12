using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Diplom_RepairPC.Pages
{
    public partial class AutorPage : Page
    {
        public AutorPage()
        {
            InitializeComponent();

            TextBoxLogin.Focus(); //курсор ставиться на текстовое поле "Логин"
        }

        private void LoginUser() //функция для проверки корректного ввода логина и пароля
        {
            try //try-catch используется для предотвращения вылетов приложения при возникновении проблем с подключением к базе данных
            {
                var users = App.Context.Diplom_Account.Where(x => x.Login == TextBoxLogin.Text &&
                x.Password == PasswordBoxPassword.Password).FirstOrDefault(); //поиск пользователя с введённым логином и паролем
                if (users == null) //нет такого пользователя
                    MessageBox.Show("Неправильный логин или пароль", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                else if (users.IDRole == 1) //роль у пользователя администратор
                {
                    MessageBox.Show("Успешная авторизация", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {users.Login}";
                    App.CurrentUser = users;
                    MainFrameClass.MainFrame.Navigate(new AdminPage());
                }
                else if (users.IDRole == 2) //роль у пользователя сотрудник
                {
                    MessageBox.Show("Успешная авторизация", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    MainTextBlockClass.MainTextBlock.Text = $"Вы зашли под {users.Login}";
                    App.CurrentUser = users;
                    MainFrameClass.MainFrame.Navigate(new EmployeePage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e) //кнопка "Войти"
        {
            LoginUser();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e) //при нажатии на кнопку на клавиатуре
        {
            if (e.Key == Key.Enter) LoginUser(); //если нажата кнопка Enter
        }
    }
}