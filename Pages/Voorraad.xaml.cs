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
    /// Interaction logic for Voorraad.xaml
    /// </summary>
    public partial class Voorraad : Page
    {
        private static AppDbContext _context = new AppDbContext();

        
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;

        public Voorraad()
        {
            InitializeComponent();
        }

        private void OpenOnderdeel(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Hidden;
            sp_Onderdeel.Visibility = Visibility.Visible;
            lb_Onderdeel.ItemsSource = _context.Onderdelen.ToList();
        }

        private void OpenAuto(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Hidden;
            sp_Onderdeel.Visibility = Visibility.Visible;
            lb_Auto.ItemsSource = _context.Autos.ToList();
            lb_Onderdeel.ItemsSource = _context.Onderdelen.ToList();
        }



        private void OpenAddOnderdeelWindow(object sender, RoutedEventArgs e)
        {
            KeuzeVoorraad.keuze = "Onderdeel";
            AddItem main = new AddItem();
            RefreshListEvent += new RefreshList(RefreshListView); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }

        private void OpenAddAutoWindow(object sender, RoutedEventArgs e)
        {
            KeuzeVoorraad.keuze = "Auto";
            AddItem main = new AddItem();
            RefreshListEvent += new RefreshList(RefreshListView); // event initialization

            main.UpdateVoorraad = RefreshListEvent; // assigning event to the Delegate
            main.Show();
        }


        private void RefreshListView()
        {
            if (KeuzeVoorraad.keuze == "Onderdeel")
            {
                lb_Onderdeel.ItemsSource = _context.Onderdelen.ToList();
            }

            else lb_Auto.ItemsSource= _context.Autos.ToList();
            
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            sp_Keuze.Visibility = Visibility.Visible;
            sp_Auto.Visibility = Visibility.Hidden;
            sp_Onderdeel.Visibility = Visibility.Hidden;
        }
    }
}
