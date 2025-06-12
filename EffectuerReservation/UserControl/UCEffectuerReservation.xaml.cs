using SAE2._01_Loxam.Classe.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
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
using SAE2._01_Loxam.Classe.Reservation;


namespace SAE2._01_Loxam.FicheClients.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCEffectuerReservation.xaml
    /// </summary>
    public partial class UCEffectuerReservation : UserControl
    {
        public UCEffectuerReservation()
        {
            InitializeComponent();
            ChargerReservations();
            DataGridResa.Items.Filter = RechercheMotClefResa;
        }

        private void ChargerReservations()
        {
            ReservationDAO reservationDAO = new ReservationDAO();
            DataGridResa.ItemsSource = reservationDAO.GetReservationsAffichage();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridResa.ItemsSource).Refresh();
        }

        private bool RechercheMotClefResa(object obj)
        {
            if (obj is ReservationAffichage reservation)
            {
                string texteRecherche = txtRecherche.Text?.ToLower() ?? "";

                return reservation.Client.ToLower().Contains(texteRecherche)
                    || reservation.Materiel.ToLower().Contains(texteRecherche);
            }
            return false;
        }

        private void DataGridResa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridResa.SelectedItem is ReservationAffichage resaSelectionnee)
            {
                DetailReservationWindow detailWindow = new DetailReservationWindow(resaSelectionnee);
                detailWindow.ShowDialog();
            }
        }
    }
}
