using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
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
using System.Windows.Shapes;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for AddReparatie.xaml
    /// </summary>
    public partial class AddReparatie : Window
    {
        private static AppDbContext _context = new AppDbContext();
        public Delegate UpdatePersoon;

        private static int KlantofLeverancierId;
        private static int OnderdeelofAutoId;


        public AddReparatie()
        {
            InitializeComponent();
            soort_box.Items.Add("Reparatie");
            soort_box.Items.Add("Nazicht");
            klant_box.ItemsSource = _context.Klanten.ToList();
            mecanicien_box.ItemsSource = _context.Mecaniciens.ToList();

        }

        private void Bestellen(object sender, RoutedEventArgs e)
        {


            if (soort_box.SelectedItem == null)
            {
                errorTxt.Text = "Gelieve de soort reparatie te selecteren";
                return;
            }

            if(klant_box.SelectedItem == null)
            {
                errorTxt.Text = "Gelieve een klant te selecteren";
                return;
            }

            if (mecanicien_box.SelectedItem == null)
            {
                errorTxt.Text = "Gelieve een mecanicien te selecteren";
                return;
            }

            if (!ramingTxt.Text.All(char.IsDigit) || ramingTxt.Text.Length == 0)
            {
                errorTxt.Text = "Raming mag alleen getallen bevatten";
                return;
            }

            if (int.Parse(ramingTxt.Text) <= 0 || int.Parse(ramingTxt.Text) > 10)
            {

                errorTxt.Text = "Raming moet tussen 1 tem 10 dagen zijn";
                return;
            }

            Reparatie reparatie = new Reparatie();
            reparatie.Klant = _context.Klanten.Where(x => x.Id == ((Klant)klant_box.SelectedItem).Id).Single();
            reparatie.Mecanicien = _context.Mecaniciens.Where(x => x.Id == ((Mecanicien)mecanicien_box.SelectedItem).Id).Single();
            reparatie.Datum = DateTime.Now;

            reparatie.Soort = soort_box.SelectedItem.ToString();
            reparatie.Raming = int.Parse(ramingTxt.Text);

            _context.Add(reparatie);
            _context.SaveChanges();

            UpdatePersoon.DynamicInvoke(); /*this will call the `RefreshListView` method of mainWindow*/
            this.Close();


        }

        //private void klant_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (klant_box.SelectedItem != null)
        //    {
        //        KlantofLeverancierId = ((Klant)klant_box.SelectedItem).Id;
        //    }

        //}
        //private void soort_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}


    }
}
