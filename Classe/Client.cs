using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("L'adresse doit être renseigné");
                }
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
                if (!(Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")))
                {
                    throw new ArgumentException($"Le mail : {value} n'est pas une adresse valide");
                }
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le mail doit être renseigné");
                }
                if (value.Length > 60)
                {
                    throw new ArgumentException("doit avoir moins de 60 caractères");
                }
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
                if (!(Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")))
                {
                    throw new ArgumentException($"Le numéro de téléphone : {value} n'est pas un numéro valide");
                }
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le numéro de téléphone doit être renseigné");
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException("doit avoir moins de 10 caractères");
                }
                this.numeroTelClient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumeroTelClient)));
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

        int ICrud<Client>.Create()
        {
            throw new NotImplementedException();
        }

        int ICrud<Client>.Delete()
        {
            throw new NotImplementedException();
        }

        List<Client> ICrud<Client>.FindAll()
        {
            throw new NotImplementedException();
        }

        List<Client> ICrud<Client>.FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        void ICrud<Client>.Read()
        {
            throw new NotImplementedException();
        }

        int ICrud<Client>.Update()
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
