using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe.Retours
{
    public class Retour
    {
        private int numMateriel;
        private int nomMateriel;
        private int numReservation;
        private int numEtat;

        public Retour()
        {
        }

        public Retour(int numMateriel, int nomMateriel, int numReservation, int numEtat)
        {
            this.NumMateriel = numMateriel;
            this.NomMateriel = nomMateriel;
            this.NumReservation = numReservation;
            this.NumEtat = numEtat;
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

        public int NomMateriel
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

        public int NumReservation
        {
            get
            {
                return this.numReservation;
            }

            set
            {
                this.numReservation = value;
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

        public override bool Equals(object? obj)
        {
            return obj is Retour retour &&
                   this.NumMateriel == retour.NumMateriel &&
                   this.NomMateriel == retour.NomMateriel &&
                   this.NumReservation == retour.NumReservation &&
                   this.NumEtat == retour.NumEtat;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.NumMateriel, this.NomMateriel, this.NumReservation, this.NumEtat);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(Retour? left, Retour? right)
        {
            return EqualityComparer<Retour>.Default.Equals(left, right);
        }

        public static bool operator !=(Retour? left, Retour? right)
        {
            return !(left == right);
        }
    }
}
