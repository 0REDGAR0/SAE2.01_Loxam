using Npgsql;
using SAE2._01_Loxam.Classe.Materiel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Reparation
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
                    e.libelleetat
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
                        Reference = (string)dr["reference"],
                        NomMateriel = (string)dr["nommateriel"],
                        Libelletype = (string)dr["libelletype"],
                        Libellecategorie = (string)dr["libellecategorie"],
                        Libelleetat = (string)dr["libelleetat"]
                    });
                }
            }
            return liste;
        }
    }
}
