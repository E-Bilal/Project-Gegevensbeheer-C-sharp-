using BCrypt.Net;
using Eindwerk__Gegevensbeheer__en_C_sharp.PBKDF;
using pbkdf_basic_demo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eindwerk__Gegevensbeheer__en_C_sharp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {       
        public static byte[] GarageWachtwoordByte = Crypto.GenerateKeyFromPassword("ABC123", Salt.salt);
        public static string Rol;
        public static string[] UpdateMecanicien = new string[5];
    }
}
