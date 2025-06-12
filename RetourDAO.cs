using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam
{
    public class RetourDAO
    {
        public List<RetourAffichage> GetRetoursAffichage()
        {
            List<RetourAffichage> list = new List<RetourAffichage>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand(@"
            SELECT 
                r.numreservation,
                m.nommateriel,
                r.datedebutlocation,
                r.dateretoureffectivelocation
            FROM reservation r
            JOIN materiel m ON r.nummateriel = m.nummateriel;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new RetourAffichage
                    {
                        NumeroReservation = (int)dr["numreservation"],
                        Materiel = dr["nommateriel"].ToString(),
                        DateDebutLocation = DateTime.Parse(dr["datedebutlocation"].ToString()),
                        DateRetourEffective = DateTime.Parse(dr["dateretoureffectivelocation"].ToString()),
                    });
                }
            }
            return list;
        }
    }
}
