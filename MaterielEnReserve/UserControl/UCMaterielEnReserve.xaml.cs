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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCMaterielEnReserve.xaml
    /// </summary>
    public partial class UCMaterielEnReserve : UserControl
    {
        public UCMaterielEnReserve()
        {
            InitializeComponent();
            DataGridMateriel.Items.Filter = RechercheMotClefMateriel;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(DataGridMateriel.ItemsSource).Refresh();
        }

        private bool RechercheMotClefMateriel(object obj)
        {
            if (obj is ReservationAffichage reservation)
            {
                string texteRecherche = txtRecherche.Text?.ToLower() ?? "";

                return reservation.Client.ToLower().Contains(texteRecherche)
                    || reservation.Materiel.ToLower().Contains(texteRecherche);
            }
            return false;
        }
    }
}
