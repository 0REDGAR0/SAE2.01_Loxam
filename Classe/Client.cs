using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAE2._01_Loxam
{
    public class Client : ICrud<Client>, INotifyPropertyChanged
    {
        private int numClient;
        private string nomClient;
        private string prenomClient;
        private string adresseClient;
        private string mailClient;
        private string numeroTelClient;
        private ObservableCollection<Client> lesClients;

        public Client()
        {
        }

        public Client(int numClient, string nomClient, string prenomClient, string adresseClient, string mailClient, string numeroTelClient)
        {
            this.NumClient = numClient;
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
            this.AdresseClient = adresseClient;
            this.MailClient = mailClient;
            this.NumeroTelClient = numeroTelClient;
        }

        public Client(string nomClient, string prenomClient, string adresseClient, string mailClient, string numeroTelClient)
        {
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
            this.AdresseClient = adresseClient;
            this.MailClient = mailClient;
            this.NumeroTelClient = numeroTelClient;
        }

        public Client(string nomClient)
        {
            this.NomClient = nomClient;
            this.LesClients = new ObservableCollection<Client>(FindAll());
        }

        public int NumClient
        {
            get
            {
                return this.numClient;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Le numéro client ne peut pas être négatif");
                }
                this.numClient = value;
            }
        }

        public string NomClient
        {
            get
            {
                return this.nomClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le nom doit être renseigné");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("doit avoir moins de 30 caractères");
                }
                this.nomClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomClient)));
            }
        }

        public string PrenomClient
        {
            get
            {
                return this.prenomClient;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le nom doit être renseigné");
                }
                if (value.Length > 30)
                {
                    throw new ArgumentException("doit avoir moins de 30 caractères");
                }
                this.prenomClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrenomClient)));
            }
        }

        public string AdresseClient
        {
            get
            {
                return this.adresseClient;
            }

            set
            {
                /*if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("L'adresse doit être renseigné");
                }*/
                if (value.Length > 60)
                {
                    throw new ArgumentException("doit avoir moins de 60 caractères");
                }
                this.adresseClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseClient)));
            }
        }

        public string MailClient
        {
            get
            {
                return this.mailClient;
            }

            set
            {
                /*if (value != null && (value.Length > 60 || !(Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))))
                {
                    throw new ArgumentException($"Le mail : {value} n'est pas une adresse valide");
                }
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le mail doit être renseigné");
                }*/
                this.mailClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailClient)));
            }
        }

        public string NumeroTelClient
        {
            get
            {
                return this.numeroTelClient;
            }

            set
            {
                /*if (value.Length > 10 || !(Regex.IsMatch(value, @"^(0\d{9}|\+33\d{9})$")))
                {
                    throw new ArgumentException($"Le numéro de téléphone : {value} n'est pas un numéro valide");
                }
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le numéro de téléphone doit être renseigné");
                }*/
                this.numeroTelClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumeroTelClient)));
            }
        }

        public ObservableCollection<Client> LesClients
        {
            get
            {
                return this.lesClients;
            }

            set
            {
                this.lesClients = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override bool Equals(object? obj)
        {
            return obj is Client client &&
                   this.NumClient == client.NumClient &&
                   this.NomClient == client.NomClient &&
                   this.PrenomClient == client.PrenomClient;
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into client (nomclient, prenomclient, adresseclient, mailclient, numerotelclient) values (@nomClient, @prenomClient, @adresseClient, @mailClient, @numeroTelClient) RETURNING numClient"))
            {
                /*
                cmdInsert.Parameters.AddWithValue("prenomclient", this.PrenomClient);
                cmdInsert.Parameters.AddWithValue("adresseclient", this.AdresseClient);
                cmdInsert.Parameters.AddWithValue("mailclient", this.MailClient);
                cmdInsert.Parameters.AddWithValue("numerotelclient", this.NumeroTelClient);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);*/
                cmdInsert.Parameters.AddWithValue("nomclient", (object?)this.NomClient ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("prenomclient", (object?)this.PrenomClient ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("adresseclient", (object?)this.AdresseClient ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("mailclient", (object?)this.MailClient ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("numerotelclient", (object?)this.NumeroTelClient ?? DBNull.Value);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumClient = nb;
            return nb;
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from client  where numclient =@numClient;"))
            {
                cmdUpdate.Parameters.AddWithValue("numclient", this.NumClient);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        List<Client> FindAll()
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from client ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesClients.Add(new Client(dr["numClient"] is DBNull ? 0 : (Int32)dr["numClient"], dr["nomclient"] is DBNull ? "" : (String)dr["nomclient"],
                   dr["prenomClient"] is DBNull ? "" : (String)dr["prenomClient"], dr["adresseClient"] is DBNull ? "" : (String)dr["adresseClient"], dr["mailClient"] is DBNull ? "" :(String)dr["mailClient"], dr["numeroTelClient"] is DBNull ? "" : (String)dr["numeroTelClient"]));
            }
            return lesClients;
        }

        List<Client> ICrud<Client>.FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        void ICrud<Client>.Read()
        {
            using (var cmdSelect = new NpgsqlCommand("select * from  client  where numclient =@numClient;"))
            {
                cmdSelect.Parameters.AddWithValue("numclient", this.NumClient);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                this.NomClient = (String)dt.Rows[0]["nomclient"];
                this.PrenomClient = (String)dt.Rows[0]["prenomclient"];
                this.AdresseClient = (String)dt.Rows[0]["adresseclient"];
                this.MailClient = (String)dt.Rows[0]["mailclient"];
                this.NumeroTelClient = (String)dt.Rows[0]["numerotelclient"];
            }
        }

        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update client set nomclient =@nomClient ,  prenomclient =@prenomClient, adresseclient =@adresseClient, mailclient =@mailClient, numerotelclient =@numeroTelClient where numclient =@numClient;"))
            {
                cmdUpdate.Parameters.AddWithValue("nomclient", this.NomClient);
                cmdUpdate.Parameters.AddWithValue("prenomclient", this.PrenomClient);
                cmdUpdate.Parameters.AddWithValue("adresseclient", this.AdresseClient);
                cmdUpdate.Parameters.AddWithValue("mailclient", this.MailClient);
                cmdUpdate.Parameters.AddWithValue("numerotelclient", this.NumeroTelClient);
                cmdUpdate.Parameters.AddWithValue("numclient", this.NumClient);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        List<Client> ICrud<Client>.FindAll()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Client? left, Client? right)
        {
            return EqualityComparer<Client>.Default.Equals(left, right);
        }

        public static bool operator !=(Client? left, Client? right)
        {
            return !(left == right);
        }
    }
}
