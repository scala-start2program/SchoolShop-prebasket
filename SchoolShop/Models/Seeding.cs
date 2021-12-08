using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SchoolShopContext>>()))
            {
                if (!context.Category.Any() )
                {
                    context.Category.AddRange(
                        new Category { CategoryName = "Schooltassen" },
                        new Category { CategoryName = "Pennenzakken" },
                        new Category { CategoryName = "Schriften" },
                        new Category { CategoryName = "Mappen" }
                        );
                    context.SaveChanges();
                }
                if(!context.Brand.Any())
                { 
                    context.Brand.AddRange(
                        new Brand { BrandName = "Atoma" },
                        new Brand { BrandName = "Oxford" },
                        new Brand { BrandName = "Kangaro" },
                        new Brand { BrandName = "Leitz" }
                        );
                    context.SaveChanges();
                }
                if (!context.Article.Any())
                {
                    context.Article.AddRange(
                        new Article
                        {
                            BrandId = 2,
                            CategoryId = 1,
                            ArticleName = "Eastpak",
                            Price = 40.5M,
                            Description = "De Eastpak Out of Office is een populaire laptoprugzak uit de Authentic serie van Eastpak. Eastpak. Deze rugzak is geschikt voor dagelijks gebruik en zeer geschikt als boekentas. De tas heeft een groot hoofdvak met daarin een laptopvak van 29 x 34 x 4 cm geschikt voor een 14” laptop. Daarnaast is er voldoende ruimte voor A4 formaat documenten. Op de voorzijde van de tas zit een groot ritsvak voor kleinere spullen, zoals een etui, rekenmachine, telefoon etc. De tas heeft een groot draagcomfort door de dik gefoamde (verstelbare) schouderbanden en het zacht gevoerde rugpaneel.",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 2,
                            CategoryId = 1,
                            ArticleName = "Micmacbags Friendship Rugtas Camel",
                            Price = 109.55M,
                            Description = "Rugtas uit de Friendship serie van Micmacbags. Ruime tas met intern rits- en steekvak zeer geschikt voor dagelijks gebruik. Mee naar je werk of gewoon een dag weg. De Friendship serie heeft een leuke tashanger die doet denken aan de vriendschapsbandjes die je vroeger zelf knoopte voor je vriendinnen. De tas sluit met een overslag met gesp die je gemakkelijk in het slot schuift. Het hoofdcompartiment sluit nog extra met een rits en de opening kun je groter of kleiner maken met drukknopen aan beide zijkanten. ",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 3,
                            CategoryId = 1,
                            ArticleName = "Bagbase Vintage ",
                            Price = 32M,
                            Description = "100% polyester (600D). Handgreep, gevoerde verstelbare schouderriemen. PU accenten in contrasterende kleur bruin(behalve black / black). Gevoerde achterkant. Koordsluiting. Gevoerd laptop compartiment, laptop tot 17. Klep met magneetsluiting. ",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 1,
                            CategoryId = 2,
                            ArticleName = "Eberhard Faber ",
                            Price = 28.9M,
                            Description = "Mooie glitter etui voor school met 3 lagen. Gevuld met alle belangrijke schoolbenodigdheden: van puntenslijper tot kleurpotlood in uw favoriete kleur. 34 delig; Inhoud: 12 kleurpotloden, 2 grafietpotloden HB, 4 inktcartridges, 1 puntenslijper, 1 liniaal, lengte: 17 cm, 1 tijdschema, 1 driehoek liniaal, 1 notitieblok, 2 paperclips, 2 gum doppen, 1 UHU-lijmstift 8,2 g. Kleur: Rosé goud.",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 1,
                            CategoryId = 2,
                            ArticleName = "Miss Melody ",
                            Price = 36.95M,
                            Description = "Om bij weg te dromen! De paarse etui van Miss Melody heeft een LED-functie: met een druk op de knop gaan de veren in de manen van Miss Melody en ook sommige vuurvliegjes in het gras branden (circa 18 seconden). De drie aparte vakken zijn gevuld met kleurpotloden, viltstiften, potloden, een liniaal, puntenslijper, gum, schaar, plakstift en een geheim vakje.",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 2,
                            CategoryId = 2,
                            ArticleName = "Avocado",
                            Price = 36.95M,
                            Description = "Een etui om van te smullen: Hip Etui/Pennenzak met veel ruimte voor je schrijfgerief en een avocadomotief om van te watertanden",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 3,
                            CategoryId = 3,
                            ArticleName = "Kangaro Schrift A4 Gelinieerd",
                            Price = 3.59M,
                            Description = "Dit zijn ideale schriften voor op kantoor, op school of voor thuis. Dit Kangaro schrift is gelinieerd, spiraalgebonden en is voorzien van 80 pagina's.",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 2,
                            CategoryId = 3,
                            ArticleName = "Oxford School - Schoolschrift - A4 - Lijn",
                            Price = 3.59M,
                            Description = "Oxford Schoolschriften, voor al je huiswerk, aantekeningen of samenvattingen! Een voordeelpak met daarin drie Oxford A4 schriften gelijnd. Ideaal voor het maken van aantekeningen tijdens de les en je huiswerk. Dit pak bevat drie schriften, twee zwarte en een turquoise. De schriften bevatten 36 vellen met een kantlijn en het 90 grams Optik Paper. Door het Oxford Optik Paper is het papier gegarandeerd van goede kwaliteit, hierdoor kun je op beide zijdes van het papier schrijven zonder doordrukken.",
                            Score = 5M
                        },
                        new Article
                        {
                            BrandId = 4,
                            CategoryId = 4,
                            ArticleName = "Leitz Active Leitz WOW 4-rings ringband - blauw",
                            Price = 11.49M,
                            Description = "Zeer duurzaam en robuust. Ideaal voor in schooltassen. Gepatenteerd SoftClick-mechanisme voor eenvoudig openen en veilig afsluiten. Binnenhoezen voor het bewaren van papier, cd's en visitekaartjes. Ronde rug en pennenhouder voor gebruiksvriendelijkheid onderweg. Capaciteit: 280 A4 - vellen 80 gram. 3 jaar garantie op het mechanisme.",
                            Score = 5M
                        }
                    );
                    context.SaveChanges();

                }
            }
        }
    }

}
