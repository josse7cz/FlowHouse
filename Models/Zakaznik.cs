using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlowHouse.Models
{
    public class Zakaznik
    {
        public int ZakaznikID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Jméno")]
        public string Jmeno { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Příjmení")]
        public string Prijmeni { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumRegistrace { get; set; }


        [Display(Name = "Celé jméno")]
        public string FullName
        {
            get
            {
                return Jmeno + ", " + Prijmeni;
            }
        }



        public ICollection<Nakup> Nakupy{ get; set; }
    }
}
