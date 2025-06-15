using Npgsql;
using System;
using System.Windows;

namespace SAE2._01_Loxam
{
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string mdp = txtPassword.Password;

            string host;
            int port;

            if (cmbServeur.SelectedIndex == 0)
            {
                host = "srv-peda-new";
                port = 5433;
            }
            else
            {
                host = "192.168.1.32";
                port = 5432;
            }

            string connectionString = $"Host={host};" +
                                       $"Port={port};" +
                                       $"Username={login};" +
                                       $"Password={mdp};" +
                                       $"Database=sae_loxam;" +
                                       $"Options='-c search_path=loxam'";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    DataAccess.Initialize(connectionString);

                    MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    MainWindow mainWindow = new MainWindow(login);
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void butFlemme_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "";
            if (cmbServeur.SelectedIndex == 0)
            {
                connectionString = $"Host=srv-peda-new;" +
                                       $"Port=5433;" +
                                       $"Username=beduneye;" +
                                       $"Password=WVTvXG;" +
                                       $"Database=sae_loxam;" +
                                       $"Options='-c search_path=loxam'";
            }
            else
            {
                connectionString = $"Host=192.168.1.32;" +
                                       $"Port=5432;" +
                                       $"Username=postgres;" +
                                       $"Password=postgres;" +
                                       $"Database=sae_loxam;" +
                                       $"Options='-c search_path=loxam'";
            }

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    DataAccess.Initialize(connectionString);

                    MainWindow mainWindow = new MainWindow("flemme");
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
