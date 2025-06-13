using SAE2._01_Loxam.Classe.Materiel;
using SAE2._01_Loxam.Classe.Reservation;
using SAE2._01_Loxam.Classe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SAE2._01_Loxam.Classe.Client;

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    public partial class CréerReservationWindow : Window
    {
        private List<Client> tousLesClients;
        private List<MaterielAffichage> tousLesMateriels;

        private Client clientSelectionne;
        private MaterielAffichage materielSelectionne;

        private ClientDAO clientDAO = new ClientDAO();
        private MaterielDAO materielDAO = new MaterielDAO();
        private ReservationDAO reservationDAO = new ReservationDAO();

        public CréerReservationWindow()
        {
            InitializeComponent();
            ChargerDonnees();

            // On remplit directement les listes complètes à l'ouverture
            listClients.ItemsSource = tousLesClients;
            listMateriels.ItemsSource = tousLesMateriels;

            dpDebut.SelectedDateChanged += CalculerPrixTotal;
            dpRetour.SelectedDateChanged += CalculerPrixTotal;
        }


        private void ChargerDonnees()
        {
            tousLesClients = clientDAO.GetTousLesClients();
            tousLesMateriels = materielDAO.GetMaterielsDisponibles();
        }

        private void txtRechercheClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            string recherche = txtRechercheClient.Text.ToLower();

            if (string.IsNullOrWhiteSpace(recherche))
            {
                listClients.ItemsSource = tousLesClients;
            }
            else
            {
                var filtres = tousLesClients
                    .Where(c => c.NomComplet.ToLower().Contains(recherche))
                    .ToList();
                listClients.ItemsSource = filtres;
            }
        }

        private void listClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientSelectionne = listClients.SelectedItem as Client;
        }

        private void txtRechercheMateriel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string recherche = txtRechercheMateriel.Text.ToLower();

            if (string.IsNullOrWhiteSpace(recherche))
            {
                listMateriels.ItemsSource = tousLesMateriels;
            }
            else
            {
                var filtres = tousLesMateriels
                    .Where(m => m.NomComplet.ToLower().Contains(recherche))
                    .ToList();
                listMateriels.ItemsSource = filtres;
            }
        }


        private void listMateriels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            materielSelectionne = listMateriels.SelectedItem as MaterielAffichage;
            CalculerPrixTotal(null, null);
        }

        private void CalculerPrixTotal(object sender, EventArgs e)
        {
            if (materielSelectionne != null &&
                dpDebut.SelectedDate.HasValue &&
                dpRetour.SelectedDate.HasValue)
            {
                int jours = (dpRetour.SelectedDate.Value - dpDebut.SelectedDate.Value).Days + 1;
                if (jours > 0)
                {
                    decimal prixTotal = jours * materielSelectionne.PrixJournee;
                    txtPrixTotal.Text = $"{prixTotal:0.00} €";
                }
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (clientSelectionne != null && materielSelectionne != null
                && dpDebut.SelectedDate.HasValue && dpRetour.SelectedDate.HasValue)
            {
                int numEmploye = 1; // Par défaut
                DateTime dateReservation = DateTime.Now;
                decimal prixTotal = decimal.Parse(txtPrixTotal.Text.Replace("€", "").Trim());

                reservationDAO.CreerReservation(
                    materielSelectionne.NumMateriel,
                    clientSelectionne.NumClient,
                    numEmploye,
                    dateReservation,
                    dpDebut.SelectedDate.Value,
                    dpRetour.SelectedDate.Value,
                    prixTotal
                );

                MessageBox.Show("Réservation créée avec succès !");
                this.Close();
            }
            else
            {
                MessageBox.Show("Merci de remplir tous les champs.");
            }
        }
    }
}
