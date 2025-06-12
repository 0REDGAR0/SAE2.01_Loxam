using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe.Materiel
{
    public class Materiel
    {
        private int numMateriel;
        private int numEtat;
        private int numType;
        private string reference;
        private string nomMateriel;
        private string descriptif;
        private decimal prixJournee;

        public Materiel()
        {

        }

        public Materiel(string nomMateriel)
        {
            this.NomMateriel = nomMateriel;
        }

        public Materiel(string reference, string nomMateriel, string descriptif, decimal prixJournee)
        {
            this.Reference = reference;
            this.NomMateriel = nomMateriel;
            this.Descriptif = descriptif;
            this.PrixJournee = prixJournee;
        }

        public Materiel(int numMateriel, int numEtat, int numType, string reference, string nomMateriel, string descriptif, decimal prixJournee)
        {
            this.NumMateriel = numMateriel;
            this.NumEtat = numEtat;
            this.NumType = numType;
            this.Reference = reference;
            this.NomMateriel = nomMateriel;
            this.Descriptif = descriptif;
            this.PrixJournee = prixJournee;
        }

        public int NumMateriel
        {
            get
            {
                return this.numMateriel;
            }

            set
            {
                this.numMateriel = value;
            }
        }

        public int NumEtat
        {
            get
            {
                return this.numEtat;
            }

            set
            {
                this.numEtat = value;
            }
        }

        public int NumType
        {
            get
            {
                return this.numType;
            }

            set
            {
                this.numType = value;
            }
        }

        public string Reference
        {
            get
            {
                return this.reference;
            }

            set
            {
                this.reference = value;
            }
        }

        public string NomMateriel
        {
            get
            {
                return this.nomMateriel;
            }

            set
            {
                this.nomMateriel = value;
            }
        }

        public string Descriptif
        {
            get
            {
                return this.descriptif;
            }

            set
            {
                this.descriptif = value;
            }
        }

        public decimal PrixJournee
        {
            get
            {
                return this.prixJournee;
            }

            set
            {
                this.prixJournee = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Materiel materiel &&
                   this.NumMateriel == materiel.NumMateriel &&
                   this.NumEtat == materiel.NumEtat &&
                   this.NumType == materiel.NumType &&
                   this.Reference == materiel.Reference &&
                   this.NomMateriel == materiel.NomMateriel &&
                   this.Descriptif == materiel.Descriptif &&
                   this.PrixJournee == materiel.PrixJournee;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
