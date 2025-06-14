using System.Windows;
using SAE2._01_Loxam.Classe.Client;

namespace SAE2._01_Loxam.FicheClients.Windows
{
    public partial class WindowDetailClient : Window
    {
        public WindowDetailClient(Client client)
        {
            InitializeComponent();

            client.LoadReservations();
            this.DataContext = client;

            ListReservations.ItemsSource = client.ReservationsClient.DefaultView;
        }
    }
}
