using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Persistence;

namespace ShoppingCart.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly ShoppingCartDbContext dbContext;

    public InMemoryOrderRepository(ShoppingCartDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Order> AddAsync(Order order)
    {
        var entry = await dbContext.Set<Order>().AddAsync(order);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Order?> GetByIdAsync(int orderId)
    {
        return await dbContext.Set<Order>()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await dbContext.Set<Order>()
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}
