using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for AddBestelling.xaml
    /// </summary>
    public partial class AddBestelling : Window
    {
        private static AppDbContext _context = new AppDbContext();
        public Delegate UpdateVoorraad;

        private static int KlantofLeverancierId;
        private static int OnderdeelofAutoId;


        public AddBestelling()
        {
            InitializeComponent();


            if (Keuze.Bestelling.Contains("Klant"))
            {
                klantOfLeverancierTxt.Text = "Klant";
                klant_box.Visibility = Visibility.Visible;
                klant_box.ItemsSource = _context.Klanten.ToList();
            }

            else if (Keuze.Bestelling.Contains("Leverancier"))
            {
                klantOfLeverancierTxt.Text = "Leverancier";
                leverancier_box.Visibility = Visibility.Visible;
                leverancier_box.ItemsSource = _context.Leveranciers.ToList();

            }

            if (Keuze.Bestelling.Contains("Onderdeel"))
            {
                OnderdeelofAutoTxt.Text = "Onderdeel";
                onderdeel_box.ItemsSource = _context.Onderdelen.ToList();
                onderdeel_box.Visibility = Visibility.Visible;
            }


            else if (Keuze.Bestelling.Contains("Auto"))
            {
                OnderdeelofAutoTxt.Text = "Auto";
                auto_box.ItemsSource = _context.Autos.ToList();
                auto_box.Visibility = Visibility.Visible;
            }
        }

        private void Bestellen(object sender, RoutedEventArgs e)
        {
            BestellingM bestelling = new BestellingM();
            string klantofLeverancier  = null;


            //Zien of dat er een  klant is geselecteerd.
            if (Keuze.Bestelling.Contains("Klant"))
            {
                if (klant_box.SelectedItem == null)
                {
                    errorTxt.Text = "Gelieve een Klant te selecteren";
                    return;
                }

                bestelling.Klant = _context.Klanten.Where(x => x.Id == KlantofLeverancierId).Single();
                klantofLeverancier = "Klant";
            }

            //Zien of dat er een leverancier is geselecteerd.
            else if (Keuze.Bestelling.Contains("Leverancier"))
            {
                if(leverancier_box.SelectedItem == null)
                {
                    errorTxt.Text = "Gelieve een Leverancier te selecteren";
                    return;
                }

                bestelling.Leverancier = _context.Leveranciers.Where(x => x.Id == KlantofLeverancierId).Single();
                klantofLeverancier = "Leverancier";
                
            }
            //Zien of het ingegeven aantal alleen nummers bevat en niet leeg is.
            if (!aantalTxt.Text.All(char.IsDigit) || aantalTxt.Text.Length <1 )
            {
                errorTxt.Text = "Aantal mag alleen nummers bevatten.";
                return;
            }


            //Bij bestellen onderdeel zien of dat er iets geselecteerd is en dat het ingegeven aantal kleiner is dan wat er in voorraad is. Daarna wordt de voorraad van Onderdelen geupdated.
            if (Keuze.Bestelling.Contains("Onderdeel"))
            {
                if(onderdeel_box.SelectedItem == null)
                {
                    errorTxt.Text = "Gelieve een onderdeel te selecteren";
                    return;
                }

                if (int.Parse(aantalTxt.Text) > ((Onderdeel)onderdeel_box.SelectedItem).Voorraad && klantofLeverancier == "Klant")
                {
                    errorTxt.Text = "Er is niet genoeg in voorraad.";
                    return;
                }


                if (int.Parse(aantalTxt.Text) + ((Onderdeel)onderdeel_box.SelectedItem).Voorraad > 900 && klantofLeverancier == "Leverancier")
                {
                    errorTxt.Text = "Er is genoeg in voorraad. Bestellingen die leiden tot 900+ in voorraad worden niet uitgevoerd";
                    return;
                }


                bestelling.Onderdeel = _context.Onderdelen.Where(x => x.Id == OnderdeelofAutoId).Single();

                var updateVoorraadOnderdeel = _context.Onderdelen.Where(x => x.Id == OnderdeelofAutoId).Single();

                if (klantofLeverancier == "Klant")
                {
                    updateVoorraadOnderdeel.Voorraad = updateVoorraadOnderdeel.Voorraad - int.Parse(aantalTxt.Text);
                }

                else
                {
                    updateVoorraadOnderdeel.Voorraad = updateVoorraadOnderdeel.Voorraad + int.Parse(aantalTxt.Text);
                }
                
            }
            //Bij bestellen Auto zien of dat er iets geselecteerd is en dat het ingegeven aantal kleiner is dan wat er in voorraad is. Daarna wordt de voorraad van Autos geupdated.
            else if (Keuze.Bestelling.Contains("Auto"))
            {
                if (auto_box.SelectedItem == null)
                {
                    errorTxt.Text = "Gelieve een Auto te selecteren";
                    return;
                }

                if (int.Parse(aantalTxt.Text) > ((Auto)auto_box.SelectedItem).Voorraad && klantofLeverancier == "Klant")
                {
                    errorTxt.Text = "Er is niet genoeg in voorraad.";
                    return;
                }


                if (int.Parse(aantalTxt.Text) + ((Auto)auto_box.SelectedItem).Voorraad > 900 && klantofLeverancier == "Leverancier")
                {
                    errorTxt.Text = "Er is genoeg in voorraad. Bestellingen die leiden tot 10+ in voorraad worden niet uitgevoerd";
                    return;
                }

                bestelling.Auto = _context.Autos.Where(x => x.Id == OnderdeelofAutoId).Single();

                var updateVoorraadAuto = _context.Autos.Where(x => x.Id == OnderdeelofAutoId).Single();
                if (klantofLeverancier == "Klant")
                {
                    updateVoorraadAuto.Voorraad = updateVoorraadAuto.Voorraad - int.Parse(aantalTxt.Text);
                }

                else
                {
                    updateVoorraadAuto.Voorraad = updateVoorraadAuto.Voorraad + int.Parse(aantalTxt.Text);
                }
            }

            bestelling.Aantal = int.Parse(aantalTxt.Text);
            bestelling.TotaalPrijs = float.Parse(totaalPrijsTxt.Text);

            _context.Add(bestelling);
            _context.SaveChanges();

            UpdateVoorraad.DynamicInvoke(); /*this will call the `RefreshListView` method of mainWindow*/
            this.Close();


        }

        private void klant_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (klant_box.SelectedItem != null)
            {
                KlantofLeverancierId = ((Klant)klant_box.SelectedItem).Id;                
            }
                
        }

        private void leverancier_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (leverancier_box.SelectedItem != null)
            {
                KlantofLeverancierId = ((Leverancier)leverancier_box.SelectedItem).Id;
            }
        }

        private void onderdeel_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (onderdeel_box.SelectedItem != null)
            {
                OnderdeelofAutoId = ((Onderdeel)onderdeel_box.SelectedItem).Id;
            }

            if (aantalTxt.Text.Length > 0 && aantalTxt.Text.All(char.IsDigit) && onderdeel_box.SelectedItem != null)
            {
                totaalPrijsTxt.Text = (int.Parse(aantalTxt.Text) * ((Onderdeel)onderdeel_box.SelectedItem).Prijs).ToString();
            }
        }

        private void auto_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (auto_box.SelectedItem != null)
            {
                OnderdeelofAutoId = ((Auto)auto_box.SelectedItem).Id;
            }


            if (aantalTxt.Text.Length > 0 && aantalTxt.Text.All(char.IsDigit) && auto_box.SelectedItem != null)
            {
                totaalPrijsTxt.Text = (int.Parse(aantalTxt.Text) * ((Auto)auto_box.SelectedItem).Prijs).ToString();
            }
        }

        private void aantalTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(aantalTxt.Text.Length > 0 && aantalTxt.Text.All(char.IsDigit) && onderdeel_box.SelectedItem != null)
            {
                totaalPrijsTxt.Text = (int.Parse(aantalTxt.Text) * ((Onderdeel)onderdeel_box.SelectedItem).Prijs).ToString();
            }

            else if (aantalTxt.Text.Length > 0 && aantalTxt.Text.All(char.IsDigit) && auto_box.SelectedItem != null)
            {
                totaalPrijsTxt.Text = (int.Parse(aantalTxt.Text) * ((Auto)auto_box.SelectedItem).Prijs).ToString();
            }
        }
    }
}
