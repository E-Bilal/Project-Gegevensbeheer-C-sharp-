using Eindwerk__Gegevensbeheer__en_C_sharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Eindwerk__Gegevensbeheer__en_C_sharp
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Onderdeel> Onderdelen { get; set; }
        public DbSet<BestellingM> Bestellingen { get; set; }
        public DbSet<Leverancier> Leveranciers { get; set; }
        public DbSet<Mecanicien> Mecaniciens { get; set; }

        public DbSet<Reparatie> Reparaties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Onderdeel>().Ignore(c => c.EditStack);
            modelBuilder.Entity<Onderdeel>().Ignore(c => c.ButtonStack);
            modelBuilder.Entity<Onderdeel>().Ignore(c => c.PrijsTextbox);
            modelBuilder.Entity<Onderdeel>().Ignore(c => c.PrijsTextbox2);
            modelBuilder.Entity<Onderdeel>().Ignore(c => c.PrijsVisibility);

            modelBuilder.Entity<Auto>().Ignore(c => c.EditStack);
            modelBuilder.Entity<Auto>().Ignore(c => c.ButtonStack);
            modelBuilder.Entity<Auto>().Ignore(c => c.PrijsTextbox);
            modelBuilder.Entity<Auto>().Ignore(c => c.PrijsTextbox2);
            modelBuilder.Entity<Auto>().Ignore(c => c.PrijsVisibility);

            modelBuilder.Entity<Klant>().Ignore(c => c.DecryptedEmail);
            modelBuilder.Entity<Klant>().Ignore(c => c.DecryptedTelefoonNummer);
            modelBuilder.Entity<Klant>().Ignore(c => c.DeleteVisibility);

            modelBuilder.Entity<Leverancier>().Ignore(c => c.DeleteVisibility);

            modelBuilder.Entity<Mecanicien>().Ignore(c => c.Visibility);

            modelBuilder.Entity<BestellingM>().Ignore(c => c.Product);
            modelBuilder.Entity<BestellingM>().Ignore(c => c.Naam);

            modelBuilder.Entity<Reparatie>().Ignore(c => c.KlantenNaam);
            modelBuilder.Entity<Reparatie>().Ignore(c => c.MecanicienNaam);
            modelBuilder.Entity<Reparatie>().Ignore(c => c.TimeFormatted);
            modelBuilder.Entity<Reparatie>().Ignore(c => c.Visibility);


            modelBuilder.Entity<BestellingM>().HasOne(e => e.Klant)
            .WithMany(d => d.Bestelling)
            .HasForeignKey(e => e.KlantId)
            .OnDelete(DeleteBehavior.Cascade);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=GarageShop.db");
    }
}
