public class MaterielAffichage
{
    public int NumMateriel { get; set; }
    public string Reference { get; set; }
    public string NomMateriel { get; set; }
    public string Descriptif { get; set; }
    public decimal PrixJournee { get; set; }
    public int NumEtat { get; set; }
    public string Categorie { get; set; }

    public string StatutReservation
    {
        get
        {
            return NumEtat switch
            {
                2 => "Prévue",
                3 => "En cours",
                4 => "Terminée",
                5 => "En attente de réparation",
                6 => "En réparation",
                7 => "HS",
                _ => "Disponible"
            };
        }
    }

    public string CouleurEtat
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
