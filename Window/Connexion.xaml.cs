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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string mdp = txtPassword.Text;

            string connectionString = $"Host=srv-peda-new;" +
                                       $"Port=5433;" +
                                       $"Username={login};" +
                                       $"Password={mdp};" +
                                       $"Database=sae_loxam;" +
                                       $"Options='-c search_path=loxam'";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Initialise le DataAccess une fois connecté
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

    }
}
