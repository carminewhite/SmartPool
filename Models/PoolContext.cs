using Microsoft.EntityFrameworkCore;

public class PoolContext : DbContext
{
    // base() calls the parent class' constructor passing the "options" parameter along
    public PoolContext(DbContextOptions options) : base(options) { }
}