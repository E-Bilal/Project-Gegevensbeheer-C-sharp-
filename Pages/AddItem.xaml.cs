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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {

        public Delegate UpdateVoorraad;

        private static AppDbContext _context = new AppDbContext();

        public AddItem()
        {
            InitializeComponent();
            if (KeuzeVoorraad.keuze == "Onderdeel")
            {
                naamOfModel.Text = "Naam";
                sp_Bouwjaar.Visibility = Visibility.Collapsed;
                sp_Model.Visibility = Visibility.Collapsed;
            }

            else naamOfModel.Text = "Merk";

        }


        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (KeuzeVoorraad.keuze == "Onderdeel")
            {
                var item = new Onderdeel(nameTxt.Text, float.Parse(prijsTxt.Text +","+ prijs2Txt.Text), int.Parse(voorraadTxt.Text));
                _context.Add(item);
            }
            
            else
            {
                var item = new Auto(nameTxt.Text, modelTxt.Text, int.Parse(bouwjaarTxt.Text), float.Parse(prijsTxt.Text + "," + prijs2Txt.Text), int.Parse(voorraadTxt.Text));
            }

            _context.SaveChanges();

            UpdateVoorraad.DynamicInvoke(); /*this will call the `RefreshListView` method of mainWindow*/
            this.Close();

        }
    }
}
