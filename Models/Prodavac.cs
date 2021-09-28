using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowHouse.Models
{
    public class Prodavac
    {
        public int ProdavacID { get; set; }

        [Required]
        [Display(Name = "Příjmení")]
        [StringLength(50)]
        public string Prijmeni { get; set; }

        [Required]
        [Column("Jméno")]
        [Display(Name = "Jméno")]
        [StringLength(50)]
        public string Jmeno { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string CeleJmeno
        {
            get { return Prijmeni + ", " + Jmeno; }
        }

        public ICollection<ProdejZadani> ProdejZadani { get; set; }
        public Pobocka Pobocka { get; set; }

        public Oddeleni Oddeleni { get; set; }
    }
}
