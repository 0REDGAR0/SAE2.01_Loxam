using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace SAE2._01_Loxam.Classe.Materiel
{
    public class MaterielDAO
    {
        public List<MaterielAffichage> GetMaterielsAffichage()
        {
            List<MaterielAffichage> liste = new List<MaterielAffichage>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT nummateriel, reference, nommateriel, descriptif, prixjournee, numetat FROM materiel"))
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
                        NumEtat = (int)dr["numetat"]
                    });
                }
            }
            return liste;
        }

    }
}
