using Microsoft.EntityFrameworkCore;
using SmartPool.Models;


    public class PoolContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public PoolContext(DbContextOptions options) : base(options) { }

            public DbSet<User> Users {get;set;}
            public DbSet<Commute> Commutes {get;set;}
            public DbSet<Carpool> Carpools {get;set;}
            public DbSet<Location> Locations {get;set;}
<<<<<<< HEAD
=======
            public DbSet<Ridership> Riderships {get;set;}
>>>>>>> 515cf0b4be585ec6a23e142d9f3bf3739bfb4429

    }
