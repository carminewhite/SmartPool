using Microsoft.EntityFrameworkCore;
using SmartPool.Models;

namespace SmartPool.Models
{
    public class PoolContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public PoolContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
    }
}