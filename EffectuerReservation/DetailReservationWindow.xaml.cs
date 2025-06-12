using SAE2._01_Loxam.Classe.Materiel;
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

        private void butRetourne_Click(object sender, RoutedEventArgs e)
        {
            var reservationToUpdate = new Reservation
            {
                NumReservation = reservationCourante.NumeroReservation,
                DateRetourReelleLocation = DateTime.Today
            };

            ReservationDAO reservationDAO = new ReservationDAO();
            reservationDAO.MettreAJourReservation(reservationToUpdate);

            MaterielDAO materielDAO = new MaterielDAO();
            var materiel = materielDAO.GetMaterielByReservation(reservationCourante.NumeroReservation);

            if (materiel != null)
            {
                materiel.NumEtat = 4; // Retourné
                materielDAO.MettreAJourMateriel(materiel);
            }

            MessageBox.Show("Retour effectué avec succès.");
            this.Close();
        }


    }
}
