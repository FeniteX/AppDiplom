using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class AddEditEmployeePage : Page
    {
        public AddEditEmployeePage(Entites.Diplom_Employee employee)
        {
            InitializeComponent();

            if (employee != null)
            {
                _Employee = employee;
                CheckBoxEdit.Visibility = Visibility.Visible;
                TextBlockCostWork.IsEnabled = false;
                BorderCostWork.IsEnabled = false;
            }
            else
            {
                TextBlockCostWork.IsEnabled = true;
                BorderCostWork.IsEnabled = true;
            }

            DataContext = _Employee;
            TextBoxSurname.Focus();
        }

        private Entites.Diplom_Employee _Employee = new Entites.Diplom_Employee();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrWhiteSpace(TextBoxSurname.Text) || String.IsNullOrWhiteSpace(TextBoxName.Text)
            || String.IsNullOrWhiteSpace(TextBoxSecondName.Text) || String.IsNullOrWhiteSpace(TextBoxPhone.Text)
            || String.IsNullOrWhiteSpace(TextBoxEmail.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (CheckBoxEdit.Visibility == Visibility.Collapsed || CheckBoxEdit.IsChecked == true)
            {
                if (String.IsNullOrWhiteSpace(TextBoxCostWork.Text))
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
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
            if (CheckBoxEdit.Visibility == Visibility.Collapsed || CheckBoxEdit.IsChecked == true)
            {
                if (double.TryParse(TextBoxCostWork.Text, out double costWork))
                {
                    if (costWork <= 0)
                    {
                        MessageBox.Show("Зарплата - положительное число", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Зарплата - это число", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            _Employee.Surname = TextBoxSurname.Text;
            _Employee.Name = TextBoxName.Text;
            _Employee.SecondName = TextBoxSecondName.Text;
            _Employee.Phone = TextBoxPhone.Text;
            _Employee.Email = TextBoxEmail.Text;
            if (CheckBoxEdit.Visibility == Visibility.Collapsed || CheckBoxEdit.IsChecked == true)
            {
                _Employee.CostWork = Convert.ToDecimal(Convert.ToDouble(TextBoxCostWork.Text) / 60);
            }
            if (_Employee.ID == 0)
                App.Context.Diplom_Employee.Add(_Employee);
            try
            {
                App.Context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                MainTextBlockClass.MainTextBlock.Text = "Сотрудники";
                MainFrameClass.MainFrame.GoBack();
                ListViewsClass.ListViewEmployee.ItemsSource = App.Context.Diplom_Employee.ToList();
                DataGridsClass.DataGridEmployees.ItemsSource = App.Context.Diplom_Employee.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите вернуться, введённые данные не будут сохранены",
                "Вопрос", MessageBoxButton.OKCancel, MessageBoxImage.Warning) ==
                MessageBoxResult.OK)
            {
                MainTextBlockClass.MainTextBlock.Text = "Сотрудники";
                MainFrameClass.MainFrame.GoBack();
            }
        }

        private void CheckBoxEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxEdit.IsChecked == true)
            {
                TextBlockCostWork.IsEnabled = true;
                BorderCostWork.IsEnabled = true;
            }
            else if (CheckBoxEdit.IsChecked == false)
            {
                TextBlockCostWork.IsEnabled = false;
                BorderCostWork.IsEnabled = false;
            }
        }
    }
}