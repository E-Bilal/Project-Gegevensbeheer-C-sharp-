using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Eindwerk__Gegevensbeheer__en_C_sharp.PBKDF;
using Microsoft.EntityFrameworkCore;
using pbkdf_basic_demo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for AddKlant.xaml
    /// </summary>
    public partial class AddPersoon : Window
    {

        private static AppDbContext _context = new AppDbContext();
        public Delegate UpdatePersoon;
        

        public AddPersoon()
        {
            InitializeComponent();


            if(Keuze.Persoon == "Klant" || Keuze.Persoon == "Mecanicien")
            {
                sp_Voornaam.Visibility = Visibility.Visible;
                sp_Achternaam.Visibility = Visibility.Visible;

                //Bypass Naam Validatie
                naamTxt.Text = "bypass";
            }

            else if (Keuze.Persoon == "Leverancier")
            {
                sp_Naam.Visibility = Visibility.Visible;

                //Bypass Naam Validatie
                voornaamTxt.Text = "bypass";
                achternaamTxt.Text = "bypass";
            }

            else if (Keuze.Persoon == "UpdateMecanicien")
            {
                sp_Voornaam.Visibility = Visibility.Visible;
                sp_Achternaam.Visibility = Visibility.Visible;

                //Bypass Naam Validatie
                naamTxt.Text = "bypass";

                voornaamTxt.Text = App.UpdateMecanicien[1];
                achternaamTxt.Text = App.UpdateMecanicien[2];
                emailTxt.Text = App.UpdateMecanicien[3];
                gsmTxt.Text = App.UpdateMecanicien[4];
            }

        }

        private void PersoonToevoegen(object sender, RoutedEventArgs e)
        {


            if ((voornaamTxt.Text.Length == 0 || achternaamTxt.Text.Length == 0) || (naamTxt.Text.Length == 0))
            {
                errorTxt.Text = "Namen mogen niet leeg zijn.";
                return;
            }

            if ( (!voornaamTxt.Text.All(char.IsLetter) || !achternaamTxt.Text.All(char.IsLetter)))
            {
                errorTxt.Text = "Namen mogen alleen letters bevatten.";
                return;
            }

            //Email Validation
            var email = new EmailAddressAttribute();
            if (!email.IsValid(emailTxt.Text))
            {
                //MessageBox.Show("Please enter a valid email address");
                errorTxt.Text = "Gelieve een geldige e-mailadres in te geven.";
                emailTxt.Text = "";
                return;
            }

            if (!gsmTxt.Text.All(char.IsDigit) || gsmTxt.Text.Length != 9)
            {
                errorTxt.Text = "Telefoonnummer mag alleen nummers bevatten en moet ook 9 nummers bevatten.";
                return;
            }

            if (Keuze.Persoon == "Klant") 
            {

                (byte[] cipher, byte[] IV) encryptedEmail = Crypto.Encrypt(Encoding.UTF8.GetBytes(emailTxt.Text), App.GarageWachtwoordByte);
                (byte[] cipher, byte[] IV) encryptedTelefoonNummer = Crypto.Encrypt(Encoding.UTF8.GetBytes(gsmTxt.Text), App.GarageWachtwoordByte);

                var klant = new Klant
                    (
                        voornaamTxt.Text,
                        achternaamTxt.Text,
                        encryptedTelefoonNummer.cipher,
                        encryptedTelefoonNummer.IV,
                        encryptedEmail.cipher,
                        encryptedEmail.IV
                    );

                _context.Add(klant);


            }

            else if (Keuze.Persoon == "Leverancier")
            {
                var leverancier = new Leverancier(naamTxt.Text, gsmTxt.Text, emailTxt.Text);
                _context.Add(leverancier);
            }

            else if (Keuze.Persoon == "Mecanicien")
            {
                var mecanicien = new Mecanicien(voornaamTxt.Text, achternaamTxt.Text, gsmTxt.Text, emailTxt.Text);
                _context.Add(mecanicien);
            }

            else if (Keuze.Persoon == "UpdateMecanicien")
            {
                var updatemecanicien = _context.Mecaniciens.Where(c => c.Id == int.Parse(App.UpdateMecanicien[0])).Single();
                updatemecanicien.Voornaam = voornaamTxt.Text;
                updatemecanicien.Achternaam = achternaamTxt.Text;
                updatemecanicien.Email = emailTxt.Text;
                updatemecanicien.TelefoonNummer = gsmTxt.Text;
                
            }

            _context.SaveChanges();
            UpdatePersoon.DynamicInvoke(); /*this will call the `RefreshListView` method of mainWindow*/
            this.Close();

        }
    }
}
