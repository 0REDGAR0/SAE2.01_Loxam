using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace SAE2._01_Loxam.Classe.Retour
{
    public class RetourDAO
    {
        public List<RetourAffichage> GetRetourAffichage()
        {
            List<RetourAffichage> liste = new List<RetourAffichage>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
                SELECT 
                    r.numreservation,
                    c.nomclient || ' ' || c.prenomclient AS client,
                    m.nommateriel AS materiel,
                    m.nummateriel,  -- ✅ ici on récupère le NumMateriel
                    cat.libellecategorie AS categorie,
                    m.numetat,
                    r.datereservation,
                    r.datedebutlocation,
                    r.dateretoureffectivelocation,
                    r.dateretourreellelocation,
                    r.prixtotal
                FROM reservation r
                JOIN client c ON r.numclient = c.numclient
                JOIN materiel m ON r.nummateriel = m.nummateriel
                JOIN type t ON m.numtype = t.numtype
                JOIN categorie cat ON t.numcategorie = cat.numcategorie
                WHERE m.numetat = 4;
            "))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    liste.Add(new RetourAffichage
                    {
                        NumeroReservation = (int)dr["numreservation"],
                        Client = dr["client"].ToString(),
                        Materiel = dr["materiel"].ToString(),
                        NumMateriel = (int)dr["nummateriel"],
                        Categorie = dr["categorie"].ToString(),
                        NumEtat = (int)dr["numetat"],
                        DateReservation = DateTime.Parse(dr["datereservation"].ToString()),
                        DateDebutLocation = DateTime.Parse(dr["datedebutlocation"].ToString()),
                        DateRetourEffective = DateTime.Parse(dr["dateretoureffectivelocation"].ToString()),
                        DateRetourReelle = DateTime.Parse(dr["dateretourreellelocation"].ToString()),
                        PrixTotal = decimal.Parse(dr["prixtotal"].ToString())
                    });
                }

            }
            return liste;
        }

        public bool MettreMaterielEnReparation(int numMateriel)
        {
            try
            {
                using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(@"
                    UPDATE materiel
                    SET numetat = 5
                    WHERE nummateriel = @NumMateriel;
                "))
                {
                    cmdUpdate.Parameters.AddWithValue("@NumMateriel", numMateriel);
                    DataAccess.Instance.ExecuteNonQuery(cmdUpdate);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool MettreMaterielDisponible(int numMateriel)
        {
            try
            {
                using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(@"
                    UPDATE materiel
                    SET numetat = 1
                    WHERE nummateriel = @NumMateriel;
                "))
                {
                    cmdUpdate.Parameters.AddWithValue("@NumMateriel", numMateriel);
                    DataAccess.Instance.ExecuteNonQuery(cmdUpdate);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
