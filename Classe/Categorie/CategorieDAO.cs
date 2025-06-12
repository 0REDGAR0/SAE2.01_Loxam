using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace SAE2._01_Loxam.Classe
{
    public class CategorieDAO
    {
        public List<string> GetToutesCategories()
        {
            List<string> categories = new List<string>();

            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT libellecategorie FROM categorie"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    categories.Add(dr["libellecategorie"].ToString());
                }
            }

            return categories;
        }
    }
}
