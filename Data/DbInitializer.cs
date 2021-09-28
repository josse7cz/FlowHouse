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

            var zakaznici = new Zakaznik[]
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
            foreach (Zakaznik s in zakaznici)
            {
                context.Zakaznici.Add(s);
            }
            context.SaveChanges();

            var polozky = new Polozka[]
            {
            new Polozka{Nazev="rohlik",Cena=3,Pocet=2 },
            new Polozka{Nazev="chleba",Cena=3,Pocet=2 },
            new Polozka{Nazev="makarony",Cena=3,Pocet=2 },
            new Polozka{Nazev="cibule",Cena=4,Pocet=2 },
            new Polozka{Nazev="turistický salám",Cena=4,Pocet=2 },
            new Polozka{Nazev="kotevník",Cena=3,Pocet=2 },
            new Polozka{Nazev="lilek",Cena=4,Pocet=2 }
            };
            foreach (Polozka c in polozky)
            {
                context.Polozky.Add(c);
            }
            context.SaveChanges();

            var nakupy = new Nakup[]
            {
           
            new Nakup{ZakaznikID=1,PolozkaID=1},
            new Nakup{ZakaznikID=1,PolozkaID=0},
            new Nakup{ZakaznikID=1,PolozkaID=4},
            new Nakup{ZakaznikID=2,PolozkaID=1},
            new Nakup{ZakaznikID=2,PolozkaID=3},
            new Nakup{ZakaznikID=2,PolozkaID=2},
            new Nakup{ZakaznikID=3,PolozkaID=1},
            new Nakup{ZakaznikID=4,PolozkaID=1},
            new Nakup{ZakaznikID=4,PolozkaID=4},
            new Nakup{ZakaznikID=5,PolozkaID=4},
            new Nakup{ZakaznikID=6,PolozkaID=1},
            new Nakup{ZakaznikID=7,PolozkaID=3},
            };
            foreach (Nakup e in nakupy)
            {
                context.Nakupy.Add(e);
            }
            context.SaveChanges();
        }
    }
}
