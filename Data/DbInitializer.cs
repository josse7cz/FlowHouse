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

      
            var prodavaci = new Prodavac[]
            {
                new Prodavac { Jmeno = "Kim",     Prijmeni = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11")},
                new Prodavac { Jmeno = "Fadi",    Prijmeni = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Prodavac { Jmeno = "Roger",   Prijmeni = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Prodavac { Jmeno = "Candace", Prijmeni = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Prodavac { Jmeno = "Roger",   Prijmeni = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Prodavac i in prodavaci)
            {
                context.Prodavaci.Add(i);
            }
            context.SaveChanges();

            var oddeleni = new Oddeleni[]
           {
                new Oddeleni { Jmeno = "Hracky",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni== "Abercrombie").ProdavacID },
                new Oddeleni { Jmeno = "Maso", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni == "Fakhouri").ProdavacID },
                new Oddeleni {Jmeno = "Pecivo", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni== "Harui").ProdavacID },
                new Oddeleni { Jmeno = "Drogerie",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni == "Kapoor").ProdavacID },
                new Oddeleni { Jmeno = "Zelenia",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni == "Zheng").ProdavacID }

           };

            foreach (Oddeleni o in oddeleni)
            {
                context.Departments.Add(o);
            }
            context.SaveChanges();


            var pobocky = new Pobocka[]
            {
                new Pobocka {
                    ProdavacID = prodavaci.Single( i => i.Prijmeni== "Fakhouri").ProdavacID,
                    Location = "Brno" },
                new Pobocka {
                    ProdavacID = prodavaci.Single( i => i.Prijmeni == "Harui").ProdavacID,
                    Location = "Praha" },
                new Pobocka {
                    ProdavacID = prodavaci.Single( i => i.Prijmeni == "Kapoor").ProdavacID,
                    Location = "HradecKrálové" },
            };

            foreach (Pobocka o in pobocky)
            {
                context.Pobocky.Add(o);
            }
            context.SaveChanges();



            var prodejZadani = new ProdejZadani[]
           {
                new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "rohlik" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Kapoor").ProdavacID
                    },new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "chleba" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Harui").ProdavacID
                    },new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "makarony" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Harui").ProdavacID
                    },
                     new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "cibule" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Kapoor").ProdavacID
                    },
           };

            foreach (ProdejZadani ci in prodejZadani)
            {
                context.ProdejAssigments.Add(ci);
            }
            context.SaveChanges();



            var nakupy = new Nakup[]
            {
                new Nakup {
                    ZakaznikID = zakaznici.Single(s => s.Prijmeni == "Justice").ZakaznikID,
                    PolozkaID = polozky.Single(c => c.Nazev == "rohlik" ).PolozkaID,
                    ProdavacID=prodavaci.Single(q=> q.Prijmeni == "Harui" ).ProdavacID,
                    Cena = 100
                },
                    new Nakup {
                    ZakaznikID = zakaznici.Single(s => s.Prijmeni == "Justice").ZakaznikID,
                    PolozkaID = polozky.Single(c => c.Nazev == "rohlik" ).PolozkaID,
                    ProdavacID=prodavaci.Single(q=> q.Prijmeni == "Harui" ).ProdavacID,
                    Cena = 100
                },
                    new Nakup {
                    ZakaznikID = zakaznici.Single(s => s.Prijmeni == "Justice").ZakaznikID,
                    PolozkaID = polozky.Single(c => c.Nazev == "rohlik" ).PolozkaID,
                    ProdavacID=prodavaci.Single(q=> q.Prijmeni == "Harui" ).ProdavacID,
                    Cena = 100
              }
            };

            foreach (Nakup e in nakupy)
            {
                var enrollmentInDataBase = context.Nakupy.Where(
                    s =>
                            s.Zakaznik.ZakaznikID == e.ZakaznikID &&
                            s.Polozka.PolozkaID == e.PolozkaID).SingleOrDefault();
                if (enrollmentInDataBase == null)
               {
                context.Nakupy.Add(e);
                }
            }
            context.SaveChanges();






        }
    }
}
