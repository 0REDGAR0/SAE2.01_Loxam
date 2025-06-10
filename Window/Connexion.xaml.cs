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

            Connexion connect = new Connexion(login, mdp);

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
}
