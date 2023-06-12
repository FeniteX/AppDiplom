using System.ComponentModel;
using System.Windows;
using System.Linq;

namespace Diplom_RepairPC.Windows
{
    public partial class CharacteristicsComponentWindow : Window
    {
        public CharacteristicsComponentWindow(Entites.Diplom_Component component)
        {
            InitializeComponent();

            ListViewComponent.ItemsSource = App.Context.Diplom_Component.Where(x => x.ID == component.ID).ToList();
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