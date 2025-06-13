using SAE2._01_Loxam.Classe.Materiel;
using System.Windows;

namespace SAE2._01_Loxam.MaterielEnReserve
{
    public partial class DetailMaterielWindow : Window
    {
        public DetailMaterielWindow(MaterielAffichage materiel)
        {
            InitializeComponent();
            DataContext = materiel;
        }

        // Si un jour tu ajoutes une action modifiante ici :
        private void butActionExemple_Click(object sender, RoutedEventArgs e)
        {
            // Après action réussie :
            this.DialogResult = true;
            this.Close();
        }
    }
}
