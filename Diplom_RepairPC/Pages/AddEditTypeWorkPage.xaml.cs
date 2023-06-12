using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class AddEditTypeWorkPage : Page
    {
        public AddEditTypeWorkPage(Entites.Diplom_TypeWork typeWork)
        {
            InitializeComponent();

            if (typeWork != null)
                _TypeWork = typeWork;

            DataContext = _TypeWork;
            TextBoxName.Focus();
        }

        private Entites.Diplom_TypeWork _TypeWork = new Entites.Diplom_TypeWork();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxName.Text) || String.IsNullOrWhiteSpace(TextBoxCost.Text)
                || String.IsNullOrWhiteSpace(TextBoxTimeWork.Text))
            {
                MessageBox.Show("Название, стоимость и время работы должны быть заполнены", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (decimal.TryParse(TextBoxCost.Text, out decimal cost))
            {
                if (cost <= 0)
                {
                    MessageBox.Show("Стоимость должна быть положительная", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Стоимость содержит только цифры", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (int.TryParse(TextBoxTimeWork.Text, out int time))
            {
                if (time <= 0)
                {
                    MessageBox.Show("Время работы должно быть положительным", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Время работы содержит только цифры", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _TypeWork.Name = TextBoxName.Text;
            _TypeWork.Description = TextBoxDescription.Text == null ? "NULL" : TextBoxDescription.Text;
            _TypeWork.Cost = Convert.ToDecimal(TextBoxCost.Text);
            _TypeWork.TimeWork = Convert.ToInt32(TextBoxTimeWork.Text);
            if (_TypeWork.ID == 0)
                App.Context.Diplom_TypeWork.Add(_TypeWork);
            try
            {
                App.Context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                MainTextBlockClass.MainTextBlock.Text = "Услуги";
                MainFrameClass.MainFrame.GoBack();
                DataGridsClass.DataGridTypeWork.ItemsSource = App.Context.Diplom_TypeWork.ToList();
                DataGridsClass.DataGridTypesWork.ItemsSource = App.Context.Diplom_TypeWork.ToList();
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
                MainTextBlockClass.MainTextBlock.Text = "Услуги";
                MainFrameClass.MainFrame.GoBack();
            }
        }
    }
}