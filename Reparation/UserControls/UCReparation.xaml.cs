using SAE2._01_Loxam.Classe.Reparation;
using SAE2._01_Loxam.Classe.Reservation;
using SAE2._01_Loxam.Reparation.WindowRepa;
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

namespace SAE2._01_Loxam.Classe.Materiel.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCReparation.xaml
    /// </summary>
    public partial class UCReparation : UserControl
    {
        public UCReparation()
        {
            InitializeComponent();
            ChargerReparations();
        }

        private void ChargerReparations()
        {
            ReparationDAO reparationDAO = new ReparationDAO();
            DataGridReparation.ItemsSource = reparationDAO.GetReparationAffichage();
            var liste = reparationDAO.GetReparationAffichage();
            DataGridReparation.ItemsSource = liste;
            var view = CollectionViewSource.GetDefaultView(DataGridReparation.ItemsSource);
            if (view != null)
                view.Filter = RechercheMotClefReparation;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridReparation.ItemsSource).Refresh();
        }

        private void DataGridReparation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridReparation.SelectedItem is ReparationAffichage reparation)
            {
                DetailReparationWindow detailWindow = new DetailReparationWindow(reparation);

                detailWindow.Closed += (s, args) =>
                {
                    ChargerReparations();
                    CollectionViewSource.GetDefaultView(DataGridReparation.ItemsSource).Refresh();
                };

                detailWindow.ShowDialog();
            }
        }


        private bool RechercheMotClefReparation(object item)
        {
            if (item is not ReparationAffichage reparation)
                return false;

            string texteRecherche = txtRecherche.Text ?? string.Empty;

            return (reparation.Reference ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                || (reparation.NomMateriel ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                || (reparation.Libelletype ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                || (reparation.Libellecategorie ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase);
        }

    }
}
