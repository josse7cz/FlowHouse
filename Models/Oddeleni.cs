using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowHouse.Models
{
    public class Oddeleni
    {
        public int OddeleniID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Jmeno { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? ProdavacID { get; set; }

        public Prodavac Administrator { get; set; }
        public ICollection<Polozka> Polozky { get; set; }
    }
}
