using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCEFOpdracht.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<ComponentContext>
    {
        protected override void Seed(ComponentContext context)
        {
            base.Seed(context);

            context.Componenten.Add(new Component()
            {
                Categorie = "Passief",
                Naam = "Schakelaar",
                DatasheetLink = "C:\\schakelaars.xls",
                Aankoopprijs = 10,
                Aantal = 50
            });

            context.Componenten.Add(new Component()
            {
                Categorie = "Actief",
                Naam = "Transistor",
                DatasheetLink = "C:\\transistors.xls",
                Aankoopprijs = 20,
                Aantal = 30
            });
            

            

            context.SaveChanges();
        }
 

    }
}