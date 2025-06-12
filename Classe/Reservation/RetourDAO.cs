using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SAE2._01_Loxam.Classe.Reservation
{
    class RetourDAO
    {
        public List<Reservation> GetRetoursTermines()
        {
            List<Reservation> liste = new List<Reservation>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(@"
            SELECT 
                m.Nom AS NomMateriel
            FROM 
                Materiel m
            JOIN 
                Reservation r ON m.NumReservation = r.NumReservation
            WHERE 
                m.Etat = 4"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmd);

                foreach (DataRow dr in dt.Rows)
                {
                    liste.Add(new Reservation
                    {




                    });
                }

            }
             
               


            return liste;
        }
    }
}
