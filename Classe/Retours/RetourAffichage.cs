namespace SAE2._01_Loxam.Classe.Retour
{
    public class RetourAffichage
    {
        public int NumeroReservation { get; set; }
        public string Client { get; set; }
        public string Materiel { get; set; }
        public int NumMateriel { get; set; }
        public string Categorie { get; set; }
        public int NumEtat { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime DateDebutLocation { get; set; }
        public DateTime DateRetourEffective { get; set; }
        public DateTime DateRetourReelle { get; set; }
        public decimal PrixTotal { get; set; }

        public string StatutReservation
        {
            get
            {
                return NumEtat switch
                {
                    4 => "Retourné",
                    _ => "Inconnu"
                };
            }
        }

        public string CouleurStatut
        {
            get
            {
                return NumEtat switch
                {
                    4 => "#145A32",  // Retourné (vert foncé)
                    _ => "#7F8C8D"
                };
            }
        }
    }
}
