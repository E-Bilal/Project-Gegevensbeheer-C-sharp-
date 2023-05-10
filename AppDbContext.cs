using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp
{
    public class AppDbContext:DbContext
    {
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Onderdeel> Onderdelen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source= GarageShop.db");
    }
}
