using Npgsql;
using SAE2._01_Loxam.Classe.Reservation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe.Retours
{
    public class RetourDAO
    {
        public List<RetourAffichage> GetRetourAffichage()
        {
            List<RetourAffichage> list = new List<RetourAffichage>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
                    SELECT m.numMateriel, m.nomMateriel, r.numReservation, e.numEtat
                    FROM materiel m
                    JOIN Reservation r ON m.numMateriel = r.numMateriel
                    JOIN Etat e ON m.numEtat = e.numEtat;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new RetourAffichage
                    {
                        NumMateriel = (int)dr["m.numMateriel"],
                        NomMateriel = (int)dr["m.numMateriel"],
                        NumReservation = (int)dr["m.numMateriel"],
                        NumEtat = (int)dr["numetat"]
                    });
                }




                return list;
            }
        }
    }
}
