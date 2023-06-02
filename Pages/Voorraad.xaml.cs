using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Voorraad.xaml
    /// </summary>
    public partial class Voorraad : Page
    {
        //private static AppDbContext _context = new AppDbContext();
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;

        public Voorraad()
        {
            InitializeComponent();
            if (App.Rol == "Viewer")
            {

                btn_OndToevoegen.Visibility = Visibility.Hidden;
                btn_AutoToevoegen.Visibility= Visibility.Hidden;
                btn_KlantOndBestelling.Visibility= Visibility.Hidden;
                btn_KlantAutoBestelling.Visibility= Visibility.Hidden;
                btn_LeverancierOndBestelling.Visibility = Visibility.Hidden;
                btn_LeverancierAutoBestelling.Visibility= Visibility.Hidden;

            }

            if (App.Rol == "Verkoper")
            {
                btn_OndToevoegen.Visibility = Visibility.Hidden;
                btn_AutoToevoegen.Visibility= Visibility.Hidden;
            }

        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Visible;
            sp_Auto.Visibility = Visibility.Hidden;
            sp_Onderdeel.Visibility = Visibility.Hidden;
        }

        private void OpenOnderdeel(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                sp_Keuze.Visibility = Visibility.Hidden;
                sp_Onderdeel.Visibility = Visibility.Visible;
                lb_Onderdeel.ItemsSource = db.Onderdelen.ToList();

                if (App.Rol == "Viewer" || App.Rol == "Verkoper")
                {
                    foreach (Onderdeel item in lb_Onderdeel.ItemsSource)
                    {
                        item.ButtonStack = "Hidden";
                    }

                }
            }
        }

        private void OpenAuto(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                sp_Keuze.Visibility = Visibility.Hidden;
                sp_Auto.Visibility = Visibility.Visible;
                lb_Auto.ItemsSource = db.Autos.ToList();

                if (App.Rol == "Viewer" || App.Rol == "Verkoper")
                {
                    foreach (Auto item in lb_Auto.ItemsSource)
                    {
                        item.ButtonStack = "Hidden";
                    }
                }
            }
        }



        private void OpenAddOnderdeelWindow(object sender, RoutedEventArgs e)
        {
            Keuze.ItemToevoegen = "Onderdeel";
            AddItem main = new AddItem();
            RefreshListEvent += new RefreshList(RefreshListViewItemToevoegen); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void OpenAddAutoWindow(object sender, RoutedEventArgs e)
        {
            Keuze.ItemToevoegen = "Auto";
            AddItem main = new AddItem();
            RefreshListEvent += new RefreshList(RefreshListViewItemToevoegen); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }


        private void RefreshListViewItemToevoegen()
        {
            using (var db = new AppDbContext())
            {
                if (Keuze.ItemToevoegen == "Onderdeel")
                {
                    lb_Onderdeel.ItemsSource = db.Onderdelen.ToList();
                }

                else lb_Auto.ItemsSource = db.Autos.ToList();
            }
        }

        private void RefreshListViewBestelling()
        {
            using (var db = new AppDbContext())
            {

                if (Keuze.Bestelling == "OnderdeelKlant" || Keuze.Bestelling == "OnderdeelLeverancier")
                {
                    lb_Onderdeel.ItemsSource = db.Onderdelen.ToList();
                }

                if (Keuze.Bestelling == "AutoKlant" || Keuze.Bestelling == "AutoLeverancier")
                {
                    lb_Auto.ItemsSource = db.Autos.ToList();    
                }                
            }
        }


        private void ShowEditStack(object sender, RoutedEventArgs e)
        {

            var rowItemOnd = (sender as Button).DataContext as Onderdeel;
            if (rowItemOnd != null) 
            {
                rowItemOnd.ButtonStack = "Collapsed";
                rowItemOnd.PrijsVisibility = "Collapsed";
                rowItemOnd.EditStack = "Visible";
                lb_Onderdeel.Items.Refresh();
            }

            else
            {
                var rowItemAuto = (sender as Button).DataContext as Auto;
                rowItemAuto.ButtonStack = "Collapsed";
                rowItemAuto.PrijsVisibility = "Collapsed";
                rowItemAuto.EditStack = "Visible";
                lb_Auto.Items.Refresh();
            }


        }

        private void CancelOnderdeel(object sender, RoutedEventArgs e)
        {

            var rowItemOnd = (sender as Button).DataContext as Onderdeel;
            if (rowItemOnd != null)
            {
                rowItemOnd.ButtonStack = "Visible";
                rowItemOnd.EditStack = "Collapsed";
                rowItemOnd.PrijsVisibility = "Collapsed";
                lb_Onderdeel.Items.Refresh();
            }

            else
            {
                var rowItemAuto = (sender as Button).DataContext as Auto;
                rowItemAuto.ButtonStack = "Visible";
                rowItemAuto.EditStack = "Collapsed";
                lb_Auto.Items.Refresh();
            }
        }

        
        private void UpdateOnderdeel(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var rowItemOnd = (sender as Button).DataContext as Onderdeel;
                if (rowItemOnd != null)
                {

                    if (!rowItemOnd.PrijsTextbox.All(char.IsDigit) || !rowItemOnd.PrijsTextbox2.All(char.IsDigit) || rowItemOnd.PrijsTextbox.Length == 0 || rowItemOnd.PrijsTextbox.Length == 0 )
                    {
                        MessageBox.Show("Voorraad en prijs mogen alleen nummers bevatten.");
                        return;
                    }

                    var updatedPrijs = db.Onderdelen.First(a => a.Id == rowItemOnd.Id);
                    updatedPrijs.Prijs = float.Parse(rowItemOnd.PrijsTextbox + "," + rowItemOnd.PrijsTextbox2);

                    rowItemOnd.ButtonStack = "Visible";
                    rowItemOnd.PrijsVisibility = "Visible";
                    rowItemOnd.EditStack = "Collapsed";

                    db.SaveChanges();
                    lb_Onderdeel.ItemsSource = db.Onderdelen.ToList();
                }

                else
                {
                    var rowItemAuto = (sender as Button).DataContext as Auto;
                    if (!rowItemAuto.PrijsTextbox.All(char.IsDigit) || !rowItemAuto.PrijsTextbox2.All(char.IsDigit) || rowItemAuto.PrijsTextbox.Length == 0 || rowItemAuto.PrijsTextbox.Length == 0)
                    {
                        MessageBox.Show("Voorraad en prijs mogen alleen nummers bevatten.");
                        return;
                    }

                    var updatedPrijs = db.Autos.First(a => a.Id == rowItemAuto.Id);
                    updatedPrijs.Prijs = float.Parse(rowItemAuto.PrijsTextbox + "," + rowItemAuto.PrijsTextbox2);


                    rowItemAuto.ButtonStack = "Visible";
                    rowItemAuto.EditStack = "Collapsed";

                    db.SaveChanges();
                    lb_Auto.ItemsSource = db.Autos.ToList();
                }
            }
        }

        private void KlantBestellingOnd(object sender, RoutedEventArgs e)
        {
            Keuze.Bestelling = "OnderdeelKlant";
            AddBestelling main = new AddBestelling();
            RefreshListEvent += new RefreshList(RefreshListViewBestelling); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void LeverancierBestellingOnd(object sender, RoutedEventArgs e)
        {
            Keuze.Bestelling = "OnderdeelLeverancier";
            AddBestelling main = new AddBestelling();
            RefreshListEvent += new RefreshList(RefreshListViewBestelling); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void KlantBestellingAuto(object sender, RoutedEventArgs e)
        {
            Keuze.Bestelling = "AutoKlant";
            AddBestelling main = new AddBestelling();
            RefreshListEvent += new RefreshList(RefreshListViewBestelling); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void LeverancierBestellingAuto(object sender, RoutedEventArgs e)
        {
            Keuze.Bestelling = "AutoLeverancier";
            AddBestelling main = new AddBestelling();
            RefreshListEvent += new RefreshList(RefreshListViewBestelling); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }


    }
}
