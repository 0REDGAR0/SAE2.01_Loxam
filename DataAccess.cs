using System;
using System.Data;
using Npgsql;
using SAE2._01_Loxam.Classe.Materiel;
using SAE2._01_Loxam.Classe.Reservation;

namespace SAE2._01_Loxam
{
    public class DataAccess
    {
        private static DataAccess instance;
        private readonly string connectionString;
        private NpgsqlConnection connection;

        private DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
            this.connection = new NpgsqlConnection(connectionString);
        }

        public static void Initialize(string connectionString)
        {
            if (instance == null)
            {
                instance = new DataAccess(connectionString);
            }
        }

        public static DataAccess Instance
        {
            get
            {
                if (instance == null)
                    throw new InvalidOperationException("DataAccess not initialized. Call Initialize() first.");
                return instance;
            }
        }

        public NpgsqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Erreur lors de l'ouverture de la connexion : \n" + connectionString);
                    throw;
                }
            }
            return connection;
        }

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
                LogError.Log(ex, "Erreur SQL (SELECT)");
                throw;
            }
            return dataTable;
        }

        public int ExecuteInsert(NpgsqlCommand cmd)
        {
            int id = 0;
            try
            {
                cmd.Connection = GetConnection();
                id = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL (INSERT) : " + cmd.CommandText);
                throw;
            }
            return id;
        }

        public int ExecuteSet(NpgsqlCommand cmd)
        {
            int rows = 0;
            try
            {
                cmd.Connection = GetConnection();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL (UPDATE/DELETE) : " + cmd.CommandText);
                throw;
            }
            return rows;
        }

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
                LogError.Log(ex, "Erreur SQL (SELECT scalar) : " + cmd.CommandText);
                throw;
            }
            return res;
        }

        public void MettreAJourReservation(Reservation reservation)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE reservation SET dateretourreellelocation = @DateRetourReelle WHERE numreservation = @NumReservation";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@DateRetourReelle", reservation.DateRetourReelleLocation);
                    cmd.Parameters.AddWithValue("@NumReservation", reservation.NumReservation);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void MettreAJourMateriel(Materiel materiel)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE materiel SET numetat = @NumEtat WHERE nummateriel = @NumMateriel";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NumEtat", materiel.NumEtat);
                    cmd.Parameters.AddWithValue("@NumMateriel", materiel.NumMateriel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Materiel GetMaterielById(int numMateriel)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM materiel WHERE nummateriel = @NumMateriel";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NumMateriel", numMateriel);
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

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
