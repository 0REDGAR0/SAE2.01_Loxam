using SAE2._01_Loxam.Classe.Materiel;
using SAE2._01_Loxam.Classe.Reservation;
using SAE2._01_Loxam.MaterielEnReserve;
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

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCMaterielEnReserve.xaml
    /// </summary>
    public partial class UCMaterielEnReserve : UserControl
    {
        private List<MaterielAffichage> listeMateriels;
        private MaterielDAO materielDAO = new MaterielDAO();

        public UCMaterielEnReserve()
        {
            InitializeComponent();
            ChargerMateriels();
        }

        private void ChargerMateriels()
        {
            listeMateriels = materielDAO.GetMaterielsAffichage();
            DataGridMateriel.ItemsSource = listeMateriels;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridMateriel.ItemsSource).Refresh();
        }

        private bool RechercheMotClefMateriel(object obj)
        {
            if (obj is Materiel materiel)
            {
                string texteRecherche = txtRecherche.Text?.ToLower() ?? "";
                return materiel.NomMateriel.ToLower().Contains(texteRecherche)
                    || materiel.Descriptif.ToLower().Contains(texteRecherche);
            }
            return false;
        }

        private void DataGridMateriel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridMateriel.SelectedItem is MaterielAffichage materiel)
            {
                DetailMaterielWindow detailWindow = new DetailMaterielWindow(materiel);
                detailWindow.ShowDialog();
            }
        }
    }

}
