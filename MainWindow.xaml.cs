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

        private DataAccess dataAccess;

        public MainWindow()
        {
            ChargeData();
            InitializeComponent();

            SPcentral.Children.Clear();
            SPcentral.Children.Add(new Bienvenue());
            UpdateButtonStates();
        }

        public MainWindow(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }


        private void butFicheClient_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new FicheClients.UserControls.UCFicheClients());

            SPcentral.Children.Clear();
            SPcentral.Children.Add(new FicheClients.UserControls.UCFicheClients());
            UpdateButtonStates();
        }
       
        private void butLoxam_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new Bienvenue());
            UpdateButtonStates();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Connexion connexion = new Connexion();
            // connexion.Show();
        }
        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerRetour());

            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerRetour());
            UpdateButtonStates();
        }

        private void butReservation_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerReservation());

            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerReservation());
            UpdateButtonStates();
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


        private void UpdateButtonStates()
        {
            // Aucun contrôle chargé, tout redevient "petit"
            if (SPcentral.Children.Count == 0)
            {
                SetButtonState(butRetour, false);
                SetButtonState(butReservation, false);
                return;
            }

            var currentControl = SPcentral.Children[0];

            if (currentControl is UCEffectuerRetour)
            {
                SetButtonState(butRetour, true);
                SetButtonState(butReservation, false);
            }
            else if (currentControl is UCEffectuerReservation)
            {
                SetButtonState(butRetour, false);
                SetButtonState(butReservation, true);
            }
            else
            {
                // Cas où un autre UserControl est chargé => tout petit
                SetButtonState(butRetour, false);
                SetButtonState(butReservation, false);
            }
        }

        private void SetButtonState(Button button, bool isSelected)
        {
            if (isSelected)
            {
                button.FontWeight = FontWeights.Bold;
                button.FontSize = 36;
            }
            else
            {
                button.FontWeight = FontWeights.Normal;
                button.FontSize = 22;
            }
        }


    }
}