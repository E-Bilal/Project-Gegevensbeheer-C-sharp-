using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class Auto
    {

        public int Id { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public int Bouwjaar { get; set; }
        public float Prijs { get; set; }
        public int Voorraad { get; set; }


        public string? EditStack { get; set; } = "Collapsed";
        public string? ButtonStack { get; set; }
        public string? PrijsTextbox { get; set; }
        public string? PrijsTextbox2 { get; set; }
        public string? PrijsVisibility { get; set; }

        public Auto(string merk, string model, int bouwjaar, float prijs, int voorraad)
        {
            Merk = merk;
            Model = model;
            Bouwjaar = bouwjaar;
            Prijs = prijs;
            Voorraad = voorraad;
        }

        public override string ToString()
        {
            return $"{Merk} {Model} - {Bouwjaar}";
        }

    }
}
