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

        public string StatutReservation
        {
            get
            {
                if (DateRetourReelle > DateTime.Now)
                {
                    if (DateDebutLocation > DateTime.Now)
                        return "Prévue";
                    else
                        return "En cours";
                }
                else
                    return "Terminée";
            }
        }

        public string CouleurStatut
        {
            get
            {
                if (StatutReservation == "Prévue")
                    return "#3498DB"; // Bleu
                if (StatutReservation == "En cours")
                    return "#F39C12"; // Orange
                return "#2ECC71"; // Vert
            }
        }
    }



}
