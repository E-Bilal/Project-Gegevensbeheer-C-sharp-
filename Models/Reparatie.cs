using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class Reparatie
    {
        public int Id { get; set; }
        public int Raming { get; set; }
        public string Soort { get; set; }
        public DateTime Datum { get; set; }
        public Mecanicien? Mecanicien { get; set; }
        public Klant? Klant { get; set; }

        public string? KlantenNaam { get; set; }
        public string? MecanicienNaam { get; set; }
        public string? TimeFormatted { get; set; }


        //Visibility
        public string? Visibility
        {   get
            {
                if (App.Rol == "Viewer")
                {
                    return "Hidden";
                }
                else return "Visible";
            }
        }
        //Navigataion
        public int? MecanicienId { get; set; }
        public int? KlantId { get; set; }

    }
}
