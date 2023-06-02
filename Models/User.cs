using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Rol { get; set; }

        public User(string gebruikersnaam, string wachtwoord, string rol)
        {
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Rol = rol;
        }
    }
}
