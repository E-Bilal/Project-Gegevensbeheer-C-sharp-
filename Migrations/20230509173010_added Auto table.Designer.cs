﻿// <auto-generated />
using System;
using Eindwerk__Gegevensbeheer__en_C_sharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230509173010_added Auto table")]
    partial class addedAutotable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Eindwerk__Gegevensbeheer__en_C_sharp.Models.Auto", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Bouwjaar")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Merk")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Prijs")
                        .HasColumnType("REAL");

                    b.Property<int>("Voorraad")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Autos");
                });

            modelBuilder.Entity("Eindwerk__Gegevensbeheer__en_C_sharp.Models.Klant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("Eindwerk__Gegevensbeheer__en_C_sharp.Models.Product", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Prijs")
                        .HasColumnType("REAL");

                    b.Property<int>("Voorraad")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Producten");
                });
#pragma warning restore 612, 618
        }
    }
}
