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
        public Connexion()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string mdp = txtPassword.Password;

            Employe emp = Employe.VerifierConnexion(login, mdp);

            if (emp != null && emp.Numrole == "1")
            {
                // Créer et afficher la MainWindow
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Fermer la fenêtre de connexion
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects ou rôle non autorisé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
