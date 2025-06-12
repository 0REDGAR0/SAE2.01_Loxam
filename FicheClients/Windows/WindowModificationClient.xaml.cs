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

namespace SAE2._01_Loxam.FicheClients.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowModificationClient.xaml
    /// </summary>
    public partial class WindowModificationClient : Window
    {
        public enum Action { Modifier, Créer, Supprimer };
        public WindowModificationClient(Client client, Action uneAction)
        {
            InitializeComponent();
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
            foreach (UIElement uie in panelFormClientmodif.Children)
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
