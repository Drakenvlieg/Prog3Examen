using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEFOpdracht.Models
{
    public class Component
    {
        public int ID { get; set; }
        public string Categorie { get; set; }
        [Required]
        public string Naam { get; set; }
        public string DatasheetLink { get; set; }
        public double? Aankoopprijs { get; set; }
        public int? Aantal { get; set; }
    }
}