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
                    liste.Add(new Client
                    {
                        NumClient = (int)dr["numclient"],
                        NomClient = dr["nomclient"].ToString(),
                        PrenomClient = dr["prenomclient"].ToString()
                    });
                }
            }
            return liste;
        }
    }
}
