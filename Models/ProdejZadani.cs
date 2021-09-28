using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowHouse.Models
{
    public class ProdejZadani
    {
        public int ProdavacID { get; set; }
        public int PolozkaID { get; set; }
        public Prodavac Prodavac { get; set; }
        public Polozka Polozka { get; set; }
    }
}
