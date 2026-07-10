using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Persistence;

namespace ShoppingCart.Infrastructure.Repositories;

public class InMemoryCartRepository : ICartRepository
{
    private readonly ShoppingCartDbContext dbContext;

    public InMemoryCartRepository(ShoppingCartDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CartItem> AddAsync(CartItem item)
    {
        var entry = await dbContext.Set<CartItem>().AddAsync(item);
        await dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(int itemId)
    {
        var item = await GetItemAsync(itemId);
        if (item is null)
            return false;

        dbContext.Set<CartItem>().Remove(item);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<CartItem?> GetItemAsync(int itemId)
    {
        return await dbContext.Set<CartItem>().FirstOrDefaultAsync(i => i.Id == itemId);
    }

    public async Task<CartItem?> GetItemByProductIdAsync(int productId)
    {
        return await dbContext.Set<CartItem>().FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task<IEnumerable<CartItem>> GetItemsAsync()
    {
        return await dbContext.Set<CartItem>().AsNoTracking().ToListAsync();
    }

    public async Task<CartItem> UpdateAsync(CartItem item)
    {
        dbContext.Set<CartItem>().Update(item);
        await dbContext.SaveChangesAsync();
        return item;
    }

    public async Task ClearAsync()
    {
        var items = await dbContext.Set<CartItem>().ToListAsync();
        if (items.Any())
        {
            dbContext.Set<CartItem>().RemoveRange(items);
            await dbContext.SaveChangesAsync();
        }
    }
}
