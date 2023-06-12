using Diplom_RepairPC.Classes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Diplom_RepairPC.Windows
{
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
            
            TextBoxSurname.Focus();
        }

        private Entites.Diplom_Client _Client = new Entites.Diplom_Client();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxSurname.Text) || String.IsNullOrWhiteSpace(TextBoxName.Text)
                || String.IsNullOrWhiteSpace(TextBoxSecondName.Text) || String.IsNullOrWhiteSpace(TextBoxPhone.Text)
                || String.IsNullOrWhiteSpace(TextBoxEmail.Text) || String.IsNullOrWhiteSpace(TextBoxCard.Text)
                || String.IsNullOrWhiteSpace(TextBoxAdress.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TextBoxPhone.Text.Length != 12)
            {
                MessageBox.Show("Номер телефона содержит 12 символов", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TextBoxPhone.Text[0] != '+' || TextBoxPhone.Text[1] != '7')
            {
                MessageBox.Show("Номер телефона должен быть формата +7XXXXXXXXXX, где X - цифры", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            for (int i = 2; i < TextBoxPhone.Text.Length; i++)
            {
                if (!int.TryParse(TextBoxPhone.Text[i].ToString(), out _))
                {
                    MessageBox.Show("Номер телефона должен быть формата +7XXXXXXXXXX, где X - цифры", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (!(TextBoxEmail.Text.Contains(".") && TextBoxEmail.Text.Contains("@")))
            {
                MessageBox.Show("Email должен содержать '.' и '@'", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(TextBoxCard.Text == "0" || TextBoxCard.Text == "1"))
            {
                MessageBox.Show("Неправильный формат скидочной карты " +
                    "(Навидите на текстовое поле чтобы увидеть подсказку)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _Client.Surname = TextBoxSurname.Text;
            _Client.Name = TextBoxName.Text;
            _Client.SecondName = TextBoxSecondName.Text;
            _Client.Phone = TextBoxPhone.Text;
            _Client.Email = TextBoxEmail.Text;
            _Client.DiscountCard = TextBoxCard.Text == "0" ? false : true;
            _Client.Adress = TextBoxAdress.Text;
            try
            {
                App.Context.Diplom_Client.Add(_Client);
                App.Context.SaveChanges();
                MessageBox.Show("Добавление прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ComboBoxsClass.ComboBoxClient.ItemsSource = App.Context.Diplom_Client.ToList();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                if (MessageBox.Show("Вы действительно хотите вернуться, введённые данные не будут сохранены",
                    "Вопрос", MessageBoxButton.OKCancel, MessageBoxImage.Warning) ==
                    MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
            
        }
    }
}