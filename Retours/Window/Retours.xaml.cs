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

namespace SAE2._01_Loxam
{
    /// <summary>
    /// Interaction logic for Retours.xaml
    /// </summary>
    public partial class Retours : Window
    {
        public Retours()
        {
            InitializeComponent();
        }

        /*private void DataGridIndisponibilites_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            RetourAffichage retour = (RetourAffichage)btn.DataContext;

            DetailMaterielWindow detailWindow = new DetailMaterielWindow(retour);
            detailWindow.ShowDialog();
        }*/

        private void btn_effectuer_retour_Click(object sender, RoutedEventArgs e)
        {
            //// Vérifie bien qu’une ligne est sélectionnée
            //if (DataGridRetour.SelectedItem is RetourAffichage retourSelectionne)
            //{
            //    // On ouvre la fenêtre de détail en lui passant l'objet complet
            //    DetailRetourWindow detailRetour = new DetailRetourWindow(retourSelectionne);
            //    detailRetour.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Veuillez sélectionner une réservation à retourner.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void DataGridIndisponibilites_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridIndisponibilites.SelectedItem != null)
            {
                var item = DataGridIndisponibilites.SelectedItem;

                // Exemple : ouvrir une fenêtre de détails ou afficher un message
                MessageBox.Show($"Double-clic sur : {item}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
