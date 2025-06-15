using Npgsql;
using SAE2._01_Loxam.Classe.Materiel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe.Reparation
{
    public class ReparationDAO
    {
        public List<ReparationAffichage> GetReparationAffichage()
        {
            List<ReparationAffichage> liste = new List<ReparationAffichage>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
                SELECT 
                    m.nummateriel,
                    m.reference,
                    m.nommateriel,
                    t.libelletype,
                    c.libellecategorie,
                    e.libelleetat,
                    m.numetat,
                    m.commentaire
                FROM materiel m
                JOIN etat e ON m.numetat = e.numetat
                JOIN type t ON m.numtype = t.numtype
                JOIN categorie c ON t.numcategorie = c.numcategorie
                WHERE e.numetat IN (5, 6, 7)
            "))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    liste.Add(new ReparationAffichage
                    {
                        NumMateriel = (int)dr["nummateriel"],
                        Reference = dr.IsNull("reference") ? "" : (string)dr["reference"],
                        NomMateriel = dr.IsNull("nommateriel") ? "" : (string)dr["nommateriel"],
                        Libelletype = dr.IsNull("libelletype") ? "" : (string)dr["libelletype"],
                        Libellecategorie = dr.IsNull("libellecategorie") ? "" : (string)dr["libellecategorie"],
                        Libelleetat = dr.IsNull("libelleetat") ? "" : (string)dr["libelleetat"],
                        NumEtat = (int)dr["numetat"],
                        Commentaire = dr.IsNull("commentaire") ? "" : (string)dr["commentaire"],
                    });
                }
            }
            return liste;
        }

        public void MettreAJourCommentaire(int numMateriel, string commentaire)
        {
            using var conn = DataAccess.Instance.GetConnection();
            string query = "UPDATE materiel SET commentaire = @commentaire WHERE nummateriel = @nummateriel";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@commentaire", commentaire ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@nummateriel", numMateriel);

            cmd.ExecuteNonQuery();
        }


        public void MettreAJourEtatEtCommentaireMateriel(int numMateriel, int numEtat, string commentaire)
        {
            using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(@"
                UPDATE materiel
                SET numetat = @NumEtat, commentaire = @Commentaire
                WHERE nummateriel = @NumMateriel;
            "))
            {
                cmdUpdate.Parameters.AddWithValue("@NumEtat", numEtat);
                cmdUpdate.Parameters.AddWithValue("@Commentaire", commentaire);
                cmdUpdate.Parameters.AddWithValue("@NumMateriel", numMateriel);
                DataAccess.Instance.ExecuteNonQuery(cmdUpdate);
            }
        }


    }
}
