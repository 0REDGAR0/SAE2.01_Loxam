using SAE2._01_Loxam.Classe;
using SAE2._01_Loxam.FicheClients.UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE2._01_Loxam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataGridLists GestionResa { get; set; }
        
        public MainWindow()
        {
            ChargeData();
            InitializeComponent();
        }

        

        private void butFicheClient_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new FicheClients.UserControls.UCFicheClients());
        }
       
        private void butLoxam_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new Bienvenue());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Connexion connexion = new Connexion();
            connexion.Show();
        }
        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerRetour());

            butRetour.FontWeight = FontWeights.Bold;
            butRetour.FontSize = 36;
            butReservation.FontWeight = FontWeights.Normal;
            butReservation.FontSize = 22;
        }

        private void butReservation_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerReservation());

            butReservation.FontWeight = FontWeights.Bold;
            butReservation.FontSize = 36;
            butRetour.FontWeight = FontWeights.Normal;
            butRetour.FontSize = 22;
        }

        public void ChargeData()
        {
            try
            {
                GestionResa = new DataGridLists();
                this.DataContext = GestionResa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                Application.Current.Shutdown();
            }
        }

        private void SPcentral_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

    }
}