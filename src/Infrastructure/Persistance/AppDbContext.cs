using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Drink> Drinks { get; init; }
    public DbSet<Brand> Brands { get; init; }
    public DbSet<Coin> Coins { get; init; }
    public DbSet<CartItem> CartItems { get; init; }
}
