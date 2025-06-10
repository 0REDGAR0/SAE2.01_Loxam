using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void butFicheClient_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new FicheClients.UserControls.UCFicheClients());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new Bienvenue());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Connexion connexion = new Connexion();
            connexion.Show();
        }
    }
}