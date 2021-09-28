using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowHouse.Models
{
    public class Pobocka
    {

        public int PobockaID { get; set; }
        [Key]
        public int ProdavacID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Prodavac Prodavac { get; set; }
    }
}
