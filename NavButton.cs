﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Eindwerk__Gegevensbeheer__en_C_sharp
{
    public class NavButton
    {
        public string Name { get; set; }
        public Page Page { get; set; }


        public NavButton(string name, Page page)
        {

            Name = name;
            Page = page;
        }


        public override string ToString()
        {
            return $"{Page}";
        }
    }
}
