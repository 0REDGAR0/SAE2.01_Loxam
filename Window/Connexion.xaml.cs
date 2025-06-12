using Npgsql;
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
using System.Windows.Shapes;

namespace SAE2._01_Loxam
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        private string login;
        private string mdp;

        public Connexion()
        {
            InitializeComponent();
        }

        public Connexion(string login, string mdp)
        {
            this.Login = login;
            this.Mdp = mdp;
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
            }
        }

        public string Mdp
        {
            get
            {
                return this.mdp;
            }

            set
            {
                this.mdp = value;
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string mdp = txtPassword.Text;

            string connexionString = $"Host=srv-peda-new;" +
                $"Port=5433;" +
                $"Username={login};" +
                $"Password={mdp};" +
                $"Database=sae_loxam;" +
                $"Options='-c search_path=loxam'";

            try
            {
                using (NpgsqlConnection connexion = new NpgsqlConnection(connexionString))
                {
                    connexion.Open(); // si échec → catch

                    // Connexion réussie → ouvrir fenêtre principale
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception)
            {
                // Échec de la connexion PostgreSQL
                MessageBox.Show("Identifiant ou mot de passe incorrect", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
