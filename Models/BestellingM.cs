using pbkdf_basic_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{

    public class BestellingM
    {

        public int Id { get; set; }
        public int Aantal { get; set; }
        public float TotaalPrijs { get; set; }
        public Klant? Klant { get; set; }
        public Leverancier? Leverancier { get; set; }
        public Onderdeel? Onderdeel{ get; set; }
        public Auto? Auto { get; set; }



        public string? Product { get; set; }
        public string? Naam { get; set; }

        //Navigataion
        public int? KlantId { get; set; }
        public int? LeverancierId { get; set; }
        public int? OnderdeelId { get; set; }
        public int? AutoId { get; set; }



    }
}
