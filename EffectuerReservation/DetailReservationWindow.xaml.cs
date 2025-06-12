using SAE2._01_Loxam.Classe.Reservation;
using System.Windows;

namespace SAE2._01_Loxam.FicheClients.UserControls
{
    public partial class DetailReservationWindow : Window
    {
        private ReservationAffichage reservationCourante;
        private DataAccess dataAccess = DataAccess.Instance;

        // Remplace ici par l’ID exact de l’état "Réparation" dans ta table ETAT
        private const int idEtatReparation = 3;

        public DetailReservationWindow(ReservationAffichage reservation)
        {
            InitializeComponent();
            reservationCourante = reservation;
            DataContext = reservationCourante;
        }
        private void butOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void butRetourne_Click(object sender, RoutedEventArgs e)
        {
            var reservationToUpdate = new Reservation
            {
                NumReservation = reservationCourante.NumeroReservation,
                DateRetourReelleLocation = DateTime.Today
            };

            dataAccess.MettreAJourReservation(reservationToUpdate);
            MessageBox.Show("Matériel retourné aujourd'hui.");
            this.Close();
        }

        private void butReparation_Click(object sender, RoutedEventArgs e)
        {
            var materiel = dataAccess.GetMaterielById(reservationCourante.NumeroReservation);

            if (materiel != null)
            {
                materiel.NumEtat = idEtatReparation;
                dataAccess.MettreAJourMateriel(materiel);
                MessageBox.Show("Matériel envoyé en réparation.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Matériel non trouvé !");
            }
        }


    }
}
