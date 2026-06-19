using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Informe> Informes { get; set; }
}
