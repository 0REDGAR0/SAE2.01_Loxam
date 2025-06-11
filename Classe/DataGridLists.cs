using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE2._01_Loxam.Classe.Reservation;


namespace SAE2._01_Loxam.Classe
{
    public class DataGridLists
    {
        private ObservableCollection<Reservation.Reservation> lesReservations;
        private String nom;

        public DataGridLists()
        {
            this.lesReservations = new ObservableCollection<Reservation.Reservation>(new Reservation.Reservation().FindAllResa());
        }

        public DataGridLists(string nom)
        {
            this.Nom = nom;
            this.lesReservations = new ObservableCollection<Reservation.Reservation>(new Reservation.Reservation().FindAllResa());
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public ObservableCollection<Reservation.Reservation> LesReservations
        {
            get
            {
                return this.lesReservations;
            }

            set
            {
                this.lesReservations = value;
            }
        }
    }
}
