﻿using System;

namespace SAE2._01_Loxam.Classe.Reservation
{
    public class ReservationAffichage
    {
        public int NumeroReservation { get; set; }
        public string Client { get; set; }
        public string Materiel { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime? DateDebutLocation { get; set; }
        public DateTime? DateRetourEffective { get; set; }
        public DateTime? DateRetourReelle { get; set; }
        public decimal PrixTotal { get; set; }
        public string Categorie { get; set; }
        public int NumMateriel { get; set; }
        public int NumEtat { get; set; }

        public string StatutReservation
        {
            get
            {
                return NumEtat switch
                {
                    2 => "Prévue",
                    3 => "En cours",
                    4 => "Terminée/Retourne",
                    5 => "En attente de réparation",
                    6 => "En réparation",
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
                    1 => "#2ECC71",
                    2 => "#3498DB",
                    3 => "#9B59B6",
                    4 => "#145A32",
                    5 => "#F1C40F",
                    6 => "#E67E22",
                    7 => "#E74C3C",
                    _ => "#7F8C8D"
                };
            }
        }
    }
}
