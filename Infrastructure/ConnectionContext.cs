using LearnApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnApi.Infrastructure;

public class ConnectionContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public ConnectionContext() { }

    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}