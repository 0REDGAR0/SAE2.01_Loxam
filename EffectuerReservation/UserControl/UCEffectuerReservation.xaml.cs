using SAE2._01_Loxam.Classe;
using SAE2._01_Loxam.Classe.Reservation;
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
            RemplirComboEtat();
            DataGridResa.Items.Filter = RechercheMotClefResa;
        }

        private void ChargerReservations()
        {
            ReservationDAO reservationDAO = new ReservationDAO();
            DataGridResa.ItemsSource = reservationDAO.GetReservationsAffichage();
        }

        private void RemplirComboEtat()
        {
            cmbEtat.Items.Clear();
            cmbEtat.Items.Add("Tous");
            cmbEtat.Items.Add("Disponible");
            cmbEtat.Items.Add("Prévue");
            cmbEtat.Items.Add("En cours");
            cmbEtat.SelectedIndex = 0;
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

                bool filtreTexte = reservation.Client.ToLower().Contains(texteRecherche)
                    || reservation.Materiel.ToLower().Contains(texteRecherche);

                bool filtreEtat = true;
                if (cmbEtat.SelectedItem != null && cmbEtat.SelectedItem.ToString() != "Tous")
                {
                    filtreEtat = reservation.StatutReservation == cmbEtat.SelectedItem.ToString();
                }

                return filtreTexte && filtreEtat;
            }

            return false;
        }



        private void DataGridResa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridResa.SelectedItem is ReservationAffichage reservation)
            {
                DetailReservationWindow detailWindow = new DetailReservationWindow(reservation);

                detailWindow.Closed += (s, args) =>
                {
                    ChargerReservations();
                    CollectionViewSource.GetDefaultView(DataGridResa.ItemsSource).Refresh();
                };

                detailWindow.ShowDialog();
            }
        }

        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridResa.ItemsSource).Refresh();
        }

        private void cmbEtat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridResa.ItemsSource).Refresh();
        }

    }
}
