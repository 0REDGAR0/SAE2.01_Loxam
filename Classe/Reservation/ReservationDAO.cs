using Npgsql;
using System.Collections.Generic;
using System.Data;
using SAE2._01_Loxam.Utils;

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
                   m.nummateriel,
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
                        NumMateriel = (int)dr["nummateriel"],
                        DateReservation = DateTime.Parse(dr["datereservation"].ToString()),
                        DateDebutLocation = SafeConvert.SafeParseDateTime(dr["datedebutlocation"]),
                        DateRetourEffective = SafeConvert.SafeParseDateTime(dr["dateretoureffectivelocation"]),
                        DateRetourReelle = SafeConvert.SafeParseDateTime(dr["dateretourreellelocation"]),
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

        public int CreerReservation(int numMateriel, int numClient, int numEmploye, DateTime dateReservation, DateTime dateDebut, DateTime dateRetourEffective, decimal prixTotal)
        {
            int numReservationGenere = 0;

            using (NpgsqlCommand cmdInsert = new NpgsqlCommand(@"
                INSERT INTO reservation 
                (nummateriel, numclient, numemploye, datereservation, datedebutlocation, dateretoureffectivelocation, dateretourreellelocation, prixtotal)
                VALUES 
                (@NumMateriel, @NumClient, @NumEmploye, @DateReservation, @DateDebut, @DateRetourEffective, NULL, @PrixTotal)
                RETURNING numreservation;
            "))
            {
                cmdInsert.Parameters.AddWithValue("@NumMateriel", numMateriel);
                cmdInsert.Parameters.AddWithValue("@NumClient", numClient);
                cmdInsert.Parameters.AddWithValue("@NumEmploye", numEmploye);
                cmdInsert.Parameters.AddWithValue("@DateReservation", dateReservation);
                cmdInsert.Parameters.AddWithValue("@DateDebut", dateDebut);
                cmdInsert.Parameters.AddWithValue("@DateRetourEffective", dateRetourEffective);
                cmdInsert.Parameters.AddWithValue("@PrixTotal", prixTotal);

                numReservationGenere = (int)DataAccess.Instance.ExecuteSelectUneValeur(cmdInsert);
            }

            int nouvelEtat = (dateDebut > DateTime.Now.Date) ? 2 : 3;

            using (NpgsqlCommand cmdUpdateEtat = new NpgsqlCommand(@"
                UPDATE materiel
                SET numetat = @NumEtat
                WHERE nummateriel = @NumMateriel;
            "))
            {
                cmdUpdateEtat.Parameters.AddWithValue("@NumEtat", nouvelEtat);
                cmdUpdateEtat.Parameters.AddWithValue("@NumMateriel", numMateriel);

                DataAccess.Instance.ExecuteNonQuery(cmdUpdateEtat);
            }

            return numReservationGenere;
        }

        public bool SupprimerReservation(int numReservation, int numMateriel)
        {
            try
            {
                using (NpgsqlCommand cmdDelete = new NpgsqlCommand(@"
                    DELETE FROM reservation
                    WHERE numreservation = @NumReservation;
                "))
                {
                    cmdDelete.Parameters.AddWithValue("@NumReservation", numReservation);
                    DataAccess.Instance.ExecuteNonQuery(cmdDelete);
                }

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
