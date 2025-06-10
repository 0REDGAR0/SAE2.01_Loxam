using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe
{
    public class Reservation
    {
        private int numReservation;
        private int numMateriel;
        private int numEmploye;
        private int numClient;
        private DateTime dateReservation;
        private DateTime dateDebutLocation;
        private DateTime dateRetourEffectiveLocation;
        private DateTime dateRetourReelleLocation;
        private double prixTotal;

        public Reservation()
        {
        }

        public Reservation(int numReservation, int numMateriel, int numEmploye, int numClient, DateTime dateReservation, DateTime dateDebutLocation, DateTime dateRetourEffectiveLocation, DateTime dateRetourReelleLocation, double prixTotal)
        {
            this.NumReservation = numReservation;
            this.NumMateriel = numMateriel;
            this.NumEmploye = numEmploye;
            this.NumClient = numClient;
            this.DateReservation = dateReservation;
            this.DateDebutLocation = dateDebutLocation;
            this.DateRetourEffectiveLocation = dateRetourEffectiveLocation;
            this.DateRetourReelleLocation = dateRetourReelleLocation;
            this.PrixTotal = prixTotal;
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

        public int NumEmploye
        {
            get
            {
                return this.numEmploye;
            }

            set
            {
                this.numEmploye = value;
            }
        }

        public int NumClient
        {
            get
            {
                return this.numClient;
            }

            set
            {
                this.numClient = value;
            }
        }

        public DateTime DateReservation
        {
            get
            {
                return this.dateReservation;
            }

            set
            {
                this.dateReservation = value;
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

        public DateTime DateRetourEffectiveLocation
        {
            get
            {
                return this.dateRetourEffectiveLocation;
            }

            set
            {
                this.dateRetourEffectiveLocation = value;
            }
        }

        public DateTime DateRetourReelleLocation
        {
            get
            {
                return this.dateRetourReelleLocation;
            }

            set
            {
                this.dateRetourReelleLocation = value;
            }
        }

        public double PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Reservation reservation &&
                   this.NumReservation == reservation.NumReservation &&
                   this.NumMateriel == reservation.NumMateriel &&
                   this.NumEmploye == reservation.NumEmploye &&
                   this.NumClient == reservation.NumClient &&
                   this.DateReservation == reservation.DateReservation &&
                   this.DateDebutLocation == reservation.DateDebutLocation &&
                   this.DateRetourEffectiveLocation == reservation.DateRetourEffectiveLocation &&
                   this.DateRetourReelleLocation == reservation.DateRetourReelleLocation &&
                   this.PrixTotal == reservation.PrixTotal;
        }

        public static bool operator ==(Reservation? left, Reservation? right)
        {
            return EqualityComparer<Reservation>.Default.Equals(left, right);
        }

        public static bool operator !=(Reservation? left, Reservation? right)
        {
            return !(left == right);
        }

        public List<Reservation> FindAllResa()
        {
            List<Reservation> list = new List<Reservation>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from reservation ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    list.Add(new Reservation(
                        (int)dr["numreservation"], 
                        (int)dr["nummateriel"],
                        (int)dr["numemploye"],
                        (int)dr["numclient"],
                        DateTime.Parse(dr["datereservation"].ToString()),
                        DateTime.Parse(dr["datedebutlocation"].ToString()),
                        DateTime.Parse(dr["dateretoureffectivelocation"].ToString()),
                        DateTime.Parse(dr["dateretourreellelocation"].ToString()),
                        double.Parse(dr["prixtotal"].ToString())
                        ));
            }
            return list; 
        }
    }

}
