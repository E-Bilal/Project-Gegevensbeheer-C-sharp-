using Eindwerk__Gegevensbeheer__en_C_sharp.Pages;
using Eindwerk__Gegevensbeheer__en_C_sharp.PBKDF;
using pbkdf_basic_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Models
{
    public class Klant
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public byte[] TelefoonNummer { get; set; }
        public byte[] Email { get; set; }
        public byte[] IVEmail { get; set; }
        public byte[] IVTelefoonNummer { get; set; }

        //Navigation
        public virtual ICollection<BestellingM> Bestelling { get; set; }
        public virtual ICollection<Reparatie> Reparatie { get; set; }


        //Decryption
        public string DecryptedEmail
        {
            get
            {
                return Encoding.UTF8.GetString(Crypto.Decrypt(Email, App.GarageWachtwoordByte, IVEmail));
            }
        }
        public string DecryptedTelefoonNummer
        {
            get
            {
                return Encoding.UTF8.GetString(Crypto.Decrypt(TelefoonNummer, App.GarageWachtwoordByte, IVTelefoonNummer));
            }
        }


        //Visibility
        public string? DeleteVisibility { get 
            {
                if (App.Rol == "Viewer")
                {
                    return "Hidden";
                }
                else return "Visible";
            } 
        }

        public Klant(string voornaam, string achternaam, byte[] telefoonNummer, byte[] iVTelefoonNummer, byte[] email, byte[] iVEmail)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            TelefoonNummer = telefoonNummer;
            IVTelefoonNummer = iVTelefoonNummer;
            Email = email;
            IVEmail = iVEmail;
        }

        public override string ToString()
        {
            return $"{Achternaam} {Voornaam}";
        }
    }
}
