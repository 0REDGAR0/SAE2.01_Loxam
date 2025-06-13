using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace SAE2._01_Loxam.Classe.Reservation
{
    internal class ReservationDAO
    {
        public List<ReservationAffichage> GetReservationsAffichage()
        {
            List<ReservationAffichage> list = new List<ReservationAffichage>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
               SELECT 
                   r.numreservation,
                   c.nomclient || ' ' || c.prenomclient AS client,
                   m.nommateriel AS materiel,
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
               WHERE m.numetat IN (1,2,3,4)

            "))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new ReservationAffichage
                    {
                        NumeroReservation = (int)dr["numreservation"],
                        Client = dr["client"].ToString(),
                        Materiel = dr["materiel"].ToString(),
                        DateReservation = DateTime.Parse(dr["datereservation"].ToString()),
                        DateDebutLocation = DateTime.Parse(dr["datedebutlocation"].ToString()),
                        DateRetourEffective = DateTime.Parse(dr["dateretoureffectivelocation"].ToString()),
                        DateRetourReelle = DateTime.Parse(dr["dateretourreellelocation"].ToString()),
                        PrixTotal = decimal.Parse(dr["prixtotal"].ToString()),
                        NumEtat = (int)dr["numetat"]
                    });
                }
            }
            return list;
        }

        public void MettreAJourReservation(Reservation reservation)
        {
            using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(@"
                    UPDATE reservation
                    SET dateretourreellelocation = @DateRetourReelleLocation
                    WHERE numreservation = @NumReservation;
                "))
            {
                cmdUpdate.Parameters.AddWithValue("@DateRetourReelleLocation", reservation.DateRetourReelleLocation);
                cmdUpdate.Parameters.AddWithValue("@NumReservation", reservation.NumReservation);
                DataAccess.Instance.ExecuteNonQuery(cmdUpdate);
            }
        }
    }
}
