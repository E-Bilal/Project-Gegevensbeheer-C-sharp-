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

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for Reparaties.xaml
    /// </summary>
    public partial class Reparaties : Page
    {
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        public Reparaties()
        {
            InitializeComponent();
            if (App.Rol == "Viewer")
            {
                sp_OpenAddReparatie.Visibility = Visibility.Hidden;
            }
            RefreshListView();

        }

        private void RefreshListView()
        {
            using (var db = new AppDbContext())
            {
                lb_Reparaties.ItemsSource = db.Reparaties.ToList();
                foreach (Reparatie reparatie in lb_Reparaties.ItemsSource)
                {
                    var klantnaam = db.Klanten.Where(a => a.Id == reparatie.KlantId).SingleOrDefault();
                    reparatie.KlantenNaam = klantnaam.Achternaam + " " + klantnaam.Voornaam;

                    var levnaam = db.Mecaniciens.Where(a => a.Id == reparatie.MecanicienId).SingleOrDefault();
                    reparatie.MecanicienNaam = levnaam.Achternaam + " " + levnaam.Voornaam;

                    reparatie.TimeFormatted = reparatie.Datum.ToString("yyyy-MM-dd");
                }
            }
        }


        private void OpenAddReparatie(object sender, RoutedEventArgs e)
        {
            Keuze.Persoon = "Leverancier";
            AddReparatie main = new AddReparatie();
            RefreshListEvent += new RefreshList(RefreshListView); // event initialization

            main.UpdatePersoon = RefreshListEvent; // assigning event to the Delegate
            main.Show();

        }

        private void ReparatieVoltooid(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as Reparatie;
            using (var db = new AppDbContext())
            {
                var deleteReparatie = db.Reparaties.Where(d => d.Id == rowItem.Id).Single();
                db.Reparaties.Remove(deleteReparatie);
                db.SaveChanges();
                RefreshListView();


            }

        }


    }
}
