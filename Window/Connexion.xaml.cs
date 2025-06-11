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
            $"Options='-c " +
            $"search_path=loxam'";

            using (NpgsqlConnection connexion = new NpgsqlConnection(connexionString))
            {
                try
                {
                    connexion.Open();

                    MainWindow mainWindow = new MainWindow();
                    this.Hide();
                    mainWindow.Show();
                }catch (Exception ex)
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect","Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

    }

            /*if ((txtLogin.Text != DataAccess.login) && (txtPassword.Password != DataAccess.password))
            {
                MessageBox.Show("CA MARCHE PAS");
            }else
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close(); 
            }*/
        
    }
}
