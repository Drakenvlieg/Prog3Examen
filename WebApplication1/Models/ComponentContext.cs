using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCEFOpdracht.Models
{
    public class ComponentContext: DbContext
    {
            
        public ComponentContext()
        {

            //opvullen met testdata:
            Database.SetInitializer<ComponentContext>( new DataInitializer());

        }

         public DbSet<Component> Componenten { get; set; }


    }
}
