using Microsoft.EntityFrameworkCore;
using PlantillaBack.Entities.Artists;
using PlantillaBack.Entities.Roles;
using PlantillaBack.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Repositories
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Rol> Roles { get; set; }

        //? Si queremos que nuestra tabla Users tenga una Foreign Key hacemos su relación en la clase "DataBaseContext"
        //? y lo hacemos creando un método de relación entre nuestra tabla User y el campo que queramos relacionar (ROL)


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Rol);
        }
    }
}
