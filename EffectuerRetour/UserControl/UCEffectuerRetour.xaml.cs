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
using SAE2._01_Loxam;

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCEffectuerRetour.xaml
    /// </summary>
    public partial class UCEffectuerRetour : UserControl
    {

        public UCEffectuerRetour()
        {
            InitializeComponent();
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int idReservation = (int)btn.Tag;

            // On pourrait aller chercher le materiel associé ici
            // Exemple : ouvrir une fenêtre de détail avec les infos de ce matériel
            //DetailMaterielWindow detailWindow = new DetailMaterielWindow();
            //detailWindow.ShowDialog();
        }

        private void btn_effectuer_retour_Click(object sender, RoutedEventArgs e)
        {
            FaireRetour retourWindow = new FaireRetour();
            retourWindow.ShowDialog();
        }
    }
}
