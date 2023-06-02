using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
using pbkdf_basic_demo;
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

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for Klanten.xaml
    /// </summary>
    public partial class Klanten : Page
    {

        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;


        public Klanten()
        {
            InitializeComponent();
            if(App.Rol == "Viewer")
            {
                sp_OpenAddKlant.Visibility = Visibility.Hidden;
            }

            using (var db = new AppDbContext())
            {
                lb_Klanten.ItemsSource = db.Klanten.ToList();
            }
        }

        private void RefreshListView()
        {
            using (var db = new AppDbContext())
            {
                lb_Klanten.ItemsSource = db.Klanten.ToList();
            }
        }


        private void OpenAddKlantenWindow(object sender, RoutedEventArgs e)
        {
            Keuze.Persoon = "Klant";
            AddPersoon main = new AddPersoon();
            RefreshListEvent += new RefreshList(RefreshListView); // event initialization

            main.UpdatePersoon = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }
        private void DeleteKlant(object sender, RoutedEventArgs e)
        {

            MessageBoxResult messageBoxResult = MessageBox.Show("Alle bestellingen van deze klant worden ook verwijderd. Ben je zeker?", "Bevestiging", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var rowItem = (sender as Button).DataContext as Klant;

                using (var db = new AppDbContext())
                {
                    var deleteKlant = db.Klanten.Where(d => d.Id == rowItem.Id).Include(e => e.Bestelling).Single();
                    var reparatiesKlant = db.Reparaties.Where(d => d.KlantId == rowItem.Id).SingleOrDefault();
                    if (reparatiesKlant == null)
                    {
                        db.Klanten.Remove(deleteKlant);
                        db.SaveChanges();
                    }

                    else MessageBox.Show("Reparatie(s) van deze klant nog niet klaar. Klant verwijderen is niet mogelijk");
                    
                    lb_Klanten.ItemsSource = db.Klanten.ToList();
                }
            }
        }

    }
}
