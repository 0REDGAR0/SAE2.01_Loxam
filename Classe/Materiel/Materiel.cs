using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE2._01_Loxam.Classe.Materiel
{
    public class Materiel
    {
        public int NumMateriel { get; set; }
        public int NumEtat { get; set; }
        public int NumType { get; set; }
        public string Reference { get; set; }
        public string NomMateriel { get; set; }
        public string Descriptif { get; set; }
        public decimal PrixJournee { get; set; }
    }
}
