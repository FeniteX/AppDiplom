using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class AddEditComponentPage : Page
    {
        public AddEditComponentPage(Entites.Diplom_Component component)
        {
            InitializeComponent();

            if (component != null)
            {
                _Component = component;
                ComboBoxTypeComponent.SelectedItem = 
                    App.Context.Diplom_TypeComponent.Where(x => x.ID == _Component.IDTypeComponent).First();
            }

            DataContext = _Component;
            ComboBoxTypeComponent.ItemsSource = App.Context.Diplom_TypeComponent.ToList();
            TextBoxName.Focus();
        }

        private Entites.Diplom_Component _Component = new Entites.Diplom_Component();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxName.Text) || String.IsNullOrWhiteSpace(TextBoxCharacteristics.Text)
                || String.IsNullOrWhiteSpace(TextBoxCost.Text) || ComboBoxTypeComponent.SelectedIndex < 0
                || (ComboBoxTypeComponent.SelectedItem as Entites.Diplom_TypeComponent).ID == -1)
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка",
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
            
            _Component.Name = TextBoxName.Text;
            _Component.Characteristics = TextBoxCharacteristics.Text;
            _Component.Cost = Convert.ToDecimal(TextBoxCost.Text);
            _Component.IDTypeComponent = (ComboBoxTypeComponent.SelectedItem as Entites.Diplom_TypeComponent).ID;
            if (_Component.ID == 0)
                App.Context.Diplom_Component.Add(_Component);
            try
            {
                App.Context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                MainTextBlockClass.MainTextBlock.Text = "Комплектующие";
                MainFrameClass.MainFrame.GoBack();
                DataGridsClass.DataGridComponent.ItemsSource = App.Context.Diplom_Component.ToList();
                DataGridsClass.DataGridComponents.ItemsSource = App.Context.Diplom_Component.ToList();
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
                MainTextBlockClass.MainTextBlock.Text = "Комплектующие";
                MainFrameClass.MainFrame.GoBack();
            }
        }
    }
}