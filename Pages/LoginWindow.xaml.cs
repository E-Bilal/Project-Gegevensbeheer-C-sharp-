using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Eindwerk__Gegevensbeheer__en_C_sharp.PBKDF;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Pages
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        private static AppDbContext _context = new AppDbContext();


        public LoginWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Create a timer with interval of 3 secs
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            var user_exists = _context.Users.Where(u => u.Gebruikersnaam.ToUpper() == gebruikersnaamTxt.Text.ToUpper()).FirstOrDefault();
            if (user_exists == null)
            {
                errorTxt.Text = "Ongeldige Gegevens";
                dispatcherTimer.Start();
                return;
            }

            if (!BCrypt.Net.BCrypt.Verify(wachtwoordTxt.Password.ToString(), user_exists.Wachtwoord))
            {
                errorTxt.Text = "Ongeldige Gegevens";
                dispatcherTimer.Start();
                return;
            }

            if (!Crypto.GenerateKeyFromPassword(garageWachtwoordTxt.Password.ToString(), Salt.salt).SequenceEqual(App.GarageWachtwoordByte))
            {
                errorTxt.Text = "Ongeldige Gegevens";
                dispatcherTimer.Start();
                return;
            }

            //var user_exists = _context.Users.Where(u => u.Gebruikersnaam == "Verkoper").FirstOrDefault();
            //if (user_exists == null)
            //{
            //    errorTxt.Text = "Ongeldige Gegevens";
            //    dispatcherTimer.Start();
            //    return;
            //}

            //if (!BCrypt.Net.BCrypt.Verify("123456789", user_exists.Wachtwoord))
            //{
            //    errorTxt.Text = "Ongeldige Gegevens";
            //    dispatcherTimer.Start();
            //    return;
            //}

            //if (!Crypto.GenerateKeyFromPassword("ABC123", Salt.salt).SequenceEqual(App.GarageWachtwoordByte))
            //{
            //    errorTxt.Text = "Ongeldige Gegevens";
            //    dispatcherTimer.Start();
            //    return;
            //}

            App.Rol = user_exists.Rol;
            var main = new MainWindow();
            main.Show();
            this.Close();
 
            

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            errorTxt.Text = "";
            //Disable the timer
            dispatcherTimer.IsEnabled = false;
        }




    }

}
