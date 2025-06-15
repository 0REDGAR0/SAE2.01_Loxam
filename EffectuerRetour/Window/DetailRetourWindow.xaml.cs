using SAE2._01_Loxam.Classe.Retour;
using System.Windows;

namespace SAE2._01_Loxam.EffectuerRetour.Window
{
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
                {
                    MessageBox.Show("Matériel envoyé en réparation.");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise en réparation.");
                }
            }
        }

        private void butDisponible_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetourAffichage retourSelectionne)
            {
                RetourDAO retourDAO = new RetourDAO();
                bool success = retourDAO.MettreMaterielDisponible(retourSelectionne.NumMateriel);
                if (success)
                {
                    MessageBox.Show("Matériel disponible.");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour.");
                }
            }
        }
    }
}
