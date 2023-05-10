using Eindwerk__Gegevensbeheer__en_C_sharp.Pages;
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

namespace Eindwerk__Gegevensbeheer__en_C_sharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            listNav.ItemsSource = new List<NavButton>
                {
                    new NavButton("Voorraad", new Voorraad()),

                };
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void listNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //frame.Content = ((NavButton)listNav.SelectedItem).Page;
            var link = ((NavButton)listNav.SelectedItem).Name;

            if (link.Contains(" "))
            {
                link = link.Replace(" ", "");
            }
            frame.Source = new System.Uri($"/Pages/{link}.xaml", UriKind.Relative);
        }
    }
}
