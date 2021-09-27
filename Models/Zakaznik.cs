using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowHouse.Models
{
    public class Zakaznik
    {
        public int ZakaznikID { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public DateTime DatumRegistrace { get; set; }

        public ICollection<Nakup> Nakupy{ get; set; }
    }
}
