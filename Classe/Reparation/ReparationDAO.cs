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
                    m.numetat
                FROM materiel m
                JOIN etat e ON m.numetat = e.numetat
                JOIN type t ON m.numtype = t.numtype
                JOIN categorie c ON t.numcategorie = c.numcategorie
                where e.numetat in (5, 6, 7)
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
                        NumEtat = (int)dr["numetat"]
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


        private readonly string connectionString = $"Host=srv-peda-new;" +
            $"Port=5433;" +
            $"Username=beduneye;" +
            $"Password=WVTvXG;" +
            $"Database=sae_loxam;" +
            $"Options='-c " +
            $"search_path=loxam'";
        public void MettreAJourEtatEtCommentaireMateriel(int numMateriel, int numEtat, string commentaire)
        {

            using var connexion = new NpgsqlConnection(connectionString);
            connexion.Open();

            string query = "UPDATE materiel SET numetat = @etat, commentaire = @commentaire WHERE nummateriel = @num";

            using var cmd = new NpgsqlCommand(query, connexion);
            cmd.Parameters.AddWithValue("@etat", numEtat);
            cmd.Parameters.AddWithValue("@commentaire", (object?)commentaire ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@num", numMateriel);

            cmd.ExecuteNonQuery();
        }

    }
}
