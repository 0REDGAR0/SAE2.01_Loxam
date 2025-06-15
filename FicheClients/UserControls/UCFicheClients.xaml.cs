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
using SAE2._01_Loxam.Classe.Client;
using SAE2._01_Loxam.Reparation;


namespace SAE2._01_Loxam.FicheClients.UserControls
{
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
                ClientDAO dao = new ClientDAO();
                List<Client> listeClients = dao.GetTousLesClients();

                dgClients.ItemsSource = listeClients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de la récupération des données : " + ex.Message);
                Application.Current.Shutdown();
            }
        }


        private void butCréerFicheClient_Click(object sender, RoutedEventArgs e)
        {
            WindowFicheClient wClient = new WindowFicheClient(new Client(), WindowFicheClient.Action.Créer);
            bool? result = wClient.ShowDialog();
            if (result == true)
            {
                try
                {
                    Client unClient = (Client)wClient.DataContext;
                    unClient.NumClient = unClient.Create();
                    ChargeData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Le client n'a pas pu être créé.\n\nDétail : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
                WindowModificationClient wClientModif = new WindowModificationClient(copie, WindowModificationClient.Action.Modifier);
                bool? result = wClientModif.ShowDialog();
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
                        MessageBox.Show("Le client n'a pas pu être modifié.", "Attention",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



        private void butSupp_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
                MessageBox.Show("Veuillez sélectionner un client", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                Client clientSellectionner = (Client)dgClients.SelectedItem;
                Client clientASupprimer = new Client(clientSellectionner.NumClient, clientSellectionner.NomClient,
                clientSellectionner.PrenomClient, clientSellectionner.AdresseClient, clientSellectionner.MailClient, clientSellectionner.NumeroTelClient);
                int nbCertif = clientASupprimer.FindNbDispose();

                MessageBoxResult result = MessageBox.Show(
                    nbCertif > 0
                        ? $"Ce client est lié à {nbCertif} reservation(s).\nVoulez-vous tout de même le supprimer ?"
                        : "Êtes-vous sûr de vouloir supprimer ce client ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        clientASupprimer.Delete();
                        LeClient.LesClients.Remove(clientASupprimer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le client n'a pas pu être supprimé.", "Attention",
                       MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void txtRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texteRecherche = txtRecherche.Text?.Trim() ?? "";

            var view = CollectionViewSource.GetDefaultView(dgClients.ItemsSource);
            if (view == null) return;

            view.Filter = item =>
            {
                if (item is not Client client)
                    return false;

                return (client.NomClient ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                    || (client.PrenomClient ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                    || (client.MailClient ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                    || (client.AdresseClient ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)
                    || (client.NumeroTelClient ?? "").Contains(texteRecherche, StringComparison.OrdinalIgnoreCase);
            };

            view.Refresh();
        }

        private void DataGridClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgClients.SelectedItem is Client selectedClient)
            {
                selectedClient.LoadReservations();

                WindowDetailClient detailWindow = new WindowDetailClient(selectedClient);
                detailWindow.ShowDialog();
            }
        }
    }
}
