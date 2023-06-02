using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for Bestelling.xaml
    /// </summary>
    public partial class Bestelling : Page
    {
        private static AppDbContext _context = new AppDbContext();
        public Delegate UpdateVoorraad;
        public Bestelling()
        {
            InitializeComponent();
        }


        private void GoBack(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Visible;
            sp_Klanten.Visibility = Visibility.Hidden;
            sp_Leveranciers.Visibility = Visibility.Hidden;
        }

        private void OpenKlanten(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Hidden;
            sp_Klanten.Visibility = Visibility.Visible;
            lb_KlantenBestelling.Items.Clear();

            foreach (BestellingM bestelling in _context.Bestellingen.ToList())
            {
                if (bestelling.KlantId != null)
                {
                    lb_KlantenBestelling.Items.Add(bestelling);
                    if (bestelling.OnderdeelId == null)
                    {

                        bestelling.Product = _context.Autos.Where(a => a.Id == bestelling.AutoId).Single().Model;
                    }
                    else
                    {
                        bestelling.Product = _context.Onderdelen.Where(a => a.Id == bestelling.OnderdeelId).Single().Name;
                    }
                var naam = _context.Klanten.Where(a => a.Id == bestelling.KlantId).SingleOrDefault();
                bestelling.Naam = naam.Achternaam + " " + naam.Voornaam;
                }
            }
        }

        private void OpenLeveranciers(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Hidden;
            sp_Leveranciers.Visibility = Visibility.Visible;
            lb_LeveranciersBestelling.Items.Clear();
            

            foreach (BestellingM bestelling in _context.Bestellingen.ToList())
            {
                if (bestelling.LeverancierId != null)
                {

                    lb_LeveranciersBestelling.Items.Add(bestelling);
                    if (bestelling.OnderdeelId == null)
                    {

                        bestelling.Product = _context.Autos.Where(a => a.Id == bestelling.AutoId).Single().Model;
                    }
                    else
                    {
                        bestelling.Product = _context.Onderdelen.Where(a => a.Id == bestelling.OnderdeelId).Single().Name;
                    }
                    var naam = _context.Leveranciers.Where(a => a.Id == bestelling.LeverancierId).SingleOrDefault();
                    bestelling.Naam = naam.Naam;
                }
            }
        }



    }
}
