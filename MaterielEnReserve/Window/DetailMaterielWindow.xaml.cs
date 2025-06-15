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
        private void butActionExemple_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
