using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam
{
    public class Client : ICrud<Client>, INotifyPropertyChanged
    {
        private int numClient;
        private string nomClient;
        private string prenomClient;

        public Client()
        {
        }

        public Client(int numClient, string nomClient, string prenomClient)
        {
            this.NumClient = numClient;
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
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
