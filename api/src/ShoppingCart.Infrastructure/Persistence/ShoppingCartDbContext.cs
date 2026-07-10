using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain;

namespace ShoppingCart.Infrastructure.Persistence;

public class ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : DbContext(options)
{
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
}
