using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class Leverancier
    {

        public int Id { get; set; }
        public string Naam { get; set; }
        public string TelefoonNummer { get; set; }
        public string Email { get; set; }

        //Navigation
        public virtual ICollection<BestellingM> Bestelling { get; set; }

        //Visibility
        public string? DeleteVisibility
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

        public Leverancier(string naam, string telefoonNummer, string email)
        {
            Naam = naam;
            TelefoonNummer = telefoonNummer;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Naam}";
        }
    }
}
