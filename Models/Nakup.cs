using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FlowHouse.Models
{
    public class Nakup
    {
        public int NakupID { get; set; }
        
        public int PolozkaID { get; set; }
        public int ZakaznikID { get; set; }
        public int ProdavacID { get; set; }
        //public Grade? Grade { get; set; }https://docs.microsoft.com/cs-cz/aspnet/core/data/ef-mvc/intro?view=aspnetcore-5.0
        [DisplayFormat(NullDisplayText = "Bez ceny!")]
        public int? Cena { get; set; }
        public Polozka Polozka { get; set; }
        public Zakaznik Zakaznik { get; set; }

        public Prodavac Prodavac { get; set; }
    }
}