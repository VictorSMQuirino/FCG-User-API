using FCG_Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG_Users.Infrastructure.Data;

public class FCGUsersDbContext(DbContextOptions<FCGUsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FCGUsersDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
