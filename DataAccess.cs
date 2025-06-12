using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Npgsql;
using SAE2._01_Loxam.Classe.Materiel;
using SAE2._01_Loxam.Classe.Reservation;

namespace SAE2._01_Loxam
{
    public class DataAccess
    {

        private static readonly DataAccess instance = new DataAccess();
        private readonly string connectionString = $"Host=srv-peda-new;" +
            $"Port=5433;" +
            $"Username=beduneye;" +
            $"Password=WVTvXG;" +
            $"Database=sae_loxam;" +
            $"Options='-c " +
            $"search_path=loxam'";

        private NpgsqlConnection connection;



        public DataAccess()
        {
            connection = new NpgsqlConnection(connectionString);
        }


        public static DataAccess Instance
        {
            get
            {
                return instance;
            }
        }


        //  Constructeur privé pour empêcher l'instanciation multiple
        public DataAccess(string login)
        {
            try
            {
                connection = new NpgsqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                throw;
            }
        }

        // pour récupérer la connexion (et l'ouvrir si nécessaire)
        public NpgsqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed || this.connection.State == ConnectionState.Broken)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                    throw;
                }
            }


            return connection;
        }

        //  pour requêtes SELECT et retourne un DataTable (table de données en mémoire)
        public DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL");
                throw;
            }
            return dataTable;
        }

        //   pour requêtes INSERT et renvoie l'ID généré

        public int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete insert " + cmd.CommandText);
                throw;
            }
            return nb;
        }




        //  pour requêtes UPDATE, DELETE
        public int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete set " + cmd.CommandText);
                throw;
            }
            return nb;

        }

        // pour requêtes avec une seule valeur retour  (ex : COUNT, SUM) 
        public object ExecuteSelectUneValeur(NpgsqlCommand cmd)
        {
            object res = null;
            try
            {
                cmd.Connection = GetConnection();
                res = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete select " + cmd.CommandText);
                throw;
            }
            return res;

        }

        public void MettreAJourReservation(Reservation reservation)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE RESERVATION SET dateretourreellelocation = @dateretourreellelocation WHERE numreservation = @numreservation";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@dateretourreellelocation", reservation.DateRetourReelleLocation);
                    cmd.Parameters.AddWithValue("@numreservation", reservation.NumReservation);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void MettreAJourMateriel(Materiel materiel)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE MATERIEL SET numetat = @numetat WHERE nummateriel = @nummateriel";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@numetat", materiel.NumEtat);
                    cmd.Parameters.AddWithValue("@nummateriel", materiel.NumMateriel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Materiel GetMaterielById(int numMateriel)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM MATERIEL WHERE nummateriel = @nummateriel";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@nummateriel", numMateriel);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Materiel
                            {
                                NumMateriel = reader.GetInt32(reader.GetOrdinal("nummateriel")),
                                NumEtat = reader.GetInt32(reader.GetOrdinal("numetat")),
                                NumType = reader.GetInt32(reader.GetOrdinal("numtype")),
                                Reference = reader["reference"].ToString(),
                                NomMateriel = reader["nommateriel"].ToString(),
                                Descriptif = reader["descriptif"].ToString(),
                                PrixJournee = reader.GetDecimal(reader.GetOrdinal("prixjournee"))
                            };
                        }
                    }
                }
            }
            return null;
        }
        public int ExecuteNonQuery(NpgsqlCommand cmd)
        {
            int affectedRows = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();
                cmd.Connection = connection;
                affectedRows = cmd.ExecuteNonQuery();
            }
            return affectedRows;
        }


        //  Fermer la connexion 
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
