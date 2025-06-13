using SAE2._01_Loxam.Classe;
using SAE2._01_Loxam.Classe.Materiel;
using SAE2._01_Loxam.MaterielEnReserve;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    public partial class UCMaterielEnReserve : UserControl
    {
        private List<MaterielAffichage> listeMateriels;
        private MaterielDAO materielDAO = new MaterielDAO();

        public UCMaterielEnReserve()
        {
            InitializeComponent();
            ChargerMateriels();
            RemplirComboCategorie();
            RemplirComboEtat();
            DataGridMateriel.Items.Filter = RechercheMotClefMateriel;
        }

        private void ChargerMateriels()
        {
            listeMateriels = materielDAO.GetMaterielsAffichage();
            DataGridMateriel.ItemsSource = listeMateriels;
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

        private void RemplirComboEtat()
        {
            cmbEtat.Items.Clear();
            cmbEtat.Items.Add("Tous");
            cmbEtat.Items.Add("Disponible");
            cmbEtat.Items.Add("Prévue");
            cmbEtat.Items.Add("En cours");
            cmbEtat.Items.Add("Terminée");
            cmbEtat.Items.Add("En attente de réparation");
            cmbEtat.Items.Add("En réparation");
            cmbEtat.Items.Add("HS");
            cmbEtat.SelectedIndex = 0;
        }

        private bool RechercheMotClefMateriel(object obj)
        {
            if (obj is MaterielAffichage materiel)
            {
                string texteRecherche = txtRecherche.Text?.ToLower() ?? "";

                bool filtreTexte = materiel.NomMateriel.ToLower().Contains(texteRecherche)
                    || materiel.Reference.ToLower().Contains(texteRecherche);

                bool filtreCategorie = true;
                if (cmbCategorie.SelectedItem != null && cmbCategorie.SelectedItem.ToString() != "Toutes")
                {
                    filtreCategorie = materiel.Categorie == cmbCategorie.SelectedItem.ToString();
                }

                bool filtreEtat = true;
                if (cmbEtat.SelectedItem != null && cmbEtat.SelectedItem.ToString() != "Tous")
                {
                    filtreEtat = materiel.StatutReservation == cmbEtat.SelectedItem.ToString();
                }

                return filtreTexte && filtreCategorie && filtreEtat;
            }

            return false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridMateriel.ItemsSource).Refresh();
        }

        private void DataGridMateriel_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataGridMateriel.SelectedItem is MaterielAffichage materielSelectionne)
            {
                DetailMaterielWindow detailWindow = new DetailMaterielWindow(materielSelectionne);
                detailWindow.ShowDialog();
            }
        }

        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridMateriel.ItemsSource).Refresh();
        }

        private void cmbEtat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridMateriel.ItemsSource).Refresh();
        }
    }
}
