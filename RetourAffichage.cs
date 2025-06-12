using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam
{
    public class RetourAffichage
    {
        private int numeroReservation;
        private string materiel;
        private DateTime dateDebutLocation;
        private DateTime dateRetourEffective;
        private string etat;

        public int NumeroReservation
        {
            get
            {
                return this.numeroReservation;
            }

            set
            {
                this.numeroReservation = value;
            }
        }

        public string Materiel
        {
            get
            {
                return this.materiel;
            }

            set
            {
                this.materiel = value;
            }
        }

        public DateTime DateDebutLocation
        {
            get
            {
                return this.dateDebutLocation;
            }

            set
            {
                this.dateDebutLocation = value;
            }
        }

        public DateTime DateRetourEffective
        {
            get
            {
                return this.dateRetourEffective;
            }

            set
            {
                this.dateRetourEffective = value;
            }
        }

        public string Etat
        {
            get
            {
                if (DateTime.Now > DateRetourEffective)
                    return "Indisponible";
                else
                    return "Disponible";
            }

            set
            {
                this.etat = value;
            }
        }
    }
}
