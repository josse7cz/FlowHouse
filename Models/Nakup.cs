namespace FlowHouse.Models
{
    public class Nakup
    {
        public int NakupID { get; set; }
        public int PolozkaID { get; set; }
        public int ZakaznikID { get; set; }
        //public Grade? Grade { get; set; }https://docs.microsoft.com/cs-cz/aspnet/core/data/ef-mvc/intro?view=aspnetcore-5.0

        public Polozka Polozka { get; set; }
        public Zakaznik Zakaznik { get; set; }
    }
}