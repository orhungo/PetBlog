using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace petblog.Models {
    public class MyAppContext:DbContext 
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(

                "Server=172.21.54.253;Database=PetBlog;User=ogr_131930072;Password=!nif.ogr72OR;",
                new MySqlServerVersion(new Version(8, 0, 40)) 


            );
        }
    }
}