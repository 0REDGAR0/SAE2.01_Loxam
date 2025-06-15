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
using SAE2._01_Loxam.Classe.Client;


namespace SAE2._01_Loxam.FicheClients.Windows
{

    public partial class WindowFicheClient : Window
    {
        public enum Action { Modifier, Créer, Supprimer };
        public WindowFicheClient(Client client, Action uneAction)
        {
            InitializeComponent();
            if (uneAction == Action.Modifier)
            {
                butValider.Content = Action.Modifier.ToString();
            }
            else if (uneAction == Action.Créer)
            {
                butValider.Content = Action.Créer.ToString();
            }
            else if (uneAction == Action.Supprimer)
            {
                butValider.Content = Action.Supprimer.ToString();
            }

            this.DataContext = client;
        }

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormClient.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                if (Validation.GetHasError(uie))
                    ok = false;
            }
            DialogResult = true;
        }
    }
}
