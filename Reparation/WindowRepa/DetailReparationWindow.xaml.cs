using SAE2._01_Loxam.Classe.Materiel;
using SAE2._01_Loxam.Classe.Reparation;
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
using System.Windows.Shapes;

namespace SAE2._01_Loxam.Reparation.WindowRepa
{
    /// <summary>
    /// Logique d'interaction pour DetailReparationWindow.xaml
    /// </summary>
    public partial class DetailReparationWindow : Window
    {
        private ReparationAffichage materiel;
        public DetailReparationWindow(ReparationAffichage m)
        {
            InitializeComponent();
            materiel = m;
            this.DataContext = materiel;
            cbEtat.ItemsSource = new List<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(5, "En attente de réparation"),
                    new KeyValuePair<int, string>(6, "En réparation"),
                    new KeyValuePair<int, string>(7, "HS"),
                    new KeyValuePair<int, string>(1, "Disponible")
                };

            cbEtat.SelectedValue = materiel.NumEtat; // ou reservation.NumEtat selon ton objet


        }

        /*private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/

        private void butEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (materiel == null)
                return;

            int nouvelEtat = materiel.NumEtat; // Valeur par défaut

            // Vérifie que quelque chose est sélectionné dans le ComboBox
            if (cbEtat.SelectedValue != null && int.TryParse(cbEtat.SelectedValue.ToString(), out int etatCombo))
            {
                nouvelEtat = etatCombo;
            }

            string nouveauCommentaire = txtCommentaire.Text?.Trim() ?? "";

            bool etatModifie = materiel.NumEtat != nouvelEtat;
            bool commentaireModifie = (materiel.Commentaire ?? "") != nouveauCommentaire;

            if (!etatModifie && !commentaireModifie)
            {
                this.Close(); // Rien à changer
                return;
            }

            try
            {
                // Met à jour l’objet local
                materiel.NumEtat = nouvelEtat;
                materiel.Commentaire = nouveauCommentaire;

                // Mise à jour en base de données
                new ReparationDAO().MettreAJourEtatEtCommentaireMateriel(
                    materiel.NumMateriel,
                    nouvelEtat,
                    nouveauCommentaire
                );

                MessageBox.Show("Modifications enregistrées avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }


        private void butFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
