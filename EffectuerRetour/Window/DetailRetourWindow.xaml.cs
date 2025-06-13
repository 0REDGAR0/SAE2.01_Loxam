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
using System.Windows.Shapes;
using SAE2._01_Loxam.Classe.Retour;


namespace SAE2._01_Loxam.EffectuerRetour.Window
{
    /// <summary>
    /// Interaction logic for DetailRetourWindow.xaml
    /// </summary>
    public partial class DetailRetourWindow : System.Windows.Window
    {
        public DetailRetourWindow()
        {
            InitializeComponent();
        }

        private void butEnvoyerReparation_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetourAffichage retourSelectionne)
            {
                RetourDAO retourDAO = new RetourDAO();
                bool success = retourDAO.MettreMaterielEnReparation(retourSelectionne.NumMateriel);
                if (success)
                    MessageBox.Show("Matériel envoyé en réparation.");
                else
                    MessageBox.Show("Erreur lors de la mise en réparation.");
            }
        }

        private void butDisponible_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetourAffichage retourSelectionne)
            {
                RetourDAO retourDAO = new RetourDAO();
                bool success = retourDAO.MettreMaterielDisponible(retourSelectionne.NumMateriel);
                if (success)
                    MessageBox.Show("Matériel disponible.");
                else
                    MessageBox.Show("Erreur lors de la mise à jour.");
            }
        }
    }
}
