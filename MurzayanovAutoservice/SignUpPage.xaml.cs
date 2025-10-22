using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MurzayanovAutoservice
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private Service _currentService = new Service();

        private ClientService _currentClientService = new ClientService();

        public SignUpPage(Service SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
            {
                this._currentService = SelectedService;
            }

            DataContext = _currentService;

            var _currentClient = MurzayanovEntities.GetContext().Client.ToList();

            ComboClient.ItemsSource = _currentClient;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ComboClient.SelectedItem == null)
            {
                errors.AppendLine("Укажите ФИО клиента.");
            }
            if (StartDate.Text == "")
            {
                errors.AppendLine("Укажите дату услуги.");
            }
            if (TBStart.Text == "")
            {
                errors.AppendLine("Укажите время начала услуги.");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentClientService.ClientID = ComboClient.SelectedIndex+1;
            _currentClientService.ServiceID = _currentService.ID;
            _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);

            if (_currentClientService.ID == 0)
            {
                MurzayanovEntities.GetContext().ClientService.Add(_currentClientService);
            }

            try
            {
                MurzayanovEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена.");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
