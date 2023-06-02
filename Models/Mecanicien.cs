using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class Mecanicien
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string TelefoonNummer { get; set; }

        public string Email { get; set; }

        //Navigation
        public virtual ICollection<Reparatie> Reparatie{ get; set; }

        //Visibility
        public string? Visibility
        {
            get
            {
                if (App.Rol == "Viewer")
                {
                    return "Hidden";
                }
                else return "Visible";
            }
        }


        public Mecanicien(string voornaam, string achternaam, string telefoonNummer, string email)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            TelefoonNummer = telefoonNummer;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Achternaam} {Voornaam}";
        }
    }

}
