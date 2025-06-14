using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Reparation
{
    public class ReparationAffichage
    {
        private int numMateriel;
        private string reference;
        private string nomMateriel;
        private string descriptif;
        private string libelletype;
        private string libellecategorie;
        private string libelleetat;
        private int numEtat;
        private int numType;
        private string commentaire;




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

        public string Libelletype
        {
            get
            {
                return this.libelletype;
            }

            set
            {
                this.libelletype = value;
            }
        }

        public string Libellecategorie
        {
            get
            {
                return this.libellecategorie;
            }

            set
            {
                this.libellecategorie = value;
            }
        }

        public string Libelleetat
        {
            get
            {
                return this.libelleetat;
            }

            set
            {
                this.libelleetat = value;
            }
        }



        /*public string StatutReparation
        {
            get
            {
                return NumEtat switch
                {
                    5 => "En attente de réparation",
                    6 => "En réparation",
                    7 => "HS",
                    _ => "Disponible"
                };
            }
        }*/


        public string CouleurStatut
        {
            get
            {
                return NumEtat switch
                {
                    5 => "#3498DB", // En attente de réparation Bleu
                    6 => "#F39C12", // En réparation Orange
                    7 => "#E74C3C", // HS Rouge
                    _ => "#2ECC71"  // Disponible Vert
                };
            }
        }

        public string Commentaire
        {
            get
            {
                return this.commentaire;
            }

            set
            {
                this.commentaire = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparationAffichage affichage &&
                   this.NumMateriel == affichage.NumMateriel &&
                   this.Reference == affichage.Reference &&
                   this.NomMateriel == affichage.NomMateriel &&
                   this.Descriptif == affichage.Descriptif &&
                   this.NumEtat == affichage.NumEtat &&
                   this.Libelletype == affichage.Libelletype &&
                   this.Libellecategorie == affichage.Libellecategorie &&
                   this.Libelleetat == affichage.Libelleetat &&
                   this.CouleurStatut == affichage.CouleurStatut;
        }
    }
}
