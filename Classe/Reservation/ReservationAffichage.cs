using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe.Reservation
{
    public class ReservationAffichage
    {
        public int NumeroReservation { get; set; }
        public string Client { get; set; }
        public string Materiel { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime DateDebutLocation { get; set; }
        public DateTime DateRetourEffective { get; set; }
        public DateTime DateRetourReelle { get; set; }
        public decimal PrixTotal { get; set; }
    }

}
