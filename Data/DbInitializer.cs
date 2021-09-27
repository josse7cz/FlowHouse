using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowHouse.Models;

namespace FlowHouse.Data
{
    public class DbInitializer
    {
        public static void Initialize(SkladContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Zakazniks.
            if (context.Zakaznici.Any())
            {
                return;   // DB has been seeded
            }

            var Zakazniks = new Zakaznik[]
            {
            new Zakaznik{Jmeno="Carson",Prijmeni="Alexander",DatumRegistrace=DateTime.Parse("2005-09-01")},
            new Zakaznik{Jmeno="Meredith",Prijmeni="Alonso",DatumRegistrace=DateTime.Parse("2002-09-01")},
            new Zakaznik{Jmeno="Arturo",Prijmeni="Anand",DatumRegistrace=DateTime.Parse("2003-09-01")},
            new Zakaznik{Jmeno="Gytis",Prijmeni="Barzdukas",DatumRegistrace=DateTime.Parse("2002-09-01")},
            new Zakaznik{Jmeno="Yan",Prijmeni="Li",DatumRegistrace=DateTime.Parse("2002-09-01")},
            new Zakaznik{Jmeno="Peggy",Prijmeni="Justice",DatumRegistrace=DateTime.Parse("2001-09-01")},
            new Zakaznik{Jmeno="Laura",Prijmeni="Norman",DatumRegistrace=DateTime.Parse("2003-09-01")},
            new Zakaznik{Jmeno="Nino",Prijmeni="Olivetto",DatumRegistrace=DateTime.Parse("2005-09-01")}
            };
            foreach (Zakaznik s in Zakazniks)
            {
                context.Zakaznici.Add(s);
            }
            context.SaveChanges();

            var polozky = new Polozka[]
            {
            new Polozka{PolozkaID=1050,Nazev="Chemistry",Cena=3},
            new Polozka{PolozkaID=4022,Nazev="Microeconomics",Cena=3},
            new Polozka{PolozkaID=4041,Nazev="Macroeconomics",Cena=3},
            new Polozka{PolozkaID=1045,Nazev="Calculus",Cena=4},
            new Polozka{PolozkaID=3141,Nazev="Trigonometry",Cena=4},
            new Polozka{PolozkaID=2021,Nazev="Composition",Cena=3},
            new Polozka{PolozkaID=2042,Nazev="Literature",Cena=4}
            };
            foreach (Polozka c in polozky)
            {
                context.Polozky.Add(c);
            }
            context.SaveChanges();

            var nakupy = new Nakup[]
            {
            new Nakup{ZakaznikID=1,PolozkaID=1050},
            new Nakup{ZakaznikID=1,PolozkaID=4022},
            new Nakup{ZakaznikID=1,PolozkaID=4041},
            new Nakup{ZakaznikID=2,PolozkaID=1045},
            new Nakup{ZakaznikID=2,PolozkaID=3141},
            new Nakup{ZakaznikID=2,PolozkaID=2021},
            new Nakup{ZakaznikID=3,PolozkaID=1050},
            new Nakup{ZakaznikID=4,PolozkaID=1050},
            new Nakup{ZakaznikID=4,PolozkaID=4022},
            new Nakup{ZakaznikID=5,PolozkaID=4041},
            new Nakup{ZakaznikID=6,PolozkaID=1045},
            new Nakup{ZakaznikID=7,PolozkaID=3141},
            };
            foreach (Nakup e in nakupy)
            {
                context.Nakupy.Add(e);
            }
            context.SaveChanges();
        }
    }
}
