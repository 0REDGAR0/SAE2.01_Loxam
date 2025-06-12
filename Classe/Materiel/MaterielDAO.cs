using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace SAE2._01_Loxam.Classe.Materiel
{
    public class MaterielDAO
    {
        public List<MaterielAffichage> GetMaterielsAffichage()
        {
            List<MaterielAffichage> liste = new List<MaterielAffichage>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(
                @"SELECT m.nummateriel, m.reference, m.nommateriel, m.descriptif, m.prixjournee, m.numetat, cat.libellecategorie
                FROM materiel m
                JOIN type t ON m.numtype = t.numtype
                JOIN categorie cat ON t.numcategorie = cat.numcategorie"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    liste.Add(new MaterielAffichage
                    {
                        NumMateriel = (int)dr["nummateriel"],
                        Reference = dr["reference"].ToString(),
                        NomMateriel = dr["nommateriel"].ToString(),
                        Descriptif = dr["descriptif"].ToString(),
                        PrixJournee = Convert.ToDecimal(dr["prixjournee"]),
                        NumEtat = (int)dr["numetat"],
                        Categorie = dr["libellecategorie"].ToString()
                    });
                }
            }
            return liste;
        }


        public Materiel GetMaterielByReservation(int numReservation)
        {
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
                    SELECT m.nummateriel, m.numetat, m.numtype, m.reference, m.nommateriel, m.descriptif, m.prixjournee
                    FROM materiel m
                    JOIN reservation r ON m.nummateriel = r.nummateriel
                    WHERE r.numreservation = @NumReservation;
                "))
            {
                cmdSelect.Parameters.AddWithValue("@NumReservation", numReservation);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    return new Materiel
                    {
                        NumMateriel = (int)dr["nummateriel"],
                        NumEtat = (int)dr["numetat"],
                        NumType = (int)dr["numtype"],
                        Reference = dr["reference"].ToString(),
                        NomMateriel = dr["nommateriel"].ToString(),
                        Descriptif = dr["descriptif"].ToString(),
                        PrixJournee = Convert.ToDecimal(dr["prixjournee"])
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public void MettreAJourMateriel(Materiel materiel)
        {
            using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(@"
                    UPDATE materiel
                    SET numetat = @NumEtat
                    WHERE nummateriel = @NumMateriel;
                "))
            {
                cmdUpdate.Parameters.AddWithValue("@NumEtat", materiel.NumEtat);
                cmdUpdate.Parameters.AddWithValue("@NumMateriel", materiel.NumMateriel);
                DataAccess.Instance.ExecuteNonQuery(cmdUpdate);
            }
        }
    }
}
