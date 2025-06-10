using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe
{
    public class DataGridLists
    {
        private ObservableCollection<Reservation> lesReservations;
        private String nom;

        public DataGridLists()
        {
            this.lesReservations = new ObservableCollection<Reservation>(new Reservation().FindAllResa());
        }

        public DataGridLists(string nom)
        {
            this.Nom = nom;
            this.lesReservations = new ObservableCollection<Reservation>(new Reservation().FindAllResa());
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

        public ObservableCollection<Reservation> LesReservations
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
