﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using petblog.Models;

#nullable disable

namespace PetBlog.Migrations
{
    [DbContext(typeof(MyAppContext))]
    [Migration("20250103133818_TestMigration")]
    partial class TestMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PetBlog.Models.Kayit", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ad")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("sifre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("soyad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("userID");

                    b.ToTable("kayitli");
                });

            modelBuilder.Entity("PetBlog.Models.Kullanici", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("sifre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("userID");

                    b.ToTable("kullanicilar");
                });
#pragma warning restore 612, 618
        }
    }
}
