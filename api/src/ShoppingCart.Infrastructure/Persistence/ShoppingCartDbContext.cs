using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Infrastructure.Persistence;

public class ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : DbContext(options)
{
}