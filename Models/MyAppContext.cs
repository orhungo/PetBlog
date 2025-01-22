using PetBlog.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace petblog.Models {
    public class MyAppContext : DbContext 
    {
        public DbSet<Kullanici> kullanicilar {get; set;}
        public DbSet<Kayit> kayitli {get; set;}
        public DbSet<Pet> petler {get; set;}
        public DbSet<Yonetici> Yoneticiler {get; set;}=null!;
        public DbSet<Blog> Bloglar {get; set;} 
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=172.21.54.253;Database=PetBlog;User=ogr_131930072;Password=!nif.ogr72OR;",
                new MySqlServerVersion(new Version(8, 0, 40)),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                )
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Yonetici>().HasData(
                new Yonetici
                {
                    yoneticiId = 1,
                    kullaniciAdi = "admin",
                    sifre = "123456",
                    eposta = "admin@admin.com"
                }
            );
        }
    }
}