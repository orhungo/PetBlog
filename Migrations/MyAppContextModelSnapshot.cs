﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using petblog.Models;

#nullable disable

namespace PetBlog.Migrations
{
    [DbContext(typeof(MyAppContext))]
    partial class MyAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Login.Models.Kullanici", b =>
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

            modelBuilder.Entity("petblog.Models.Kayit", b =>
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
#pragma warning restore 612, 618
        }
    }
}
