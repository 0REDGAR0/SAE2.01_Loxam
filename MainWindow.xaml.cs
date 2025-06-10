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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        public DataGridLists GestionResa { get; set; }
        
=======
        private DataAccess dataAccess;

>>>>>>> Stashed changes
=======
        private DataAccess dataAccess;

>>>>>>> Stashed changes
        public MainWindow()
        {
            ChargeData();
            InitializeComponent();
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        
=======
=======
>>>>>>> Stashed changes
        public MainWindow(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

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

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerRetour());
        }

        private void butReservation_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerReservation());
        }
    }
}