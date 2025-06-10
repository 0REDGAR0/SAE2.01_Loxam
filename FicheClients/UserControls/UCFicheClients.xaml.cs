using SAE2._01_Loxam.FicheClients.Windows;
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
    /// Logique d'interaction pour UCFicheClients.xaml
    /// </summary>
    public partial class UCFicheClients : UserControl
    {
        public Client LeClient { get; set; }
        public UCFicheClients()
        {
            InitializeComponent();
            ChargeData();
        }

        public void ChargeData()
        {
            try
            {
                LeClient = new Client("Client n1");
                this.DataContext = LeClient;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données,veuillez consulter votre admin");

                Application.Current.Shutdown();
            }
        }

        private void butCréerFicheClient_Click(object sender, RoutedEventArgs e)
        {
            Client unClient = new Client();
            WindowFicheClient wClient = new WindowFicheClient(unClient, WindowFicheClient.Action.Créer);
            bool? result = wClient.ShowDialog();
            if (result == true)
            {
                try
                {
                    unClient.NumClient = unClient.Create();
                    LeClient.LesClients.Add(unClient);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le client n'a pas pu être créé.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void butEdit_Click(object sender, RoutedEventArgs e)
        {

        }


        private void textMotClefChien_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void butSupp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
