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
            if (dgClients.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un client", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Client clientSelectionne = (Client)dgClients.SelectedItem;
                Client copie = new Client(clientSelectionne.NumClient, clientSelectionne.NomClient,
                clientSelectionne.PrenomClient, clientSelectionne.AdresseClient, clientSelectionne.MailClient, clientSelectionne.NumeroTelClient);
                WindowFicheClient wClient = new WindowFicheClient(copie, WindowFicheClient.Action.Modifier);
                bool? result = wClient.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        copie.Update();
                        clientSelectionne.NumClient = copie.NumClient;
                        clientSelectionne.NomClient = copie.NomClient;
                        clientSelectionne.PrenomClient = copie.PrenomClient;
                        clientSelectionne.AdresseClient = copie.AdresseClient;
                        clientSelectionne.MailClient = copie.MailClient;
                        clientSelectionne.NumeroTelClient = copie.NumeroTelClient;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le chien n'a pas pu être modifié.", "Attention",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //CollectionViewSource.GetDefaultView(dgChiens.ItemsSource)?.Refresh();
            }
        }


        private void textMotClefChien_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void butSupp_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un chien", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Client clientSellectionner = (Client)dgClients.SelectedItem;
                Client clientASupprimer = new Client(clientSellectionner.NumClient, clientSellectionner.NomClient,
                clientSellectionner.PrenomClient, clientSellectionner.AdresseClient, clientSellectionner.MailClient, clientSellectionner.NumeroTelClient);
                /*if (chienAsupprimer.FindNbSejours() > 0)
                {
                    MessageBox.Show(this, $"Attention, ce chien est lié à {chienAsupprimer.FindNbSejours()} séjours. Désirez-vous tout de même supprimer ce chien et ces {chienAsupprimer.FindNbSejours()} séjours", "Attention",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }*/
                WindowFicheClient wClient = new WindowFicheClient(clientASupprimer, WindowFicheClient.Action.Supprimer);
                bool? result = wClient.ShowDialog();
                try
                {
                    clientASupprimer.Delete();
                    LeClient.LesClients.Remove(clientASupprimer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le chien n'a pas pu être supprimé.", "Attention",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
