using SAE2._01_Loxam.Classe.Reservation;
using System.Windows;

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    public partial class DetailReservationWindow : Window
    {
        public DetailReservationWindow(ReservationAffichage reservation)
        {
            InitializeComponent();
            DataContext = reservation;
        }
    }
}
