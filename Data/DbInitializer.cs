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

            //var polozky = new Polozka[]
            //{
            //new Polozka{Nazev="rohlik",Cena=3,Pocet=2 },
            //new Polozka{Nazev="chleba",Cena=3,Pocet=2 },
            //new Polozka{Nazev="makarony",Cena=3,Pocet=2 },
            //new Polozka{Nazev="cibule",Cena=4,Pocet=2 },
            //new Polozka{Nazev="turistický salám",Cena=4,Pocet=2 },
            //new Polozka{Nazev="kotevník",Cena=3,Pocet=2 },
            //new Polozka{Nazev="lilek",Cena=4,Pocet=2 }
            //};
            //foreach (Polozka c in polozky)
            //{
            //    context.Polozky.Add(c);
            //}
            //context.SaveChanges();

            //var nakupy = new Nakup[]
            //{
           
            //new Nakup{ZakaznikID=1,PolozkaID=1},
            //new Nakup{ZakaznikID=1,PolozkaID=0},
            //new Nakup{ZakaznikID=1,PolozkaID=4},
            //new Nakup{ZakaznikID=2,PolozkaID=1},
            //new Nakup{ZakaznikID=2,PolozkaID=3},
            //new Nakup{ZakaznikID=2,PolozkaID=2},
            //new Nakup{ZakaznikID=3,PolozkaID=1},
            //new Nakup{ZakaznikID=4,PolozkaID=1},
            //new Nakup{ZakaznikID=4,PolozkaID=4},
            //new Nakup{ZakaznikID=5,PolozkaID=4},
            //new Nakup{ZakaznikID=6,PolozkaID=1},
            //new Nakup{ZakaznikID=7,PolozkaID=3},
            //};
            //foreach (Nakup e in nakupy)
            //{
            //    context.Nakupy.Add(e);
            //}
            //context.SaveChanges();



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
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni== "Abercrombie").ID },
                new Oddeleni { Jmeno = "Maso", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni == "Fakhouri").ID },
                new Oddeleni {Jmeno = "Pecivo", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni== "Harui").ID },
                new Oddeleni { Jmeno = "Drogerie",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    ProdavacID  = prodavaci.Single( i => i.Prijmeni == "Kapoor").ID }
           };

            foreach (Oddeleni o in oddeleni)
            {
                context.Oddelenis.Add(o);
            }
            context.SaveChanges();




            var polozky = new Polozka[]
            {
                new Polozka {Nazev = "Rohlík", Cena = 2,
                    OddeleniID = oddeleni.Single( s => s.Jmeno == "Pecivo").OddeleniID
                },
                new Polozka { Nazev = "Svíčková", Cena = 200,Pocet=5,
                    PolozkaID = oddeleni.Single( s => s.Jmeno == "Maso").OddeleniID
                },
                            };

            foreach (Polozka c in polozky)
            {
                context.Polozky.Add(c);
            }
            context.SaveChanges();



            var pobocky = new Pobocka[]
            {
                new Pobocka {
                    ProdavacID = prodavaci.Single( i => i.Prijmeni== "Fakhouri").ID,
                    Location = "Smith 17" },
                new Pobocka {
                    ProdavacID = prodavaci.Single( i => i.Prijmeni == "Harui").ID,
                    Location = "Gowan 27" },
                new Pobocka {
                    ProdavacID = prodavaci.Single( i => i.Prijmeni == "Kapoor").ID,
                    Location = "Thompson 304" },
            };

            foreach (Pobocka o in pobocky)
            {
                context.Pobocky.Add(o);
            }
            context.SaveChanges();




            var prodejZadani = new ProdejZadani[]
           {
                new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "Svíčková" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Kapoor").ID
                    },new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "Rohlík" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Harui").ID
                    },new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "Rohlík" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Harui").ID
                    },
                     new ProdejZadani {
                    PolozkaID = polozky.Single(c => c.Nazev == "Svíčková" ).PolozkaID,
                    ProdavacID = prodavaci.Single(i => i.Prijmeni == "Kapoor").ID
                    },
                
                
           };

            foreach (ProdejZadani ci in prodejZadani)
            {
                context.ProdejZadanis.Add(ci);
            }
            context.SaveChanges();



            var nakupy = new Nakup[]
            {
                new Nakup {
                    ZakaznikID = zakaznici.Single(s => s.Prijmeni == "Alexander").ZakaznikID,
                    PolozkaID = polozky.Single(c => c.Nazev == "Svíčková" ).PolozkaID,
                    Cena = 100
                },
                    new Nakup {
                    ZakaznikID = zakaznici.Single(s => s.Prijmeni == "Prosos").ZakaznikID,
                    PolozkaID = polozky.Single(c => c.Nazev == "Rohlík" ).PolozkaID,
                    Cena = 100
                },
                    new Nakup {
                    ZakaznikID = zakaznici.Single(s => s.Prijmeni == "Ferdinand").ZakaznikID,
                    PolozkaID = polozky.Single(c => c.Nazev == "Rohlik" ).PolozkaID,
                    Cena = 100
                },

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
