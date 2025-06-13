using SAE2._01_Loxam.Classe;
using SAE2._01_Loxam.Classe.Reservation;
using SAE2._01_Loxam.EffectuerRetour.Window;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SAE2._01_Loxam.Classe.Retour;


namespace SAE2._01_Loxam.FicheClients.UserControls
{
    public partial class UCEffectuerRetour : UserControl
    {
        private List<RetourAffichage> listeReservationsRetour;
        private RetourDAO retourDAO = new RetourDAO();

        public UCEffectuerRetour()
        {
            InitializeComponent();
            ChargerReservationsRetour();
            RemplirComboCategorie();
            DataGridRetour.Items.Filter = RechercheMotClefRetour;
        }

        private void ChargerReservationsRetour()
        {
            listeReservationsRetour = retourDAO.GetRetourAffichage();
            DataGridRetour.ItemsSource = listeReservationsRetour;
        }

        private void RemplirComboCategorie()
        {
            cmbCategorie.Items.Clear();
            cmbCategorie.Items.Add("Toutes");

            CategorieDAO categorieDAO = new CategorieDAO();
            List<string> categories = categorieDAO.GetToutesCategories();

            foreach (var cat in categories)
            {
                cmbCategorie.Items.Add(cat);
            }

            cmbCategorie.SelectedIndex = 0;
        }

        private bool RechercheMotClefRetour(object obj)
        {
            if (obj is RetourAffichage retour)
            {
                string texteRecherche = txtRecherche.Text?.ToLower() ?? "";

                bool filtreTexte = retour.Client.ToLower().Contains(texteRecherche)
                        || retour.Materiel.ToLower().Contains(texteRecherche);

                bool filtreCategorie = true;
                if (cmbCategorie.SelectedItem != null && cmbCategorie.SelectedItem.ToString() != "Toutes")
                {
                    filtreCategorie = retour.Categorie == cmbCategorie.SelectedItem.ToString();
                }

                return filtreTexte && filtreCategorie;
            }
            return false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridRetour.ItemsSource).Refresh();
        }

        private void DataGridRetour_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataGridRetour.SelectedItem is RetourAffichage retour)
            {
                DetailRetourWindow detailWindow = new DetailRetourWindow();
                detailWindow.DataContext = retour;

                // Ici on abonne un handler à l'événement Closed de la fenêtre
                detailWindow.Closed += (s, args) =>
                {
                    // On recharge la DataGrid dès que la fenêtre de détail est fermée
                    ChargerReservationsRetour();
                    CollectionViewSource.GetDefaultView(DataGridRetour.ItemsSource).Refresh();
                };

                detailWindow.ShowDialog();
            }
        }




        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridRetour.ItemsSource).Refresh();
        }

    }
}
