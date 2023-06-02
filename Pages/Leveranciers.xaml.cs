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
    /// Interaction logic for Leveranciers.xaml
    /// </summary>
    public partial class Leveranciers : Page
    {
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;

        public Leveranciers()
        {
            InitializeComponent();

            if (App.Rol == "Viewer")
            {
                sp_OpenAddLeverancier.Visibility = Visibility.Hidden;
            }
            using (var db = new AppDbContext())
            {
                lb_Leveranciers.ItemsSource = db.Leveranciers.ToList();
            }
        }
        private void RefreshListView()
        {
            using (var db = new AppDbContext())
            {
                lb_Leveranciers.ItemsSource = db.Leveranciers.ToList();
            }
        }

        private void OpenLeverancier(object sender, RoutedEventArgs e)
        {
            Keuze.Persoon = "Leverancier";
            AddPersoon main = new AddPersoon();
            RefreshListEvent += new RefreshList(RefreshListView); // event initialization

            main.UpdatePersoon = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void DeleteLeverancier(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Alle bestellingen van deze Leverancier worden ook verwijderd. Ben je zeker?", "Bevestiging", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var rowItem = (sender as Button).DataContext as Leverancier;

                using (var db = new AppDbContext())
                {
                    var deleteLeverancier = db.Leveranciers.Where(d => d.Id == rowItem.Id).Include(e => e.Bestelling).Single();
                    db.Leveranciers.Remove(deleteLeverancier);
                    db.SaveChanges();
                    lb_Leveranciers.ItemsSource = db.Leveranciers.ToList();
                }
            }
        }
    }
}
