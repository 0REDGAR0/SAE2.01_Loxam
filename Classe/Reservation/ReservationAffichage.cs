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
        public int NumEtat { get; set; }  // Ajouté pour récupérer l'état du matériel

        public string StatutReservation
        {
            get
            {
                return NumEtat switch
                {
                    2 => "Prévue",
                    3 => "En cours",
                    4 => "Terminée",
                    5 or 6 => "En réparation",
                    7 => "HS",
                    _ => "Disponible"
                };
            }
        }

        public string CouleurStatut
        {
            get
            {
                return NumEtat switch
                {
                    1 => "#2ECC71",  // Disponible - Vert
                    2 => "#3498DB",  // Location prévue - Bleu
                    3 => "#9B59B6",  // Loué - Violet
                    4 => "#145A32",  // Retourné - Vert foncé
                    5 => "#F1C40F",  // En attente de réparation - Jaune
                    6 => "#E67E22",  // Réparation en cours - Orange
                    7 => "#E74C3C",  // Hors Service - Rouge
                    _ => "#7F8C8D"   // Inconnu - Gris
                };
            }
        }
    }
}
