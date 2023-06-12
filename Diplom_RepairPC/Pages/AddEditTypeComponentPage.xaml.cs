using Diplom_RepairPC.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Diplom_RepairPC.Pages
{
    public partial class AddEditTypeComponentPage : Page
    {
        public AddEditTypeComponentPage(Entites.Diplom_TypeComponent typeComponent)
        {
            InitializeComponent();

            if (typeComponent != null)
                _TypeComponent = typeComponent;

            DataContext = _TypeComponent;
            TextBoxName.Focus();
        }

        private Entites.Diplom_TypeComponent _TypeComponent = new Entites.Diplom_TypeComponent();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxName.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _TypeComponent.Name = TextBoxName.Text;
            if (_TypeComponent.ID == 0)
                App.Context.Diplom_TypeComponent.Add(_TypeComponent);
            try
            {
                App.Context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                MainTextBlockClass.MainTextBlock.Text = "Виды комплектующих";
                MainFrameClass.MainFrame.GoBack();
                DataGridsClass.DataGridTypeComponent.ItemsSource = App.Context.Diplom_TypeComponent.ToList();
                DataGridsClass.DataGridTypesComponent.ItemsSource = App.Context.Diplom_TypeComponent.ToList();
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
                MainTextBlockClass.MainTextBlock.Text = "Виды комплектующих";
                MainFrameClass.MainFrame.GoBack();
            }
        }
    }
}