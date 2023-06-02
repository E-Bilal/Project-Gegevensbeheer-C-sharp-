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
        private static AppDbContext _context = new AppDbContext();
        public Delegate UpdateVoorraad;

        public AddItem()
        {
            InitializeComponent();
            if (Keuze.ItemToevoegen == "Onderdeel")
            {
                naamOfModel.Text = "Naam";
                sp_Bouwjaar.Visibility = Visibility.Collapsed;
                sp_Model.Visibility = Visibility.Collapsed;
                prijsTxt.MaxLength = 4;
            }

            else 
            { 
                naamOfModel.Text = "Merk";
                prijsTxt.MaxLength = 5;
            }            
        }


        


        private void Toevoegen(object sender, RoutedEventArgs e)
        {

            
            if (Keuze.ItemToevoegen == "Onderdeel")
            {
                if (nameTxt.Text.Length == 0)
                {
                    errorTxt.Text = "Naam mag niet leeg zijn.";
                    return;
                }

                if (!voorraadTxt.Text.All(char.IsDigit) || !prijsTxt.Text.All(char.IsDigit) || !prijs2Txt.Text.All(char.IsDigit) || voorraadTxt.Text.Length == 0 || prijsTxt.Text.Length == 0 || prijs2Txt.Text.Length == 0)
                {
                    errorTxt.Text = "Voorraad en prijs mogen alleen nummers bevatten.";
                    return;
                }

                var item = new Onderdeel(nameTxt.Text, float.Parse(prijsTxt.Text +","+ prijs2Txt.Text), int.Parse(voorraadTxt.Text));
                _context.Add(item);
            }
            
            else
            {
                if (naamOfModel.Text.Length == 0 || modelTxt.Text.Length == 0) 
                {
                    errorTxt.Text = "Model en merk mogen niet leeg zijn.";
                    return;
                }

                if (!bouwjaarTxt.Text.All(char.IsDigit) || bouwjaarTxt.Text.Length != 4)
                {
                    errorTxt.Text = "Bouwjaar mag alleen nummers bevatten en moet ook 4 nummers bevatten.";
                    return;
                }

                if (!voorraadTxt.Text.All(char.IsDigit) || !prijsTxt.Text.All(char.IsDigit) || !prijs2Txt.Text.All(char.IsDigit) || voorraadTxt.Text.Length == 0 || prijsTxt.Text.Length == 0 || prijs2Txt.Text.Length == 0)
                {
                    errorTxt.Text = "Voorraad en prijs mogen alleen nummers bevatten.";
                    return;
                }


                var item = new Auto(nameTxt.Text, modelTxt.Text, int.Parse(bouwjaarTxt.Text), float.Parse(prijsTxt.Text + "," + prijs2Txt.Text), int.Parse(voorraadTxt.Text));
                _context.Add(item);
            }

            _context.SaveChanges();
            UpdateVoorraad.DynamicInvoke(); /*this will call the `RefreshListView` method of mainWindow*/
            this.Close();

        }
    }
}
