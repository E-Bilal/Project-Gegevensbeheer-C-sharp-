using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for Registratie.xaml
    /// </summary>
    public partial class Registratie : Page
    {
        public Registratie()
        {
            InitializeComponent();
            rol_box.Items.Add("User");
            rol_box.Items.Add("Verkoper");
            rol_box.Items.Add("Viewer");
        }

        private void Registreren(object sender, RoutedEventArgs e)
        {
            errorTxt.Text = "";

            if (gebruikersnaamTxt.Text.Length < 5)
            {
                errorTxt.Text = "Gebruikersnaam moet minstens 5 karakters bevatten";
                return;
            }

            if (wachtwoordTxt.Password.Length < 5)
            {
                errorTxt.Text = "Wachtwoord moet minstens 5 karakters bevatten";
                return;
            }

            if (wachtwoordTxt.Password.ToString() != confirmWachtwoordTxt.Password.ToString())
            {
                errorTxt.Text = "Wachtwoorden moeten hetzelfde zijn";
                return;
            }

            if (rol_box.SelectedItem == null)
            {
                errorTxt.Text = "Gelieve een rol te selecteren";
                return;
            }

            using (var db = new AppDbContext())
            {
                var user_exists = db.Users.Where(u => u.Gebruikersnaam.ToUpper() == gebruikersnaamTxt.Text.ToUpper()).FirstOrDefault();
                if (user_exists != null)
                {
                    errorTxt.Text = "Gebruikernaam bestaal al.";
                    return;
                }
                var user = new User(gebruikersnaamTxt.Text, BCrypt.Net.BCrypt.HashPassword(wachtwoordTxt.Password.ToString()), rol_box.SelectedItem.ToString());
                db.Add(user);
                db.SaveChanges();
                MessageBox.Show("Gebruiker is geregistreerd");

            }
        }



    }                
}

