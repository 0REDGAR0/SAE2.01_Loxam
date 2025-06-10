using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam
{
    public class Employe
    {
        private int idEmploye;
        private string nom;
        private string prenom;
        private string login;
        private string mdp;
        private string numrole;

        public Employe()
        {
        }

        public Employe(int idEmploye, string nom, string prenom, string login, string mdp, string numrole)
        {
            this.idEmploye = idEmploye;
            this.nom = nom;
            this.prenom = prenom;
            this.login = login;
            this.mdp = mdp;
            this.numrole = numrole;

        }

        public int IdClient
        {
            get
            {
                return this.idEmploye;
            }

            set
            {
                this.idEmploye = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
            }
        }

        public string Mdp
        {
            get
            {
                return this.mdp;
            }

            set
            {
                this.mdp = value;
            }
        }

        public string Numrole
        {
            get
            {
                return this.numrole;
            }

            set
            {
                this.numrole = value;
            }
        }

        public static Employe VerifierConnexion(string login, string mdp)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM Employe WHERE login = @login AND mdp = @mdp AND numrole = 1");
            
            //Ces deux lignes servent à attribuer les réponses de l'utilisateur aux attributs de la table dans la bdd
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@mdp", mdp);

            DataTable table = DataAccess.Instance.ExecuteSelect(cmd);

            if (table.Rows.Count == 0)
            {
                return null; // Aucun résultat → mauvais identifiants OU pas un employé (peut-être un responsable atelier)
            }

            DataRow row = table.Rows[0];
            Employe emp = new Employe
            {
                idEmploye = Convert.ToInt32(row["numemploye"]),
                nom = row["nom"].ToString(),
                prenom = row["prenom"].ToString(),
                login = row["login"].ToString(),
                mdp = row["mdp"].ToString(),
                numrole = row["numrole"].ToString()
            };
            return emp; 
        }   
    }
}
