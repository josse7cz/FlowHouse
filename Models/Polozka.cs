using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace FlowHouse.Models
{
    public class Polozka
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PolozkaID { get; set; }
        public string Nazev { get; set; }
        public int Cena { get; set; }
        public int Pocet { get; set; }
        public DateTime DatumNaskladneni { get; set; }

        public ICollection<Nakup> Nakupy { get; set; }
    }
}