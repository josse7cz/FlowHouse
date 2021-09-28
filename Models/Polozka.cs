using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FlowHouse.Models
{
    public class Polozka
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int PolozkaID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Název")]
        public string Nazev { get; set; }
        public int Cena { get; set; }
        public int Pocet { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumNaskladneni { get; set; }

        public int OddeleniID { get; set; }

        public Oddeleni Oddeleni { get; set; }

        public ICollection<Nakup> Nakupy { get; set; }
        public ICollection<ProdejZadani> ProdejZadanis { get; set; }
    }
}