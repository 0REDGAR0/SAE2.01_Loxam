﻿using SAE2._01_Loxam.Classe;
using SAE2._01_Loxam.Classe.Reservation;
using SAE2._01_Loxam.FicheClients.UserControls;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAE2._01_Loxam.Classe.Reservation;

namespace SAE2._01_Loxam
{
    public partial class MainWindow : Window
    {
        public DataGridLists GestionResa { get; set; }

        private DataAccess dataAccess;

        private string login;

        public MainWindow(string login)
        {
            InitializeComponent();
            this.login = login;

            labConnect.Text = login;

            SPcentral.Children.Clear();
            SPcentral.Children.Add(new Bienvenue());
            UpdateButtonStates();
        }


        public void LoadData()
        {
            try
            {
                GestionResa = new DataGridLists();
                this.DataContext = GestionResa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin\n" + ex);
                Application.Current.Shutdown();
            }
        }

        private void butFicheClient_Click(object sender, RoutedEventArgs e)
        {
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

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerRetour());
            UpdateButtonStates();
        }

        private void butReservation_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new UCEffectuerReservation());
            UpdateButtonStates();
        }

        public void ChargeData()
        {
            try
            {
                GestionResa = new DataGridLists();
                GestionResa.LesReservations = new ObservableCollection<Reservation>(new Reservation().FindAllResa());
                this.DataContext = GestionResa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin\n" + ex);
                Application.Current.Shutdown();
            }
        }


        private void UpdateButtonStates()
        {
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
                SetButtonState(butRetour, false);
                SetButtonState(butReservation, false);
            }
        }

        private void SetButtonState(Button button, bool isSelected)
        {
            double targetSize = isSelected ? 36 : 22;
            AnimateFontSize(button, targetSize);

            button.FontWeight = isSelected ? FontWeights.Bold : FontWeights.Normal;
        }


        private void AnimateFontSize(Button button, double toSize)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = toSize,
                Duration = TimeSpan.FromMilliseconds(600),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            button.BeginAnimation(Button.FontSizeProperty, animation);
        }

        private void butRechercheMat_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new FicheClients.UserControls.UCMaterielEnReserve());
            UpdateButtonStates();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown(); 
        }

        private void butReparation_Click(object sender, RoutedEventArgs e)
        {
            SPcentral.Children.Clear();
            SPcentral.Children.Add(new Classe.Materiel.UserControls.UCReparation());
            UpdateButtonStates();
        }
    }
}