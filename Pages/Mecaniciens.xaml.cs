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
    /// Interaction logic for Mecaniciens.xaml
    /// </summary>
    public partial class Mecaniciens : Page
    {
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        public Mecaniciens()
        {
            InitializeComponent();
            if (App.Rol == "Viewer")
            {
                sp_OpenAddMecanicien.Visibility = Visibility.Hidden;
            }

            RefreshListView();
        }
      
        private void RefreshListView()
        {
            using (var db = new AppDbContext())
            {
                lb_Mecaniciens.ItemsSource = db.Mecaniciens.ToList();
            }
        }



        private void OpenAddMecanicienWindow(object sender, RoutedEventArgs e)
        {
            Keuze.Persoon = "Mecanicien";
            AddPersoon main = new AddPersoon();
            RefreshListEvent += new RefreshList(RefreshListView); // event initialization

            main.UpdatePersoon = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void OpenUpdateMecanicienWindow(object sender, RoutedEventArgs e)
        {
            Keuze.Persoon = "UpdateMecanicien";
            var rowItem = (sender as Button).DataContext as Mecanicien;
            RefreshListEvent += new RefreshList(RefreshListView);

            App.UpdateMecanicien = new string[] { rowItem.Id.ToString(), rowItem.Voornaam, rowItem.Achternaam, rowItem.Email, rowItem.TelefoonNummer };
            AddPersoon main = new AddPersoon();

            main.UpdatePersoon = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void DeleteMecanicien(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as Mecanicien;

            using (var db = new AppDbContext())
            {
                var deleteMecanicien = db.Mecaniciens.Where(d => d.Id == rowItem.Id).Single();
                var reparatiesMecanicien = db.Reparaties.Where(d => d.MecanicienId == rowItem.Id).SingleOrDefault();
                if (reparatiesMecanicien == null)
                {
                    db.Mecaniciens.Remove(deleteMecanicien);
                    db.SaveChanges();
                }

                else MessageBox.Show("Deze mecanicien heeft nog niet alle reparaties/nazicht voltooid. Mecanicien verwijderen is niet mogelijk");

                lb_Mecaniciens.ItemsSource = db.Mecaniciens.ToList();

            } 
        }





    }
}