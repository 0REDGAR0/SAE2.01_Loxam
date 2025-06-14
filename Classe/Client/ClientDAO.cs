using SAE2._01_Loxam.Classe;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace SAE2._01_Loxam.Classe.Client
{
    public class ClientDAO
    {
        public List<Client> GetTousLesClients()
        {
            List<Client> liste = new List<Client>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT numclient, nomclient, prenomclient FROM client"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Client client = new Client
                    {
                        NumClient = (int)dr["numclient"],
                        NomClient = dr["nomclient"].ToString(),
                        PrenomClient = dr["prenomclient"].ToString(),
                        NbMaterielEnReservation = GetNbMaterielEnReservation((int)dr["numclient"])
                    };

                    liste.Add(client);
                }
            }
            return liste;
        }

        private int GetNbMaterielEnReservation(int numClient)
        {
            using (var cmd = new NpgsqlCommand(@"
                SELECT COUNT(*)
                FROM RESERVATION r
                JOIN MATERIEL m ON r.nummateriel = m.nummateriel
                WHERE r.numclient = @numClient
                AND m.numetat IN (2, 3);
            "))
            {
                cmd.Parameters.AddWithValue("numClient", numClient);
                object result = DataAccess.Instance.ExecuteSelectUneValeur(cmd);
                return Convert.ToInt32(result);
            }
        }

    }
}
