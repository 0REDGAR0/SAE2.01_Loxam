using SAE2._01_Loxam.Classe.Reservation;
using SAE2._01_Loxam.Reparation;
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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridReparation.ItemsSource).Refresh();
        }

        private void DataGridResa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();  
        }

        private bool RechercheMotClefReparation(object obj)
        {
            if (obj is ReparationAffichage reparation)
            {
                string texteRecherche = txtRecherche.Text?.ToLower() ?? "";

                return reparation.NomMateriel.ToLower().Contains(texteRecherche)
                    || reparation.NumMateriel.Equals(int.Parse(texteRecherche));
            }
            return false;
        }
    }
}
