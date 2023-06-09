﻿
using pbkdf_basic_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class Onderdeel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public float Prijs { get; set; }
        public int Voorraad { get; set; }

        public string? EditStack { get; set; } = "Collapsed";
        public string? ButtonStack { get; set; }


        public string? PrijsTextbox { get; set; }
        public string? PrijsTextbox2 { get; set; }
        public string? PrijsVisibility { get; set; }


        public Onderdeel(string name, float prijs, int voorraad)
        {
            Name = name;
            Prijs = prijs;
            Voorraad = voorraad;
        }

        public override string ToString()
        {
            return $"{Name} - {Prijs} - Voorrraad : {Voorraad}";
        }
    }
}
